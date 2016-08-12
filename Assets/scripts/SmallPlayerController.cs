using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class SmallPlayerController : NetworkBehaviour {

    public Rigidbody2D player;
    public GameObject BigPlayer;
    float velocity;
    float radius;
    float maxRadius;
    float angle;
    float speed;
    float upvel;
    int t;
    int u;
    int d;
    public AudioClip jump;
    //public GameObject NS;
    public bool canControl;
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

        velocity = 8.5f;
        t = 0;
        u = 0;
        d = 0;
        radius = 1.2f;
        maxRadius = 5;
        angle = 0;
        speed = 1;

        string RegexStr = @"^Lv3.[\w\W]*";
        if (Regex.IsMatch(SceneManager.GetActiveScene().name, RegexStr))
            isInCh3 = true;
        else
            isInCh3 = false;

        isTurnover = false;
    }

    void Update()
    {
        if (isInCh3 && d == 0)
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

        if (canControl) {
            if (MagnetismController.trick/* && !MainMenuButton.mode)||(gameObject.GetComponent<MagnetismController>().contrick && MainMenuButton.mode) */ /*&& !gameObject.GetComponent<CollisionController>().isInPortal*/)
            {
                this.gameObject.transform.position = GameObject.Find(BigPlayer.name).transform.position + new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);
                if (Input.GetKey(KeyCode.UpArrow) || CrossPlatformInputManager.GetAxis("smallPlayerVertical") > 0.5 && radius >= 0.25)
                {
                    radius += 0.02f;
                }
                else if (Input.GetKey(KeyCode.DownArrow) || CrossPlatformInputManager.GetAxis("smallPlayerVertical") < -0.5 && radius >= 0.25)
                {
                    radius -= 0.02f;
                }

                if (Input.GetKey(KeyCode.LeftArrow) || CrossPlatformInputManager.GetAxis("smallPlayerHorizontal") < -0.4f)
                {
                    angle += (speed * Time.deltaTime);
                }
                else if (Input.GetKey(KeyCode.RightArrow) || (CrossPlatformInputManager.GetAxis("smallPlayerHorizontal") > 0.4))
                {
                    angle -= (speed * Time.deltaTime);
                }

                if (radius > maxRadius)
                {
                    MagnetismController.trick = false;
                    /*if (MainMenuButton.mode)
                    {
                        if(isServer)
                            GetComponent<MagnetismController>().contrick = false;
                    }*/
                }

            }
            else
            {
               // Debug.Log(GetComponent<CollisionController>().inBorder);
                radius = 1.2f;
                if (player.velocity.x<=3 && (Input.GetKey(KeyCode.RightArrow) || (CrossPlatformInputManager.GetAxis("smallPlayerHorizontal") > 0)) && GetComponent<CollisionController>().isTouchingFloor == 1)
                {
                    player.velocity += new Vector2(0.5f, 0);
                }
                else if (t == 0 && (Input.GetKeyDown(KeyCode.RightArrow) || CrossPlatformInputManager.GetAxis("smallPlayerHorizontal") > 0.7f) && GetComponent<CollisionController>().isTouchingFloor == 0)
                {
                    player.AddForce(new Vector2(70, 0));
                    t = 1;
                }
                else if (t == 0 && (Input.GetKeyDown(KeyCode.LeftArrow) || CrossPlatformInputManager.GetAxis("smallPlayerHorizontal") < -0.7f) && GetComponent<CollisionController>().isTouchingFloor == 0)
                {
                    player.AddForce(new Vector2(-70, 0));
                    t = 1;
                }
                else if (player.velocity.x >= -3 && (Input.GetKey(KeyCode.LeftArrow) || CrossPlatformInputManager.GetAxis("smallPlayerHorizontal") < 0) && GetComponent<CollisionController>().isTouchingFloor == 1)
                {
                    player.velocity += new Vector2(-0.5f, 0);

                }
                if (isTurnover)
                {
                    
                    if (u == 0 && (Input.GetKeyDown(KeyCode.DownArrow) || CrossPlatformInputManager.GetAxis("smallPlayerVertical") < -0.4f) && GetComponent<CollisionController>().isTouchingFloor == 1)
                    {
                        player.velocity -= new Vector2(0, velocity);
                        GetComponent<AudioSource>().clip = jump;
                        GetComponent<AudioSource>().Play();
                        u = 1;
                    }
                }
                else
                {
                   
                    if (u == 0 && (Input.GetKeyDown(KeyCode.UpArrow) || CrossPlatformInputManager.GetAxis("smallPlayerVertical") > 0.6f) && GetComponent<CollisionController>().isTouchingFloor == 1)
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
                        if ((Input.GetKeyDown(KeyCode.DownArrow) || CrossPlatformInputManager.GetAxis("smallPlayerVertical") < -0.4f))
                        {
                            player.velocity -= new Vector2(0, velocity);
                            GetComponent<AudioSource>().clip = jump;
                            GetComponent<AudioSource>().Play();
                            GetComponent<CollisionController>().inBorder = false;
                        }
                    }
                    else
                    {
                        if ((Input.GetKeyDown(KeyCode.UpArrow) || CrossPlatformInputManager.GetAxis("smallPlayerVertical") > 0.6f))
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
