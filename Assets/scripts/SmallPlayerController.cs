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
    public AudioClip jump;
    //public GameObject NS;
    public bool canControl;

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
        upvel = 0;
        radius = 0.8f;
        maxRadius = 5;
        angle = 0;
        speed = 1;
    }

    void Update()
    {
        // Debug.Log(MagnetismController.trick);
       
        if (canControl) {
            if (MagnetismController.trick/* && !MainMenuButton.mode)||(gameObject.GetComponent<MagnetismController>().contrick && MainMenuButton.mode) */&& !this.gameObject.GetComponent<CollisionController>().isInPortal)
            {
                //Animator anim = NS.GetComponent<Animator>();
                //AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
                this.gameObject.transform.position = GameObject.Find(BigPlayer.name).transform.position + new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);
                if (Input.GetKey(KeyCode.UpArrow) || CrossPlatformInputManager.GetAxis("smallPlayerVertical") > 0.5)
                {
                    radius += 0.01f;
                }
                else if (Input.GetKey(KeyCode.DownArrow) || CrossPlatformInputManager.GetAxis("smallPlayerVertical") < -0.5 && radius <= maxRadius)
                {
                    radius -= 0.01f;
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

                /*if (stateinfo.fullPathHash == Animator.StringToHash("Base Layer.Idle"))
                {
                    anim.SetBool("NS", true);

                }*/
            }
            else
            {
                /*string RegexStr = @"^Lv2[\w\W]*";
                if (Regex.IsMatch(SceneManager.GetActiveScene().name, RegexStr) || SceneManager.GetActiveScene().name == "Trial1.8")
                {
                    Animator anim = NS.GetComponent<Animator>();
                    AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
                    if (stateinfo.fullPathHash == Animator.StringToHash("Base Layer.megni"))
                    {
                        anim.SetBool("NS", false);

                    }
                }*/
               
                upvel = player.velocity.y;
                if ((Input.GetKey(KeyCode.RightArrow) || (CrossPlatformInputManager.GetAxis("smallPlayerHorizontal") > 0)) && GetComponent<CollisionController>().isTouchingFloor == 1)
                {
                    player.velocity = new Vector2(3, upvel);
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
                else if ((Input.GetKey(KeyCode.LeftArrow) || CrossPlatformInputManager.GetAxis("smallPlayerHorizontal") < 0) && GetComponent<CollisionController>().isTouchingFloor == 1)
                {
                    player.velocity = new Vector2(-3, upvel);

                }

                if (u==0 && (Input.GetKeyDown(KeyCode.UpArrow) || CrossPlatformInputManager.GetAxis("smallPlayerVertical") > 0.6f) && GetComponent<CollisionController>().isTouchingFloor == 1)
                {
                    player.velocity += new Vector2(0, velocity);
                    GetComponent<AudioSource>().clip = jump;
                    GetComponent<AudioSource>().Play();
                    u = 1;
                }
                //times = 0;
               // if (times >= 3)
                  //  times = 0;

               /* if ((Input.GetKeyDown(KeyCode.RightArrow)) && GetComponent<CollisionController>().isTouchingFloor == 1)
                {
                    player.velocity = new Vector2(2, upvel);
                }
                else if (t == 0 && (Input.GetKeyDown(KeyCode.RightArrow) ) && GetComponent<CollisionController>().isTouchingFloor == 0)
                {
                    player.AddForce(new Vector2(60, 0));
                    t = 1;
                }
                else if (t == 0 && (Input.GetKeyDown(KeyCode.LeftArrow) ) && GetComponent<CollisionController>().isTouchingFloor == 0)
                {
                    player.AddForce(new Vector2(-60, 0));
                    t = 1;
                }
                else if ((Input.GetKey(KeyCode.LeftArrow) ) && GetComponent<CollisionController>().isTouchingFloor == 1)
                {
                    player.velocity = new Vector2(-2, upvel);

                }

                if ((Input.GetKeyDown(KeyCode.UpArrow) ) && GetComponent<CollisionController>().isTouchingFloor == 1)
                {
                    player.velocity += new Vector2(0, velocity);


                    GetComponent<AudioSource>().clip = jump;
                    GetComponent<AudioSource>().Play();
                }*/
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

}
