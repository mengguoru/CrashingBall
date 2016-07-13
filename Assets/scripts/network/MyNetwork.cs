using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System;

public class MyNetwork : NetworkManager {
    public GameObject[] SmallPlayer;
    public GameObject[] BigPlayer;
    private NetworkStartPosition[] spawnPoints;

    // NetworkManager network;

    // Use this for initialization
    void Start () {
        //GameController.nextLevelName = "Lv1.1";
    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(BigPlayer.GetComponent<NetworkStartPosition>().transform.position);
        
        //Debug.Log(network.isNetworkActive);
    }

    public void OnHostBtnClick()
    {
        SceneManager.LoadScene("ConChapter1");
    }


   /* public void StartupHost()
    {
        //singleton.onlineScene = GameController.nextLevelName;
        SetPort();
        singleton.StartHost();
        Debug.Log("host");
    }*/

    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        singleton.StartClient();
    }

    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        singleton.networkPort = 7777;
    }

    void OnLevelWasLoaded(int level)
    {
        int[] allScene = { 5 ,6,7}; //6,7,8,9,10,11,12,13,24,25,26,27,28,29,30,31,32};
        int id = Array.IndexOf(allScene, level);
        if (level == 2)
            SetupConnectionSceneButtons();
        else if(level == 3)
        {
            singleton.StopHost();
            //singleton.onlineScene = GameController.nextLevelName;
        }
        if(id != -1)
        {
            SetupOtherSceneButtons();
            Debug.Log("isinlevel");
            GameObject.Find("BigPlayer").gameObject.SetActive(false);
            GameObject.Find("SmallPlayer").gameObject.SetActive(false);
        }
    }
    
    void SetupConnectionSceneButtons()
    {
        //GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(OnHostBtnClick);

        //GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);

        GameObject.Find("ButtonGetIP").GetComponent<Button>().onClick.AddListener(GetUserIP);
    }

    void SetupOtherSceneButtons()
    {
        //GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(singleton.StopHost);
    }

    public void GetUserIP()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        GameObject.Find("IP").GetComponent<Text>().text = localIP;

    }


    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("ModeChoose");
    }

}
