using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MagTrial : MonoBehaviour {
    public Text showText;
    public string[] dialogue;
    public int currentState;
    public string coutinueText;

	// Use this for initialization
	void Start () {
        showText.text = "";
        coutinueText = "     (按space键继续)";
        dialogue = new string[] {
            "两球分别与两个磁极相碰，异性小球相互吸引，碰撞后触发电磁力状态", 
            "电磁力状态：大球仍可正常操作，小球将随着大球运动",
            "电磁力状态：小球绕大球顺时针转（摇杆向左）",
            "电磁力状态：小球绕大球逆时针转(摇杆向右)",
            "电磁力状态：小球远离大球(摇杆向上)，距离较远时该状态将消失",
            "电磁力状态：小球靠近大球(摇杆向下)，大球按Cancel键取消该状态",
            "教学结束，开启你的解谜之路吧！"
        };
        currentState = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (0 == currentState)
        {
            myStateMachine(currentState);
            currentState++;
        }
        if (Input.GetKeyDown(KeyCode.Space) && currentState<=dialogue.Length)
        {
            myStateMachine(currentState);
            currentState++;
        }
        if (currentState == dialogue.Length)
            SceneManager.LoadScene("Chapter1");
	}

    void myStateMachine(int i)
    {
        showText.text = dialogue[i]+coutinueText;
    }
}
