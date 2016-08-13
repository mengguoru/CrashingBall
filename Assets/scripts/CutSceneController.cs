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
        Invoke("Loading",1);
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
