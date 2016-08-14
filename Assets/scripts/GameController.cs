using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

public class GameController : MonoBehaviour {
    public int condition;
    string levelName;
    public static string nextLevelName;
    bool pausewindowShow;
    bool volumnwindowShow;
    bool winShow;
    bool horiz;
    public GUIStyle pause;
    public GUIStyle resume;
    public GUIStyle restart;
    public GUIStyle back;
    public GUIStyle option;
    public GUIStyle volumn;
    public GUIStyle slider;
    public GUIStyle thumb;


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
                int chapter = levelName[2] - 48;//1 2 3
                int level = levelName[4] - 48;//1 2 3 ...9
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
                    int chapter = nextLevelName[2] - 48;//1 2 3
                    int level = nextLevelName[4] - 48;//1 2 3 ...9
                    int index = (chapter - 1) * 9 + level ;
                    //EditFile(index, "0", Application.persistentDataPath+"/data.txt");
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

    void WinWindow(int WindwoID)
    {
        Time.timeScale = 0;
        
        if (GUI.Button(new Rect(Screen.width / 3, Screen.height * 3 / 5, Screen.width * 2 / 5, Screen.height / 5), "", back))
        {
            Time.timeScale = 1.0f;
            if (!MainMenuButton.mode)
               SceneManager.LoadScene("Chapter");
        }
        else if (GUI.Button(new Rect(Screen.width / 3, Screen.height / 5, Screen.width * 0.35f, Screen.height / 5), "next"))
        {
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
                GUI.Window(3, new Rect(0, 0, Screen.width, Screen.height), WinWindow, "");
        }
            
    }

    public void OnPauseButtonClick()
    {
        pausewindowShow = true;
    }

}
