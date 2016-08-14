using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuButton : MonoBehaviour {

    public static int hadRegulateSlider;
    public static bool mode;//true 为联机 false为单人

    void Awake()
    {
        
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
        DeleteFile(Application.persistentDataPath, "data.txt");
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
        //GameController.nextLevelName = "Chapter";
        SceneManager.LoadScene("Chapter");
        mode = false;
    }

    public void OnConModeBtnClick()
    {
        SceneManager.LoadScene("NetworkConnection");
        mode = true;
    }

    public void OnTrialBtnClick()
    {
        SceneManager.LoadScene("Trial");
        mode = false;
    }

    public void OnCollectBtnClick()
    {
        SceneManager.LoadScene("Collection");
    }
    
    void DeleteFile(string path, string name)
    {
        File.Delete(path + "//" + name);

    }
}
