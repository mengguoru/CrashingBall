using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class EndChoose : MonoBehaviour {

    public GameObject BigPlayer;
    public GameObject SmallPlayer;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
        if (BigPlayer.GetComponent<DoorCollision>().num == 1 && SmallPlayer.GetComponent<DoorCollision>().num == 1)
        {
           
        }
        else if (BigPlayer.GetComponent<DoorCollision>().num == 2 && SmallPlayer.GetComponent<DoorCollision>().num == 2)
        {
           
        }
        else if (BigPlayer.GetComponent<DoorCollision>().num == 3 && SmallPlayer.GetComponent<DoorCollision>().num == 3)
        {
            
        }
    }
}
