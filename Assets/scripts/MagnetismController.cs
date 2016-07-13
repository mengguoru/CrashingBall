using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class MagnetismController : MonoBehaviour {

    public int magnetism;//0无磁性  1N 2S
    public GameObject[] ball;
    public Rigidbody2D rb;
    public static bool trick;
    List<Vector2> disVector;
    List<Vector2> forceVector;
    public AudioClip[] aud;
    void Start () {
        magnetism = 0;
        rb = GetComponent<Rigidbody2D>();
        trick = false;
        disVector = new List<Vector2>();
        forceVector = new List<Vector2>();
        for(int i = 0; i < ball.Length; i++)
        {
            disVector.Add(new Vector2(0, 0));
            forceVector.Add(new Vector2(0, 0));
        }
	}
	
	
	void Update () {
        if (magnetism != 0)
        {
            for (int i = 0; i < ball.Length; i++)
            {
                if(ball[i].gameObject.GetComponent<MagnetismController>().magnetism != 0)
                {
                    disVector[i] = ball[i].gameObject.transform.position - this.gameObject.transform.position;
                    forceVector[i] = disVector[i].normalized * 30 / disVector[i].magnitude;
                    if (ball[i].gameObject.GetComponent<MagnetismController>().magnetism != magnetism)
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

        if (!trick)
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
            else if (coll.gameObject.name == "BigPlayer" || coll.gameObject.name == "SmallPlayer" || Regex.IsMatch(coll.gameObject.name, RegexStr3))
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
                    if ((coll.gameObject.name == "BigPlayer" && this.gameObject.name == "SmallPlayer") || (coll.gameObject.name == "SmallPlayer" && this.gameObject.name == "BigPlayer"))
                    {
                        trick = true;
                        GetComponent<AudioSource>().clip = aud[3];
                        GetComponent<AudioSource>().Play();
                    }

                }
            }
        }
        
    }
}
