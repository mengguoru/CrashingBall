using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class temp_GameController : MonoBehaviour {
    public static temp_GameController instance = null;

	// Use this for initialization
	void Start () {
        //singleton pattern
        if (null == instance)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if("Scene1" == SceneManager.GetActiveScene().name)
                SceneManager.LoadScene("Scene2");
            else if("Scene2" == SceneManager.GetActiveScene().name)
                SceneManager.LoadScene("Scene1");
        }
	}
}
