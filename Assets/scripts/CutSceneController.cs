using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class CutSceneController : MonoBehaviour {

    //NetworkManager net;
    void Start()
    {
        //net = GetComponent<NetworkManager>();
        //net.onlineScene = GameController.nextLevelName;
        Time.timeScale = 1.0f;
        //StartCoroutine(LoadNextScene());
        if (SceneManager.GetActiveScene().name == "Tip")
            Invoke("Loading", 5.0f);
        else
            Invoke("Loading",1.0f);
    }

    IEnumerator LoadNextScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(GameController.nextLevelName);
        yield return async;

    }
    void Loading()
    {
        StartCoroutine(LoadNextScene());
    }
}
