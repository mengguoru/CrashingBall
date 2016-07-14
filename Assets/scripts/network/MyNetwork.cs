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
        string levelName = SceneManager.GetActiveScene().name;

        if (levelName == "Lv1.1" || levelName == "Lv1.2" || levelName == "Lv1.5" || levelName == "Lv1.8")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[0];
            }
            else
                playerPrefab = BigPlayer[0];
        }
        else if (levelName == "Lv1.3")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[1];
            }
            else
                playerPrefab = BigPlayer[1];
        }
        else if (levelName == "Lv1.4")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[2];
            }
            else
                playerPrefab = BigPlayer[2];
        }
        else if (levelName == "Lv1.6")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[3];
            }
            else
                playerPrefab = BigPlayer[3];
        }
        else if (levelName == "Lv1.7")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[4];
            }
            else
                playerPrefab = BigPlayer[4];
        }
        else if (levelName == "Lv1.9")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[5];
            }
            else
                playerPrefab = BigPlayer[5];
        }
        else if (levelName == "Lv2.1" || levelName == "Lv2.2" || levelName == "Lv2.3" || levelName == "Lv2.5" || levelName == "Lv2.7")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[6];
            }
            else
                playerPrefab = BigPlayer[6];
        }
        else if (levelName == "Lv2.4")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[7];
            }
            else
                playerPrefab = BigPlayer[7];
        }
        else if (levelName == "Lv2.6")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[8];
            }
            else
                playerPrefab = BigPlayer[8];
        }
        else if (levelName == "Lv2.8")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[9];
            }
            else
                playerPrefab = BigPlayer[9];
        }
        else if (levelName == "Lv2.9")
        {
            if (singleton.numPlayers == 1)
            {
                playerPrefab = SmallPlayer[10];
            }
            else
                playerPrefab = BigPlayer[10];
        }
        //Debug.Log(network.isNetworkActive);
        // Debug.Log(singleton.numPlayers);

    }

    public void StartupHost()
    {
        //singleton.onlineScene = ButtonController.conLevelName;
        SetPort();
        singleton.StartHost();
        Debug.Log("host");
    }

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
        int[] allScene = { 7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24 }; //6,7,8,9,10,11,12,13,24,25,26,27,28,29,30,31,32};
        int id = Array.IndexOf(allScene, level);
        if (level == 2)
            SetupConnectionSceneButtons();
        else if(level == 3)
        {
            singleton.StopHost();
            singleton.onlineScene = GameController.nextLevelName;
        }
        else if(level == 5)
        {
            SetupChapter1Buttons();
        }
        else if(level == 6)
        {
            SetupChapter2Buttons();
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
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);

       // GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);

        //GameObject.Find("ButtonGetIP").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonGetIP").GetComponent<Button>().onClick.AddListener(GetUserIP);

        //GameObject.Find("ButtonChooseLevel").GetComponent<Button>().onClick.AddListener(OnLevelChooseBtnClick);
    }

    void SetupChapter1Buttons()
    {
        GameObject.Find("Lv1.1").GetComponent<Button>().onClick.AddListener(OnC1L1BtnClick);

        GameObject.Find("Lv1.2").GetComponent<Button>().onClick.AddListener(OnC1L2BtnClick);

        GameObject.Find("Lv1.3").GetComponent<Button>().onClick.AddListener(OnC1L3BtnClick);

        GameObject.Find("Lv1.4").GetComponent<Button>().onClick.AddListener(OnC1L4BtnClick);

        GameObject.Find("Lv1.5").GetComponent<Button>().onClick.AddListener(OnC1L5BtnClick);

        GameObject.Find("Lv1.6").GetComponent<Button>().onClick.AddListener(OnC1L6BtnClick);

        GameObject.Find("Lv1.7").GetComponent<Button>().onClick.AddListener(OnC1L7BtnClick);

        GameObject.Find("Lv1.8").GetComponent<Button>().onClick.AddListener(OnC1L8BtnClick);

        GameObject.Find("Lv1.9").GetComponent<Button>().onClick.AddListener(OnC1L9BtnClick);
    }

    void SetupChapter2Buttons()
    {
        GameObject.Find("Lv2.1").GetComponent<Button>().onClick.AddListener(OnC2L1BtnClick);

        GameObject.Find("Lv2.2").GetComponent<Button>().onClick.AddListener(OnC2L2BtnClick);

        GameObject.Find("Lv2.3").GetComponent<Button>().onClick.AddListener(OnC2L3BtnClick);

        GameObject.Find("Lv2.4").GetComponent<Button>().onClick.AddListener(OnC2L4BtnClick);

        GameObject.Find("Lv2.5").GetComponent<Button>().onClick.AddListener(OnC2L5BtnClick);

        GameObject.Find("Lv2.6").GetComponent<Button>().onClick.AddListener(OnC2L6BtnClick);

        GameObject.Find("Lv2.7").GetComponent<Button>().onClick.AddListener(OnC2L7BtnClick);

        GameObject.Find("Lv2.8").GetComponent<Button>().onClick.AddListener(OnC2L8BtnClick);

        GameObject.Find("Lv2.9").GetComponent<Button>().onClick.AddListener(OnC2L9BtnClick);
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
        SceneManager.LoadScene("ConChapter1");
    }

    /*public void OnConLevelChooseBtnClick(GameObject obj)
    {
        string levelName = obj.name;
        int chapter = levelName[2] - 48;//1 2 3
        int level = levelName[4] - 48;//1 2 3 ...9
        int index = (chapter - 1) * 9 + level - 1;
        Debug.Log(singleton.numPlayers);
        // if (levelStatus[index] == 0)
        // {
        singleton.onlineScene = obj.name;
        if (levelName == "Lv1.1" || levelName == "Lv1.2" || levelName == "Lv1.5" || levelName == "Lv1.8")
        {
            playerPrefab = BigPlayer[0];
        }
        else if (levelName == "Lv1.3")
        {
            playerPrefab = BigPlayer[1];
        }

        SceneManager.LoadScene("NetworkConnection");
        // }
        // else
        //Debug.Log("还没通关哟");
    }*/

    public void OnC1L1BtnClick()
    {
        singleton.onlineScene = "Lv1.1";
        singleton.playerPrefab = BigPlayer[0];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC1L2BtnClick()
    {
        singleton.onlineScene = "Lv1.2";
        singleton.playerPrefab = BigPlayer[0];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC1L3BtnClick()
    {
        singleton.onlineScene = "Lv1.3";
        singleton.playerPrefab = BigPlayer[1];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC1L4BtnClick()
    {
        singleton.onlineScene = "Lv1.4";
        singleton.playerPrefab = BigPlayer[2];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC1L5BtnClick()
    {
        singleton.onlineScene = "Lv1.5";
        singleton.playerPrefab = BigPlayer[0];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC1L6BtnClick()
    {
        singleton.onlineScene = "Lv1.6";
        singleton.playerPrefab = BigPlayer[3];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC1L7BtnClick()
    {
        singleton.onlineScene = "Lv1.7";
        singleton.playerPrefab = BigPlayer[4];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC1L8BtnClick()
    {
        singleton.onlineScene = "Lv1.8";
        singleton.playerPrefab = BigPlayer[0];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC1L9BtnClick()
    {
        singleton.onlineScene = "Lv1.9";
        singleton.playerPrefab = BigPlayer[5];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC2L1BtnClick()
    {
        singleton.onlineScene = "Lv2.1";
        singleton.playerPrefab = BigPlayer[6];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC2L2BtnClick()
    {
        singleton.onlineScene = "Lv2.2";
        singleton.playerPrefab = BigPlayer[6];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC2L3BtnClick()
    {
        singleton.onlineScene = "Lv2.3";
        singleton.playerPrefab = BigPlayer[6];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC2L4BtnClick()
    {
        singleton.onlineScene = "Lv2.4";
        singleton.playerPrefab = BigPlayer[7];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC2L5BtnClick()
    {
        singleton.onlineScene = "Lv2.5";
        singleton.playerPrefab = BigPlayer[6];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC2L6BtnClick()
    {
        singleton.onlineScene = "Lv2.6";
        singleton.playerPrefab = BigPlayer[8];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC2L7BtnClick()
    {
        singleton.onlineScene = "Lv2.7";
        singleton.playerPrefab = BigPlayer[6];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC2L8BtnClick()
    {
        singleton.onlineScene = "Lv2.8";
        singleton.playerPrefab = BigPlayer[9];
        SceneManager.LoadScene("NetworkConnection");
    }

    public void OnC2L9BtnClick()
    {
        singleton.onlineScene = "Lv2.9";
        singleton.playerPrefab = BigPlayer[10];
        SceneManager.LoadScene("NetworkConnection");
    }

}
