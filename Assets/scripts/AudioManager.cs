﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance = null;
    public AudioSource m_Aud;
    public AudioClip bgm;

	// Use this for initialization
	void Start () {
        //singleton pattern
        if (null == instance)
        {
            instance = this;
        }else if(this != instance)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
       

        m_Aud = this.GetComponent<AudioSource>();
        m_Aud.clip = bgm;

        m_Aud.Play();
	}
	
	// Update is called once per frame
	void Update () {
        string RegexStr = @"^Lv[\w\W]*";
        if (Regex.IsMatch(SceneManager.GetActiveScene().name, RegexStr))
        {
            this.gameObject.SetActive(false);
        }
    }
}