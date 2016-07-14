using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuButton : MonoBehaviour {

    public static int hadRegulateSlider;

    void Awake()
    {
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
        CreateFile(Application.persistentDataPath, "data1.txt", "0");
    }

    void Start()
    {
        //Debug.Log(hadEnterOption);
    }

    public void OnStartBtnClick()
    {
        SceneManager.LoadScene("ModeChoose");
    }


    public void OnOptionBtnClick()
    {
        hadRegulateSlider = 1;
        SceneManager.LoadScene("Option");
    }

    public void OnQuitBtnClick()
    {
        //DeleteFile(Application.persistentDataPath, "data1.txt");
        Application.Quit();
    }

    public void OnStaffBtnClick()
    {
        SceneManager.LoadScene("Staff");
    }

    public void OnBackBtnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnIntroBtnClick()
    {
        SceneManager.LoadScene("Intro");
    }

    public void OnSingleModeBtnClick()
    {
        SceneManager.LoadScene("Chapter1");
    }

    public void OnConModeBtnClick()
    {
        SceneManager.LoadScene("ConChapter1");
    }

    public void OnTrialBtnClick()
    {
        SceneManager.LoadScene("Trial");
    }


    void CreateFile(string path, string name, string info)
    {
        //文件流信息
        StreamWriter sw;
        FileInfo t = new FileInfo(path + "//" + name);
        if (!t.Exists)
        {
            //如果此文件不存在则创建
            sw = t.CreateText();
        }
        else
        {
            //如果此文件存在则打开
            sw = t.AppendText();
        }
        //以行的形式写入信息
        sw.WriteLine(info);
        //关闭流
        sw.Close();
        //销毁流
        sw.Dispose();
    }

    void DeleteFile(string path, string name)
    {
        File.Delete(path + "//" + name);

    }
}
