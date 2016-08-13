using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class ButtonController : MonoBehaviour {
    //List<int> levelStatus = new List<int>();
    List<string> arr = new List<string>();
    public Sprite[] locked;
    public Sprite[] unlocked;
    public GameObject[] button;
    public static string conLevelName;

   /* void LoadXml()
    {
        XmlDocument xml = new XmlDocument();
        XmlReaderSettings set = new XmlReaderSettings();
        set.IgnoreComments = true;
        xml.Load(XmlReader.Create((Application.persistentDataPath + "/data.xml"), set));
        XmlNodeList xmlNodeList = xml.SelectSingleNode("level").ChildNodes;

        foreach (XmlElement xl in xmlNodeList)
        {
            levelStatus.Add(int.Parse(xl.InnerText));
            Debug.Log(xl.InnerText);
        }
        for (int i = 0; i < levelStatus.Count; i++)
            Debug.Log(levelStatus[i]);
    }*/
    /**
   * path：读取文件的路径
   * name：读取文件的名称
   */
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

    void Start()
    {
        int j,k;
        Time.timeScale = 1.0f;
        arr = LoadFile(Application.persistentDataPath, "data1.txt");
        for(j = 0; j < 8; j++)//第一章前八关
        {
            if (arr[j] == "0")
                break;
        }
        if(j == 8)//第一章前八关全部通过
        {
            button[0].GetComponent<Button>().image.sprite = unlocked[0];
        }
        else
        {
            button[0].GetComponent<Button>().image.sprite = locked[0];
        }

        for (k = 9; k < 17; k++)//第二章前八关
        {
            if (arr[k] == "0")
                break;
        }
        if (k == 8)//第二章前八关全部通过
        {
            button[1].GetComponent<Button>().image.sprite = unlocked[1];
        }
        else
        {
            button[1].GetComponent<Button>().image.sprite = locked[1];
        }
    }

    public void OnBackBtnClick() {
        SceneManager.LoadScene("ModeChoose");
    }


    public void OnLevelChooseBtnClick(GameObject obj)
    {
        string levelName = obj.name;
        if (obj.name != "Lv1.9" || obj.name != "Lv2.9")
        {
            GameController.nextLevelName = levelName;
            SceneManager.LoadScene("CutScene");
        }
        else if(obj.name == "Lv1.9")
        {
            if(arr[8] == "1")
            {
                GameController.nextLevelName = levelName;
                SceneManager.LoadScene("CutScene");
            }
        }
        else if(obj.name == "Lv2.9")
        {
            if (arr[17] == "1")
            {
                GameController.nextLevelName = levelName;
                SceneManager.LoadScene("CutScene");
            }
        }
      //  else
           // Debug.Log("还没通关哟");
    }

    public void OnTrailChooseBtnClick(GameObject obj)
    {
        SceneManager.LoadScene(obj.name);
    }


    /*public void OnConNextChapterBtnClick()
    {
        SceneManager.LoadScene("ConChapter2");
    }

    public void OnConLastChapterBtnClick()
    {
        SceneManager.LoadScene("ConChapter1");
    }*/

    public void OnBackConBtnClick()
    {
        SceneManager.LoadScene("ModeChoose");
    }

    /*public void OnConLevelChooseBtnClick(GameObject obj)
    {
        string levelName = obj.name;
        int chapter = levelName[2] - 48;//1 2 3
        int level = levelName[4] - 48;//1 2 3 ...9
        int index = (chapter - 1) * 9 + level - 1;
        // if (levelStatus[index] == 0)
        // {
        conLevelName = obj.name;
        SceneManager.LoadScene("NetworkConnection");
       // }
       // else
            //Debug.Log("还没通关哟");
    }*/

}
