using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.Networking;

public class MagnetismController : NetworkBehaviour {

    public int magnetism;//0无磁性  1N 2S
    public GameObject[] ball;
    public Rigidbody2D rb;
    public static bool trick;
    [SyncVar]
    public bool contrick;

    List<Vector2> disVector;
    List<Vector2> forceVector;
    public AudioClip[] aud;
    void Start () {
        magnetism = 0;
        rb = GetComponent<Rigidbody2D>();
        if (!MainMenuButton.mode)
            trick = false;
        else
            contrick = false;
        disVector = new List<Vector2>();
        forceVector = new List<Vector2>();
        for(int i = 0; i < ball.Length; i++)
        {
            disVector.Add(new Vector2(0, 0));
            forceVector.Add(new Vector2(0, 0));
        }
	}
	
	
	void Update () {
        string RegexStr1 = @"^BigPlayer[\w\W]*";
        string RegexStr2 = @"^SmallPlayer[\w\W]*";
        if (!isLocalPlayer && MainMenuButton.mode)
        {
            if (Regex.IsMatch(gameObject.name, RegexStr1))
            {
                gameObject.GetComponent<BigPlayerController>().canControl = false;
            }
            else if(Regex.IsMatch(gameObject.name, RegexStr2))
            {
                gameObject.GetComponent<SmallPlayerController>().canControl = false;
            }

        }
        if (magnetism != 0)
        {
            for (int i = 0; i < ball.Length; i++)
            {
                Debug.Log(ball[i].name);
                if (GameObject.Find(ball[i].name).GetComponent<MagnetismController>().magnetism != 0)
                {
                    disVector[i] = GameObject.Find(ball[i].name).transform.position - this.gameObject.transform.position;
                    forceVector[i] = disVector[i].normalized * 30 / disVector[i].magnitude;
                    if (GameObject.Find(ball[i].name).GetComponent<MagnetismController>().magnetism != magnetism)
                        rb.AddForce(forceVector[i]);
                    else
                        rb.AddForce(-0.2f * forceVector[i]);
                }
            }
        }

	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        string RegexStr1 = @"^North[\w\W]*";
        string RegexStr2 = @"^South[\w\W]*";
        string RegexStr3 = @"^ball[\w\W]*";
        string RegexStr4 = @"^BigPlayer[\w\W]*";
        string RegexStr5 = @"^SmallPlayer[\w\W]*";


        if ((!trick && !MainMenuButton.mode) || (!contrick && MainMenuButton.mode))
        {
            //与磁极碰撞
            if (Regex.IsMatch(coll.gameObject.name, RegexStr1) && magnetism == 0)
            {
                magnetism = 1;
                GetComponent<AudioSource>().clip = aud[0];
                GetComponent<AudioSource>().Play();
            }
            else if (Regex.IsMatch(coll.gameObject.name, RegexStr2) && magnetism == 0)
            {
                magnetism = 2;
                GetComponent<AudioSource>().clip = aud[1];
                GetComponent<AudioSource>().Play();
            }
            else if ((Regex.IsMatch(coll.gameObject.name, RegexStr1) && magnetism == 2) || (Regex.IsMatch(coll.gameObject.name, RegexStr2) && magnetism == 1))
            {
                magnetism = 0;
                GetComponent<AudioSource>().clip = aud[2];
                GetComponent<AudioSource>().Play();
            }
            //球之间碰撞
            else if (Regex.IsMatch(coll.gameObject.name, RegexStr4) || Regex.IsMatch(coll.gameObject.name, RegexStr5) || Regex.IsMatch(coll.gameObject.name, RegexStr3))
            {
                if (coll.gameObject.GetComponent<MagnetismController>().magnetism == 1 && magnetism == 0)
                {
                    magnetism = 1;
                    GetComponent<AudioSource>().clip = aud[0];
                    GetComponent<AudioSource>().Play();
                }
                else if (coll.gameObject.GetComponent<MagnetismController>().magnetism == 2 && magnetism == 0)
                {
                    magnetism = 2;
                    GetComponent<AudioSource>().clip = aud[1];
                    GetComponent<AudioSource>().Play();
                }
                else if ((coll.gameObject.GetComponent<MagnetismController>().magnetism == 1 && magnetism == 2) || (coll.gameObject.GetComponent<MagnetismController>().magnetism == 2 && magnetism == 1))
                {
                    magnetism = 0;
                    Debug.Log("000");
                    coll.gameObject.GetComponent<MagnetismController>().magnetism = 0;
                    if ((Regex.IsMatch(coll.gameObject.name, RegexStr4) && Regex.IsMatch(this.gameObject.name, RegexStr5)) || (Regex.IsMatch(coll.gameObject.name, RegexStr5) && Regex.IsMatch(this.gameObject.name, RegexStr4)))
                    {
                        if (!MainMenuButton.mode)
                            trick = true;
                        else
                            contrick = true;
                        GetComponent<AudioSource>().clip = aud[3];
                        GetComponent<AudioSource>().Play();
                    }

                }
            }
        }
        
    }
}
