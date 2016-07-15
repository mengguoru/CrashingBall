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
    public GameObject NS;
    int t = 0;
    public bool canControl;
    int times = 0;
    public Button btnCancel;

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
        velocity = 7.5f;
        upvel = 0;
        if (SceneManager.GetActiveScene().name == "Lv1.5")
            l = 1;
        else
            l = 0;
    }

    void Update()
    {
        if (canControl)
        {
            upvel = player.velocity.y;
            if (MagnetismController.trick /*&& !MainMenuButton.mode) || (gameObject.GetComponent<MagnetismController>().contrick && MainMenuButton.mode)*/)
            {
                Animator anim = NS.GetComponent<Animator>();
                AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
                if (stateinfo.fullPathHash == Animator.StringToHash("Base Layer.Idle"))
                {
                    anim.SetBool("NS", true);

                }
                btnCancel.GetComponent<Button>().onClick.AddListener(OnCancelButtonClick);
                
                if (Input.GetKeyDown("s"))
                {
                    MagnetismController.trick = false;
                    if (MainMenuButton.mode)
                    {
                        if(isServer)
                            GetComponent<MagnetismController>().contrick = false;
                    }
                      
                    if (stateinfo.fullPathHash == Animator.StringToHash("Base Layer.Bigmegni"))
                    {
                        anim.SetBool("NS", false);

                    }
                }
            }

             if ((Input.GetKey("d") || (CrossPlatformInputManager.GetAxis("bigPlayerHorizontal") > 0.5)) && this.GetComponent<CollisionController>().isTouchingFloor == 1)
                 {
                 player.velocity = new Vector2(2, upvel);
             }

             else if (t == 0 && (Input.GetKey("d") || (CrossPlatformInputManager.GetAxis("bigPlayerHorizontal") > 0.5)) && this.GetComponent<CollisionController>().isTouchingFloor == 0)
             {
                 player.AddForce(new Vector2(100 - 10 * 9 * l, 0));
                 t = 1;
             }
             else if (t == 0 && (Input.GetKey("a") || CrossPlatformInputManager.GetAxis("bigPlayerHorizontal") < -0.5) && this.GetComponent<CollisionController>().isTouchingFloor == 0)
             {
                 player.AddForce(new Vector2(-100 + 10 * 9 * l, 0));
                 t = 1;
             }
             else if ((Input.GetKey("a") || CrossPlatformInputManager.GetAxis("bigPlayerHorizontal") < -0.5) && this.GetComponent<CollisionController>().isTouchingFloor == 1)
                 player.velocity = new Vector2(-2, upvel);

             }

             if ((Input.GetKeyDown("w") || CrossPlatformInputManager.GetAxis("bigPlayerVertical") > 0.5) && GetComponent<CollisionController>().isTouchingFloor == 1)
             {
                 if(times == 0)
                 {
                     player.velocity += new Vector2(0, velocity);
                 }
                 times++;
                 GetComponent<AudioSource>().clip = jump;
                 GetComponent<AudioSource>().Play();
             }

             if (times >= 3)
                 times = 0;
                 
            /*
            if ((Input.GetKey("d") ) && this.GetComponent<CollisionController>().isTouchingFloor == 1)
            {
                player.velocity = new Vector2(2, upvel);
            }

            else if (t == 0 && (Input.GetKey("d") ) && this.GetComponent<CollisionController>().isTouchingFloor == 0)
            {
                player.AddForce(new Vector2(100 - 10 * 9 * l, 0));
                t = 1;
            }
            else if (t == 0 && (Input.GetKey("a") ) && this.GetComponent<CollisionController>().isTouchingFloor == 0)
            {
                player.AddForce(new Vector2(-100 + 10 * 9 * l, 0));
                t = 1;
            }
            else if ((Input.GetKey("a") ) && this.GetComponent<CollisionController>().isTouchingFloor == 1)
                player.velocity = new Vector2(-2, upvel);

        }
    
        if ((Input.GetKeyDown("w") ) && GetComponent<CollisionController>().isTouchingFloor == 1)
        {

            player.velocity += new Vector2(0, velocity);

            GetComponent<AudioSource>().clip = jump;
            GetComponent<AudioSource>().Play();
        }*/
        if (t == 1 && GetComponent<CollisionController>().isTouchingFloor == 1) { t = 0; }
        }

   void OnCancelButtonClick()
    {
        MagnetismController.trick = false;
        if (MainMenuButton.mode)
        {
            if (isServer)
                GetComponent<MagnetismController>().contrick = false;
        }
        Animator anim = NS.GetComponent<Animator>();
        AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateinfo.fullPathHash == Animator.StringToHash("Base Layer.Bigmegni"))
        {
            anim.SetBool("NS", false);
            Debug.Log("cancel");
        }
    }
}



  

    

