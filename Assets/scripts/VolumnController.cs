using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class VolumnController : MonoBehaviour {

    //public AudioSource bgm;
    public GameObject[] sound;
    public static float[] volumn;//第一个是背景音乐 第二个是音效

    // Update is called once per frame
    void Update () {
        string RegexStr = @"^Lv[\w\W]*";
        string RegexStr1 = @"^Trial[\w\W]*";
       

        if (MainMenuButton.hadRegulateSlider != 0)
        {
            if (Regex.IsMatch(SceneManager.GetActiveScene().name, RegexStr) || (Regex.IsMatch(SceneManager.GetActiveScene().name, RegexStr1) && SceneManager.GetActiveScene().name != "Trial"))
                GameObject.Find("bgm").GetComponent<AudioSource>().volume = volumn[0];
            else
                GameObject.Find("AudioManager").GetComponent<AudioSource>().volume = volumn[0];
            if (sound.Length > 0)
            {
                for (int i = 0; i < sound.Length; i++)
                {
                    sound[i].GetComponent<AudioSource>().volume = volumn[1];
                }
                Debug.Log(volumn[0]);
            }
        }
        else
        {
            if (Regex.IsMatch(SceneManager.GetActiveScene().name, RegexStr) || (Regex.IsMatch(SceneManager.GetActiveScene().name, RegexStr1) && SceneManager.GetActiveScene().name != "Trial"))
                GameObject.Find("bgm").GetComponent<AudioSource>().volume = 0.5f;
            else
                GameObject.Find("AudioManager").GetComponent<AudioSource>().volume = 0.5f;
            if (sound.Length > 0)
                if (sound.Length > 0)
            {
                for (int i = 0; i < sound.Length; i++)
                {
                    sound[i].GetComponent<AudioSource>().volume = 0.5f;
                }
            }
            volumn = new float[2] { 0.5f, 0.5f };

        }
	}
}
