using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class SmallPlayerStartPosition : NetworkBehaviour {

    //private NetworkStartPosition[] spawnPoints;

    // Use this for initialization
    void Start () {
        //spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        if(isServer)
            RpcFindPosition();
       
	}
	
    [ClientRpc]
    void RpcFindPosition()
    {
        if (!isLocalPlayer)
            return;
        else
        {
            //spawnPoints = FindObjectsOfType<NetworkStartPosition>();
           // string player1 = @"^BigPlayer[\w\W]*";
           // string player2 = @"^SmallPlayer[\w\W]*";
          
           // if (Regex.IsMatch(this.gameObject.name, player1))
          //  {
                
                transform.position = GameObject.Find("StartPosition2").transform.position;
          //  }
          //  else if (Regex.IsMatch(this.gameObject.name, player2))
          //  {
          //      transform.position = GameObject.Find("StartPosition2").transform.position;
           // }
           // Debug.Log(this.gameObject.name);
        }

    }
	
}
