using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class DoorChoose : MonoBehaviour {

    public GameObject BigPlayer;
    public GameObject SmallPlayer;
    public Text level;
    static string lvName;
    bool once;
	void Start () {
        Debug.Log(lvName);
        if (lvName == null)
            lvName = level.GetComponent<Text>().text;
        else
            level.GetComponent<Text>().text = lvName;
        once = true;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(lvName);
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || CrossPlatformInputManager.GetAxis("PlayerHorizontal") < -0.7f)
        {
            BigPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
            SmallPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        }
        else if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || CrossPlatformInputManager.GetAxis("PlayerHorizontal") > 0.7f)
        {
            BigPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
            SmallPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
        }

    }

    public void OnSureButtonClick()
    {
        //level.GetComponent<Text>().text = lvName;
        if (once && BigPlayer.GetComponent<DoorCollision>().num == 1 && SmallPlayer.GetComponent<DoorCollision>().num == 1)
        {
            if (lvName == "1" || lvName == "6")
            {
                CountNextLevel();
            }
            else
            {
                lvName = "1";
            }
            once = false;
            //sure.GetComponent<Button>().onClick.AddListener(OnSureButtonClick);
        }
        else if (once && BigPlayer.GetComponent<DoorCollision>().num == 2 && SmallPlayer.GetComponent<DoorCollision>().num == 2)
        {
            if (lvName == "2" || lvName == "5" || lvName == "7" || lvName == "8" || lvName == "9")
            {
                CountNextLevel();
            }
            else
            {
                lvName = "1";
            }
            once = false;
            //sure.GetComponent<Button>().onClick.AddListener(OnSureButtonClick);
        }
        else if (once && BigPlayer.GetComponent<DoorCollision>().num == 3 && SmallPlayer.GetComponent<DoorCollision>().num == 3)
        {
            if (lvName == "3" || lvName == "4")
            {
                CountNextLevel();
            }
            else
            {
                lvName = "1";
            }
            once = false;
            //sure.GetComponent<Button>().onClick.AddListener(OnSureButtonClick);
        }
        if (lvName == "10")
            SceneManager.LoadScene("end");
        else
            SceneManager.LoadScene("Lv3.9");
        //once = true;
       // Debug.Log(level.GetComponent<Text>().text);
    }

    void CountNextLevel()
    {
        int num = int.Parse(lvName) + 1;
        lvName = num.ToString();
        Debug.Log(lvName);
    }
}
