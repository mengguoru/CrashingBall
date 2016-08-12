using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BigPlayerController : NetworkBehaviour
{

    public Rigidbody2D player;
    float velocity;
    float upvel;
    float force;
    int l;
    public AudioClip jump;
    //public GameObject NS;
    int t = 0;
    int u;
    int d;
    public bool canControl;
    public Button btnCancel;
    bool isInCh3;
    public bool isTurnover;

    void Start()
    {
        canControl = true;
        if (MainMenuButton.mode)
        {
            if (!isLocalPlayer)
            {
                canControl = false;

            }

        }
        player = GetComponent<Rigidbody2D>();
        velocity = 10f;
        u = 0;
        d = 0;
        if (SceneManager.GetActiveScene().name == "Lv1.5")
            l = 1;
        else
            l = 0;

        string RegexStr = @"^Lv3.[\w\W]*";
        if (Regex.IsMatch(SceneManager.GetActiveScene().name, RegexStr))
            isInCh3 = true;
        else
            isInCh3 = false;

        isTurnover = false;
    }

    void Update()
    {
        if (isInCh3 && d==0)
        {
            if (GameObject.Find("turnoverControl").GetComponent<SpaceTransController>().control)
            {
                canControl = true;
                d = 1;
            }
            else
                canControl = false;
        }
        if (isInCh3 && GameObject.Find("turnoverControl").GetComponent<SpaceTransController>().control)
        {
            CheckPosition();
            //Debug.Log(isTurnover);
        }

        if (canControl)
        {
            if (MagnetismController.trick /*&& !MainMenuButton.mode) || (gameObject.GetComponent<MagnetismController>().contrick && MainMenuButton.mode)*/)
            {
                btnCancel.GetComponent<Button>().onClick.AddListener(OnCancelButtonClick);
                
                if (Input.GetKeyDown("s"))
                {
                    MagnetismController.trick = false;
                }
            }

             if (player.velocity.x <= 3 && (Input.GetKey("d") || (CrossPlatformInputManager.GetAxis("bigPlayerHorizontal") > 0)) && this.GetComponent<CollisionController>().isTouchingFloor == 1)
             {
                 player.velocity += new Vector2(1, 0);
             }

             else if (t == 0 && (Input.GetKey("d") || (CrossPlatformInputManager.GetAxis("bigPlayerHorizontal") > 0.7f)) && this.GetComponent<CollisionController>().isTouchingFloor == 0)
             {
                 player.AddForce(new Vector2(200 - 20 * 9 * l, 0));
                 t = 1;
             }
             else if (t == 0 && (Input.GetKey("a") || CrossPlatformInputManager.GetAxis("bigPlayerHorizontal") < -0.7f) && this.GetComponent<CollisionController>().isTouchingFloor == 0)
             {
                 player.AddForce(new Vector2(-200 + 20 * 9 * l, 0));
                 t = 1;
             }
             else if (player.velocity.x >= -3 && (Input.GetKey("a") || CrossPlatformInputManager.GetAxis("bigPlayerHorizontal") < 0) && this.GetComponent<CollisionController>().isTouchingFloor == 1)
                 player.velocity += new Vector2(-1, 0);
            if (isTurnover)
            {
                if (u == 0 && (Input.GetKeyDown("s") || CrossPlatformInputManager.GetAxis("bigPlayerVertical") < -0.4f) && GetComponent<CollisionController>().isTouchingFloor == 1)
                {
                    player.velocity -= new Vector2(0, velocity);
                    GetComponent<AudioSource>().clip = jump;
                    GetComponent<AudioSource>().Play();
                    u = 1;
                }
            }
            else
            {
                
                if (u == 0 && (Input.GetKeyDown("w") || CrossPlatformInputManager.GetAxis("bigPlayerVertical") > 0.6f) && GetComponent<CollisionController>().isTouchingFloor == 1)
                {
                    player.velocity += new Vector2(0, velocity);
                    GetComponent<AudioSource>().clip = jump;
                    GetComponent<AudioSource>().Play();
                    u = 1;
                }
            }

            if (GetComponent<CollisionController>().inBorder)
            {
                if (isTurnover)
                {
                    if ((Input.GetKeyDown("s") || CrossPlatformInputManager.GetAxis("bigPlayerVertical") < -0.4f))
                    {
                        player.velocity -= new Vector2(0, velocity);
                        GetComponent<AudioSource>().clip = jump;
                        GetComponent<AudioSource>().Play();
                        GetComponent<CollisionController>().inBorder = false;
                    }
                }
                else
                {
                    if ((Input.GetKeyDown("w") || CrossPlatformInputManager.GetAxis("bigPlayerVertical") > 0.6f))
                    {
                        player.velocity += new Vector2(0, velocity);
                        GetComponent<AudioSource>().clip = jump;
                        GetComponent<AudioSource>().Play();
                        GetComponent<CollisionController>().inBorder = false;
                    }
                }
            }


            if (t == 1 && GetComponent<CollisionController>().isTouchingFloor == 1)
            {
                t = 0;
            }
            if (u == 1 && GetComponent<CollisionController>().isTouchingFloor == 0)
            {
                u = 0;
            }
        }
        
    }

   void OnCancelButtonClick()
    {
        MagnetismController.trick = false;
    }

    void CheckPosition()
    {
        if (gameObject.transform.position.x < 0 && gameObject.transform.position.x > -Screen.width / 2 && gameObject.transform.position.y > 0 && gameObject.transform.position.y < Screen.height / 2)
        {
            isTurnover = true;
            player.gravityScale = -1;
        }
        else if (gameObject.transform.position.x > 0 && gameObject.transform.position.x < Screen.width / 2 && gameObject.transform.position.y < 0 && gameObject.transform.position.y > -Screen.height / 2)
        {
            isTurnover = true;
            player.gravityScale = -1;
        }
        else
        {
            isTurnover = false;
            player.gravityScale = 1;

        }
    }

}



  

    

