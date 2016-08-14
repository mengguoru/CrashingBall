using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
    public int condition;
    string levelName;
    public static string nextLevelName;
    bool pausewindowShow;
    bool volumnwindowShow;
    bool winShow;
    bool horiz;
    public GUIStyle win;
    public GUIStyle next;
    public GUIStyle pause;
    public GUIStyle resume;
    public GUIStyle restart;
    public GUIStyle back;
    public GUIStyle option;
    public GUIStyle volumn;
    public GUIStyle slider;
    public GUIStyle thumb;
    List<string> arr = new List<string>();
    int chapter;
    int level;

    void Start()
    {
        pausewindowShow = false;
        volumnwindowShow = false;
        winShow = false;
        levelName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1.0f;
        condition = 2;
        if (levelName != "CutScene" && levelName != "Trial1.8" && levelName != "end")
        {
            string RegexStr = @"^Lv[\w\W]*";
            if(Regex.IsMatch(levelName, RegexStr))
            {
                if (!MainMenuButton.mode)
                {
                    GameObject.Find("Canvas").transform.FindChild("ButtonDisconnect").gameObject.SetActive(false);
                }
                chapter = levelName[2] - 48;//1 2 3
                level = levelName[4] - 48;//1 2 3 ...9
                if (level != 9)
                    nextLevelName = "Lv" + chapter.ToString() + "." + (level + 1).ToString();
                else
                {
                    if (chapter == 1 || chapter == 2)
                        nextLevelName = "Lv" + (chapter + 1).ToString() + "." + (1).ToString();
                    else
                        nextLevelName = "OverAnimation";
                }
                Debug.Log(nextLevelName);
            }
            else
            {
                string[] str = levelName.Split('.');
                int num = int.Parse(str[1])+1;
                nextLevelName = str[0] + "." + num.ToString();
            }
        }
        
    }

    void Update()
    {
        if (levelName != "CutScene" && levelName != "Trial1.8")
        {
            if (condition == 0)
            {
                //GetComponent<AudioSource>().Play();
                string RegexStr1 = @"^Lv[\w\W]*";
                if (Regex.IsMatch(levelName, RegexStr1))
                {
                    int index = (chapter - 1) * 9 + level ;
                    EditFile(index, "1", Application.persistentDataPath+"/data.txt");
                    winShow = true;
                    //SceneManager.LoadScene("CutScene");
                }
                else
                {
                   // if (nextLevelName == "Trial1.9")
                   //     SceneManager.LoadScene("Chapter1");
                    //else
                        SceneManager.LoadScene(nextLevelName);
                }

            }
        }
    }
    

    void EditFile(int curLine, string newLineValue, string patch)
    {
        FileStream fs = new FileStream(patch, FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8"));
        string line = sr.ReadLine();
        StringBuilder sb = new StringBuilder();
        for (int i = 1; line != null; i++)
        {
            sb.Append(line + "\r\n");
            if (i != curLine - 1)
                line = sr.ReadLine();
            else
            {
                sr.ReadLine();
                line = newLineValue;
            }
        }
        sr.Close();
        fs.Close();
        FileStream fs1 = new FileStream(patch, FileMode.Open, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs1);
        sw.Write(sb.ToString());
        sw.Close();
        fs1.Close();
    }

    List<string> LoadFile(string path, string name)
    {
        //使用流的形式读取
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(path + "//" + name);
        }
        catch (Exception e)
        {
            //路径与名称未找到文件则直接返回空
            return null;
        }
        string line;
        List<string> arrlist = new List<string>();
        while ((line = sr.ReadLine()) != null)
        {
            //一行一行的读取
            //将每一行的内容存入数组链表容器中
            arrlist.Add(line);
        }
        //关闭流
        sr.Close();
        //销毁流
        sr.Dispose();
        //将数组链表容器返回
        return arrlist;
    }

    void WinWindow(int WindwoID)
    {
        Time.timeScale = 0;
        arr = LoadFile(Application.persistentDataPath, "data.txt");
        int index = (chapter - 1) * 9 + level -1;
        if(index != 8 && index != 17 && index != 26)
        {
            GameObject.Find("piece").GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
        if (GUI.Button(new Rect(Screen.width / 3, Screen.height * 3 / 5, Screen.width * 2 / 5, Screen.height / 5), "", back))
        {
            Time.timeScale = 1.0f;
            if (!MainMenuButton.mode)
               SceneManager.LoadScene("Chapter");
        }
        else if (GUI.Button(new Rect(Screen.width / 3, Screen.height / 5, Screen.width * 0.35f, Screen.height / 5), "",next))
        {
            if (nextLevelName == "Lv1.9")
            {
                int i;
                for(i = 0; i < 8; i++)
                {
                    if (arr[i] != "1")
                        break;
                }
                if(i == 8)
                {
                    SceneManager.LoadScene("CutScene");
                }
            }
            else if (nextLevelName == "Lv2.9")
            {
                int i;
                for (i = 9; i < 17; i++)
                {
                    if (arr[i] != "1")
                        break;
                }
                if (i == 17)
                {
                    SceneManager.LoadScene("CutScene");
                }
            }
            else if (nextLevelName == "Lv3.9")
            {
                int i;
                for (i = 0; i < 26; i++)
                {
                    if (arr[i] != "1")
                        break;
                }
                if (i == 26)
                {
                    SceneManager.LoadScene("CutScene");
                }
            }
            else if(nextLevelName == "Lv2.1")
            {
                SceneManager.LoadScene("Chapter1End");
            }
            else
                SceneManager.LoadScene("CutScene");
            //Time.timeScale = 1.0f;
        }
    }
    void VolumnWindow(int WindowID)
    {
        MainMenuButton.hadRegulateSlider = 1;
        VolumnController.volumn[0] = GUI.HorizontalSlider(new Rect(Screen.width * 0.4f, Screen.height * 0.38f, Screen.width *8/35, Screen.width / 35), VolumnController.volumn[0], 0.0f, 1.0f, slider, thumb);
         VolumnController.volumn[1] = GUI.HorizontalSlider(new Rect(Screen.width * 0.4f, Screen.height /2, Screen.width *8 / 35, Screen.width / 35), VolumnController.volumn[1], 0.0f, 1.0f, slider, thumb);
        if(GUI.Button(new Rect(Screen.width / 3, Screen.height*0.55f, Screen.width * 2 / 5, Screen.height / 5), "", back))
        {
            pausewindowShow = true;
            volumnwindowShow = false;
        }
    }

    void PauseWindow(int WindowID)
    {
        Time.timeScale = 0;
        //Debug.Log("pause");
        if (GUI.Button(new Rect(Screen.width/3, Screen.height*3/5, Screen.width*2/5, Screen.height / 5), "",back))
        {
            Time.timeScale = 1.0f;
            string RegexStr1 = @"^Lv[\w\W]*";
            if (Regex.IsMatch(levelName, RegexStr1))
            {
                if (!MainMenuButton.mode)
                    SceneManager.LoadScene("Chapter");
            }    
            else
                SceneManager.LoadScene("Trial");
        }
        else if(GUI.Button(new Rect(Screen.width / 3, Screen.height/5, Screen.width*0.35f, Screen.height / 5), "",resume))
        {
            pausewindowShow = false;
            Time.timeScale = 1.0f;
        }
        else if(GUI.Button(new Rect(Screen.width*3 / 8, Screen.height*0.35f, Screen.width*0.31f, Screen.height / 5), "",restart))
        {
            
            if (!MainMenuButton.mode)
                SceneManager.LoadScene(levelName);
        }
        else if(GUI.Button(new Rect(Screen.width / 3, Screen.height*0.45f, Screen.width * 0.38f, Screen.height / 5), "", option))
        {
            pausewindowShow = false;
            volumnwindowShow = true;
            Debug.Log("option");
        }
    }

    void OnGUI()
    {
        if (levelName != "CutScene")
        {
            if (pausewindowShow)
                GUI.Window(1, new Rect(0, 0, Screen.width, Screen.height), PauseWindow, "",pause);
            if(volumnwindowShow)
                GUI.Window(2, new Rect(0, 0, Screen.width, Screen.height), VolumnWindow, "", volumn);
            if (winShow)
                GUI.Window(3, new Rect(0, 0, Screen.width, Screen.height), WinWindow, "", win);
        }
            
    }

    public void OnPauseButtonClick()
    {
        pausewindowShow = true;
    }

}
