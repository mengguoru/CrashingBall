using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CollisionController : MonoBehaviour {

    //public GameObject MainCamera;
    public int attribute;//1冰 2火 0无
    public int isTouchingFloor;
    List<Vector3> portalPosition;
    public GameObject[] Trigger;
    public GameObject[] portal;
    public GameObject[] TriggerDoor;
    public Sprite[] sp;
    public GameObject[] elementDoor;
    public AudioClip[] aud;
    public AudioClip[] laseraud;
    public AudioClip success;
    public bool isInPortal;

    int breakdooraudio;
    int dooraudio;
    bool failWindowShow;

    void Start () {
        attribute = 0;
        isTouchingFloor = 1;
        portalPosition = new List<Vector3>();
        breakdooraudio = 0;
        dooraudio = 0;
        failWindowShow = false;

        for(int j = 0; j < portal.Length; j++)
        {
            portalPosition.Add(portal[j].transform.position);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lv1.9")
        {
            int playerIsTrigger = 0;
            for (int i = 0; i < elementDoor.Length; i++)
            {
                float dis = Mathf.Abs(elementDoor[i].transform.position.y - transform.position.y);
                bool isTouchL = (elementDoor[i].transform.position.x - elementDoor[i].transform.localScale.y / 2) < transform.position.x ;
                bool isTouchR = (elementDoor[i].transform.position.x + elementDoor[i].transform.localScale.y / 2) > transform.position.x;
                if(elementDoor[i].gameObject.name == "FireDoor")
                {
                    Debug.Log(isTouchL);
                    Debug.Log(isTouchR);
                }
                if (dis < GetComponent<CircleCollider2D>().radius + elementDoor[i].transform.localScale.y / 2 && isTouchL && isTouchR)
                {
                    Debug.Log("dis");
                    Debug.Log(elementDoor[i].gameObject.name);
                    if (Regex.IsMatch(elementDoor[i].name, @"^IceDoor[\w\W]*") && attribute == 1)
                    {
                        Debug.Log("ice");
                        GetComponent<CircleCollider2D>().isTrigger = true;
                        playerIsTrigger++;
                        break;
                    }
                    
                    else if (Regex.IsMatch(elementDoor[i].name, @"^FireDoor[\w\W]*") && attribute == 2)
                    {
                        GetComponent<CircleCollider2D>().isTrigger = true;
                        playerIsTrigger++;
                        break;
                    }

                    else
                    {
                        GetComponent<CircleCollider2D>().isTrigger = false;
                    }
                }  
            }
            if(playerIsTrigger == 0)
            {
                Debug.Log(playerIsTrigger);
                GetComponent<CircleCollider2D>().isTrigger = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        string RegexStr1 = @"^Switch[\w\W]*";
        string RegexStr2 = @"^Switch[\w\W]*Door[\w\W]*";
        string RegexStr3 = @"^BigPlayer[\w\W]*";
        string RegexStr4 = @"^SmallPlayer[\w\W]*";

        if ((obj.name == "SmallTarget" && Regex.IsMatch(this.gameObject.name, RegexStr4)) || (obj.name == "BigTarget" && Regex.IsMatch(this.gameObject.name, RegexStr3)) )
        {
            Debug.Log("yes");
            Destroy(obj.gameObject);
            GameObject.Find("Main Camera").GetComponent<GameController>().condition--;
            GetComponent<AudioSource>().clip = success;
            GetComponent<AudioSource>().Play();
        }
        else if (Regex.IsMatch(obj.gameObject.name, RegexStr1) &&! Regex.IsMatch(obj.gameObject.name,RegexStr2))//机关门
        {
            Destroy(obj.gameObject);
            GameObject switchDoor = GameObject.Find(obj.gameObject.name + "Door");
            switchDoor.GetComponent<BoxCollider2D>().isTrigger = true;
            SpriteRenderer spriteRender = switchDoor.GetComponent<SpriteRenderer>();
            if(spriteRender.sprite.name == "leftSwitchDoor")
            {
                GetComponent<AudioSource>().clip = aud[5];
                GetComponent<AudioSource>().Play();
                spriteRender.sprite = sp[0];
            }
            else if(spriteRender.sprite.name == "rightSwitchDoor" || spriteRender.sprite.name == "2rightSwitchDoor")
            {
                GetComponent<AudioSource>().clip = aud[3];
                GetComponent<AudioSource>().Play();
                spriteRender.sprite = sp[1];
            }

        }

        if (SceneManager.GetActiveScene().name == "Lv1.4" || SceneManager.GetActiveScene().name == "Lv1.6"|| SceneManager.GetActiveScene().name == "Lv1.7" || SceneManager.GetActiveScene().name == "Trial1.7" || SceneManager.GetActiveScene().name == "Lv2.9")//小球进入传送门 
        {
            int portalSwitchState = GameObject.Find("PortalSwitch").GetComponent<PortalController>().state;
   
            if(portalSwitchState == 1)
            {
                if(obj.name == portal[1].name)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    gameObject.transform.position = portalPosition[2] + new Vector3(-1.5f,-portal[2].transform.localScale.y/2,0);
                    GetComponent<AudioSource>().clip = aud[2];
                    GetComponent<AudioSource>().Play();
                }
                else if(obj.name == portal[2].name)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    gameObject.transform.position = portalPosition[1] + new Vector3(1.5f, -portal[1].transform.localScale.y / 2, 0);
                    GetComponent<AudioSource>().clip = aud[2];
                    GetComponent<AudioSource>().Play();
                }
            }
            else if (portalSwitchState == 2)
            {
                if (obj.name == portal[0].name)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    gameObject.transform.position = portalPosition[2] + new Vector3(-1.5f, -portal[2].transform.localScale.y / 2, 0);
                    GetComponent<AudioSource>().clip = aud[2];
                    GetComponent<AudioSource>().Play();
                }
                else if (obj.name == portal[2].name)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    gameObject.transform.position = portalPosition[0] + new Vector3(-1.5f, -portal[0].transform.localScale.y / 2, 0);
                    GetComponent<AudioSource>().clip = aud[2];
                    GetComponent<AudioSource>().Play();
                }
            }
            else if (portalSwitchState == 3)
            {
                if (obj.name == portal[0].name)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    gameObject.transform.position = portalPosition[1] + new Vector3(1.5f, -portal[1].transform.localScale.y / 2, 0);
                    GetComponent<AudioSource>().clip = aud[2];
                    GetComponent<AudioSource>().Play();
                }
                else if (obj.name == portal[1].name)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    gameObject.transform.position = portalPosition[0] + new Vector3(-1.5f, -portal[0].transform.localScale.y / 2, 0);
                    GetComponent<AudioSource>().clip = aud[2];
                    GetComponent<AudioSource>().Play();
                }
            }
            
        }
        else if(SceneManager.GetActiveScene().name == "Lv2.8")
        {
            if(obj.name == portal[0].name)
            {
                isInPortal = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                gameObject.transform.position = portalPosition[1] + new Vector3(1.5f, -portal[1].transform.localScale.y / 2, 0);
                GetComponent<AudioSource>().clip = aud[2];
                GetComponent<AudioSource>().Play();
            }
            else if (obj.name == portal[1].name)
            {
                isInPortal = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                gameObject.transform.position = portalPosition[0] + new Vector3(-1.5f, -portal[0].transform.localScale.y / 2, 0);
                GetComponent<AudioSource>().clip = aud[2];
                GetComponent<AudioSource>().Play();
            }
        }

    }

    void OnTriggerExit2D(Collider2D obj)
    {
        string RegexStr1 = @"^IceDoor[\w\W]*";

        string RegexStr3 = @"^NoneDoor[\w\W]*";

        string RegexStr5 = @"^FireDoor[\w\W]*";

        string RegexStr = @"^rope[\w\W]*";

        if (Regex.IsMatch(obj.gameObject.name, RegexStr))
        {
           GetComponent<CircleCollider2D>().isTrigger = false;
        }

        if (Regex.IsMatch(obj.gameObject.name, RegexStr1) && attribute == 1 && SceneManager.GetActiveScene().name != "Lv1.9")//冰门
        {
                obj.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else if (Regex.IsMatch(obj.gameObject.name, RegexStr3) && attribute == 0)//无属性门
        {
                obj.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else if (Regex.IsMatch(obj.gameObject.name, RegexStr5) && attribute == 2 && SceneManager.GetActiveScene().name != "Lv1.9")//火门
        {
                obj.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }

       
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        string RegexStr1 = @"^IceDoor[\w\W]*";
        string RegexStr2 = @"^FireWall[\w\W]*";
        string RegexStr3 = @"^NoneDoor[\w\W]*";
        string RegexStr4 = @"^IcePlane[\w\W]*";
        string RegexStr5 = @"^FireDoor[\w\W]*";
        string RegexStr6 = @"^ball[\w\W]*";
        string RegexStr7 = @"^FirePlane[\w\W]*";
        string RegexStr8 = @"^rope[\w\W]*";
        string RegexStr9 = @"^BigPlayer[\w\W]*";
        string RegexStr10 = @"^SmallPlayer[\w\W]*";

        //与冰火地形碰撞
        if (Regex.IsMatch(coll.gameObject.name, RegexStr4) && attribute == 0)//获得冰属性
        {
            attribute = 1;
            GetComponent<AudioSource>().clip = aud[1];
            GetComponent<AudioSource>().Play();
        }
        else if (Regex.IsMatch(coll.gameObject.name, RegexStr2) || Regex.IsMatch(coll.gameObject.name, RegexStr7) && attribute == 0)//获得火属性
        {
            attribute = 2;
            GetComponent<AudioSource>().clip = aud[0];
            GetComponent<AudioSource>().Play();
        }
        else if (Regex.IsMatch(coll.gameObject.name, RegexStr2) && attribute == 1 || Regex.IsMatch(coll.gameObject.name, RegexStr7) && attribute == 1 || Regex.IsMatch(coll.gameObject.name, RegexStr4) && attribute == 2)
        {
            attribute = 0;//冰火相消
        }
        //与球碰撞
        else if (Regex.IsMatch(coll.gameObject.name, RegexStr6) || Regex.IsMatch(coll.gameObject.name, RegexStr10) || Regex.IsMatch(coll.gameObject.name, RegexStr9))
        {
            if (coll.gameObject.GetComponent<CollisionController>().attribute == 1 && attribute == 0)//获得冰属性
            {
                attribute = 1;
                GetComponent<AudioSource>().clip = aud[1];
                GetComponent<AudioSource>().Play();
            }
            else if (coll.gameObject.GetComponent<CollisionController>().attribute == 2 && attribute == 0)//获得火属性
            {
                attribute = 2;
                GetComponent<AudioSource>().clip = aud[0];
                GetComponent<AudioSource>().Play();
            }
            else if (coll.gameObject.GetComponent<CollisionController>().attribute == 1 && attribute == 2 || coll.gameObject.GetComponent<CollisionController>().attribute == 2 && attribute == 1)//冰火相消
            {
                attribute = 0;
                coll.gameObject.GetComponent<CollisionController>().attribute = 0;
            }

            if (GetComponent<Rigidbody2D>().velocity.y > 0.1f && transform.position.y<coll.transform.position.y)
            {
                Debug.Log("crash");
                if (Regex.IsMatch(coll.gameObject.name, RegexStr10))
                {
                    coll.gameObject.GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().velocity*25);
                    Debug.Log("smallcrash");
                }
                   
                else if(Regex.IsMatch(coll.gameObject.name, RegexStr9))
                {
                    if(GetComponent<Rigidbody2D>().velocity.y * 500<600)
                        coll.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<Rigidbody2D>().velocity.x*20,GetComponent<Rigidbody2D>().velocity.y*600));
                    else
                        coll.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<Rigidbody2D>().velocity.x * 20, 600));
                    Debug.Log("bigcrash");
                }
                    
            }
        }

        //与门碰撞
        else if (Regex.IsMatch(coll.gameObject.name, RegexStr1) && attribute == 1)//冰门
        {
            if (SceneManager.GetActiveScene().name == "Lv1.7" || SceneManager.GetActiveScene().name == "Lv1.8")
             {
                 if (Regex.IsMatch(this.gameObject.name, RegexStr9))
                        Destroy(coll.gameObject);
             }
            else if(SceneManager.GetActiveScene().name != "Lv1.9")
                coll.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                

        }
        else if (Regex.IsMatch(coll.gameObject.name, RegexStr3) && attribute == 0)//无属性门
        {

                if (SceneManager.GetActiveScene().name == "Lv1.1" || SceneManager.GetActiveScene().name == "Lv1.8")
                {
                    if (Regex.IsMatch(this.gameObject.name, RegexStr9))
                        Destroy(coll.gameObject);
                }


            else 
                coll.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            
           
        }
        else if (Regex.IsMatch(coll.gameObject.name, RegexStr5) && attribute == 2)//火门
        {
            
                if (SceneManager.GetActiveScene().name == "Lv1.1" || SceneManager.GetActiveScene().name == "Lv1.8")
                {
                    if (Regex.IsMatch(this.gameObject.name, RegexStr9))
                        Destroy(coll.gameObject);
                }
            else if (SceneManager.GetActiveScene().name != "Lv1.9")
                coll.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            
            
        }
        else if (Regex.IsMatch(coll.gameObject.name, RegexStr8))
        {
           GetComponent<CircleCollider2D>().isTrigger = true;
        }


    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if( coll.gameObject.name != "background")
            isTouchingFloor = 1;

        for(int i = 0 ; i<Trigger.Length ; i++)
        {
            if (coll.gameObject.name == Trigger[i].name)
            {
                Debug.Log("stay");
                GameObject.Find(TriggerDoor[i].name).GetComponent<BoxCollider2D>().isTrigger = true;
                Animator anim = GameObject.Find(TriggerDoor[i].name).GetComponent<Animator>();
                AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
                if(stateinfo.fullPathHash == Animator.StringToHash("Base Layer.Idle"))
                {
                    anim.SetBool("isTrigger",true);
                    anim.SetBool("notTrigger", false);
                    dooraudio = 0;
                    if (breakdooraudio == 0)
                        breakdooraudio = 1;
                }
                if(breakdooraudio == 1)
                {
                    if(coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "brickSwitch")
                    {
                        GetComponent<AudioSource>().clip = aud[4];
                        GetComponent<AudioSource>().Play();
                    }
                    else if(coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "woodSwitch")
                    {
                        GetComponent<AudioSource>().clip = aud[6];
                        GetComponent<AudioSource>().Play();
                    }
                    else if (coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "lightDoorSwitch")
                    {
                        GetComponent<AudioSource>().clip = laseraud[0];
                        GetComponent<AudioSource>().Play();
                    }
                    breakdooraudio = 2;
                }
            }

        }

    }

    void OnCollisionExit2D(Collision2D coll)
    {
      
        isTouchingFloor = 0;


        for (int i = 0; i < Trigger.Length; i++)
        {
            if (coll.gameObject.name == Trigger[i].name)
            {
                GameObject.Find(TriggerDoor[i].name).GetComponent<BoxCollider2D>().isTrigger = false;
                Animator anim = GameObject.Find(TriggerDoor[i].name).GetComponent<Animator>();
                AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
                if (stateinfo.fullPathHash == Animator.StringToHash("Base Layer.DoorBreak"))
                {
                    Debug.Log("brickdoor");
                    anim.SetBool("notTrigger", true);
                    anim.SetBool("isTrigger", false);
                    breakdooraudio = 0;
                    if (dooraudio == 0)
                        dooraudio = 1;
                    if (dooraudio == 1)
                    {
                        if (coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "brickSwitch")
                        {
                            GetComponent<AudioSource>().clip = aud[7];
                            GetComponent<AudioSource>().Play();
                        }
                        else if (coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "woodSwitch")
                        {
                            GetComponent<AudioSource>().clip = aud[8];
                            GetComponent<AudioSource>().Play();
                        }
                        else if (coll.gameObject.GetComponent<SpriteRenderer>().sprite.name == "lightDoorSwitch")
                        {
                            GetComponent<AudioSource>().clip = laseraud[1];
                            GetComponent<AudioSource>().Play();
                        }
                        dooraudio = 2;
                    }
                }

            }

        }

    }

    void OnBecameInvisible()
    {
        failWindowShow = true;
    }

    void OnGUI()
    {
        if(failWindowShow)
            GUI.Window(1, new Rect(0, 0, Screen.width, Screen.height), FailWindow, "Fail");
    }
    void FailWindow(int WindowID)
    {
        string levelName = SceneManager.GetActiveScene().name;
        Time.timeScale = 0;
        //Debug.Log("pause");
        if (GUI.Button(new Rect(Screen.width / 3, Screen.height * 3 / 5, Screen.width * 2 / 5, Screen.height / 5), "back"))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Chapter");
         
        }
        else if (GUI.Button(new Rect(Screen.width * 3 / 8, Screen.height * 0.35f, Screen.width * 0.31f, Screen.height / 5), "restart"))
        {
            if (!MainMenuButton.mode)
                SceneManager.LoadScene(levelName);
        }

    }


}
