using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollectionController : MonoBehaviour {

    List<string> arr = new List<string>();
    public GameObject[] sp;
    public Texture2D[] ch1;
    public Texture2D[] ch2;
    public Texture2D[] ch3;
    public Texture2D[] tp;
    public GUIStyle back;
    public GUIStyle window;
    bool[] show;
    bool once;
    // Use this for initialization
    void Start () {
        arr = LoadFile(Application.persistentDataPath, "data.txt");
        for(int i = 0; i < arr.Count; i++)
        {
            Debug.Log("suipian"+arr[i]);
        }
        show = new bool[3] { false, false, false };
        once = true;
        GameObject.Find("ch1Btn").GetComponent<Button>().onClick.AddListener(OnCh1BtnClick);
        GameObject.Find("ch2Btn").GetComponent<Button>().onClick.AddListener(OnCh2BtnClick);
        GameObject.Find("ch3Btn").GetComponent<Button>().onClick.AddListener(OnCh3BtnClick);
    }
	
	// Update is called once per frame
	void Update () {
	
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
    public void OnCh1BtnClick()
    {
        show[0] = true;
        
    }

    public void OnCh2BtnClick()
    {
        show[1] = true;

    }

    public void OnCh3BtnClick()
    {
        show[2] = true;

    }

    public void OnBackBtnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Ch1Window(int WindowID)
    {
        Sprite[] piece = new Sprite[8];
        SpriteRenderer[] render = new SpriteRenderer[8];
        for (int i = 0; i < 8; i++)
        {
            render[i] = sp[i].GetComponent<SpriteRenderer>();
        }
        
        if (once)
        {
            for (int i = 0; i < 8; i++)
            {
                if (arr[i] == "1")
                {
                    //render[i] = sp[i].GetComponent<SpriteRenderer>();
                    piece[i] = Sprite.Create(ch1[i], new Rect(0, 0, ch1[i].width, ch1[i].height), new Vector2(0.5f, 0.5f));
                    //render[i].sprite = piece[i];
                    Debug.Log("sprite");
                }
                else
                {
                    piece[i] = Sprite.Create(tp[0], new Rect(0, 0, tp[0].width, tp[0].height), new Vector2(0.5f, 0.5f));
                }
                render[i].sprite = piece[i];
            }
            once = false;
        }
        GameObject.Find("ch1Btn").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ch2Btn").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ch3Btn").GetComponent<Button>().onClick.RemoveAllListeners();
        if (GUI.Button(new Rect(Screen.width * 0.7f, Screen.height * 3 / 5, Screen.width  / 3, Screen.height / 5), "", back))
        {
            once = true;
            show[0] = false;
            for (int i = 0; i < 8; i++)
            {
                //piece[i] = Sprite.Create(tp[0], new Rect(0, 0, Screen.height * 3.3f, Screen.height * 3.3f), new Vector2(0.5f, 0.5f));
                render[i].sprite = null;
                Debug.Log("null");
            }
            GameObject.Find("ch1Btn").GetComponent<Button>().onClick.AddListener(OnCh1BtnClick);
            GameObject.Find("ch2Btn").GetComponent<Button>().onClick.AddListener(OnCh2BtnClick);
            GameObject.Find("ch3Btn").GetComponent<Button>().onClick.AddListener(OnCh3BtnClick);
        }

    }

    void Ch2Window(int WindowID)
    {
        Sprite[] piece = new Sprite[8];
        SpriteRenderer[] render = new SpriteRenderer[8];
        for (int i = 0; i < 8; i++)
        {
            render[i] = sp[i].GetComponent<SpriteRenderer>();
        }

        if (once)
        {
            for (int i = 0; i < 8; i++)
            {
                if (arr[i+9] == "1")
                {
                    //render[i] = sp[i].GetComponent<SpriteRenderer>();
                    piece[i] = Sprite.Create(ch2[i], new Rect(0, 0, ch2[i].width, ch2[i].height), new Vector2(0.5f, 0.5f));
                    //render[i].sprite = piece[i];
                    Debug.Log("sprite");
                }
                else
                {
                    piece[i] = Sprite.Create(tp[1], new Rect(0, 0, tp[1].width, tp[1].height), new Vector2(0.5f, 0.5f));
                }
                render[i].sprite = piece[i];
            }
            once = false;
        }
        GameObject.Find("ch1Btn").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ch2Btn").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ch3Btn").GetComponent<Button>().onClick.RemoveAllListeners();
        if (GUI.Button(new Rect(Screen.width *0.72f, Screen.height * 3 / 5, Screen.width /3, Screen.height / 5), "", back))
        {
            once = true;
            show[1] = false;
            for (int i = 0; i < 8; i++)
            {
                //piece[i] = Sprite.Create(tp[0], new Rect(0, 0, Screen.height * 3.3f, Screen.height * 3.3f), new Vector2(0.5f, 0.5f));
                render[i].sprite = null;
                Debug.Log("null");
            }
            GameObject.Find("ch1Btn").GetComponent<Button>().onClick.AddListener(OnCh1BtnClick);
            GameObject.Find("ch2Btn").GetComponent<Button>().onClick.AddListener(OnCh2BtnClick);
            GameObject.Find("ch3Btn").GetComponent<Button>().onClick.AddListener(OnCh3BtnClick);
        }

    }

    void Ch3Window(int WindowID)
    {
        Sprite[] piece = new Sprite[8];
        SpriteRenderer[] render = new SpriteRenderer[8];
        for (int i = 0; i < 8; i++)
        {
            render[i] = sp[i].GetComponent<SpriteRenderer>();
        }

        if (once)
        {
            for (int i = 0; i < 8; i++)
            {
                if (arr[i + 18] == "1")
                {
                    //render[i] = sp[i].GetComponent<SpriteRenderer>();
                    piece[i] = Sprite.Create(ch3[i], new Rect(0, 0, ch3[i].width, ch3[i].height), new Vector2(0.5f, 0.5f));
                    //render[i].sprite = piece[i];
                    Debug.Log("sprite");
                }
                else
                {
                    piece[i] = Sprite.Create(tp[0], new Rect(0, 0, tp[0].width, tp[0].height), new Vector2(0.5f, 0.5f));
                }
                render[i].sprite = piece[i];
            }
            once = false;
        }
        GameObject.Find("ch1Btn").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ch2Btn").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ch3Btn").GetComponent<Button>().onClick.RemoveAllListeners();
        if (GUI.Button(new Rect(Screen.width *0.7f, Screen.height * 3 / 5, Screen.width /3, Screen.height / 5), "", back))
        {
            once = true;
            show[2] = false;
            for (int i = 0; i < 8; i++)
            {
                //piece[i] = Sprite.Create(tp[0], new Rect(0, 0, Screen.height * 3.3f, Screen.height * 3.3f), new Vector2(0.5f, 0.5f));
                render[i].sprite = null;
                Debug.Log("null");
            }
            GameObject.Find("ch1Btn").GetComponent<Button>().onClick.AddListener(OnCh1BtnClick);
            GameObject.Find("ch2Btn").GetComponent<Button>().onClick.AddListener(OnCh2BtnClick);
            GameObject.Find("ch3Btn").GetComponent<Button>().onClick.AddListener(OnCh3BtnClick);
        }

    }

    void OnGUI()
    {
        if (show[0])
        {
            GUI.Window(1, new Rect(0, 0, Screen.width, Screen.height), Ch1Window, "", window);
        }
        if (show[1])
        {
            GUI.Window(2, new Rect(0, 0, Screen.width, Screen.height), Ch2Window, "", window);
        }
        if (show[2])
        {
            GUI.Window(3, new Rect(0, 0, Screen.width, Screen.height), Ch3Window, "", window);
        }
    }
}
