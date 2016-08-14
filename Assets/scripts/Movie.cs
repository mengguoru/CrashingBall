using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Movie : MonoBehaviour {

    //电影纹理
    public MovieTexture movTexture;
    bool firstplay;

    void Start()
    {
        //设置电影纹理播放模式为循环
        movTexture.loop = false;
        firstplay = false;
    }

    void OnGUI()
    {
        //绘制电影纹理
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movTexture, ScaleMode.StretchToFill);
        if (!firstplay)
        {
            movTexture.Play();
            firstplay = true;
        }
        /*if (GUILayout.Button("播放/继续"))
        {
            //播放/继续播放视频
            if (!movTexture.isPlaying)
            {
                movTexture.Play();
            }

        }

        if (GUILayout.Button("暂停播放"))
        {
            //暂停播放
            movTexture.Pause();
        }*/

        if (GUILayout.Button("停止播放"))
        {
            //停止播放
            movTexture.Stop();
        }
    }

    void Update()
    {
        if (!movTexture.isPlaying && firstplay)
        {
            SceneManager.LoadScene("Lv2.1");
        }
    }
}
