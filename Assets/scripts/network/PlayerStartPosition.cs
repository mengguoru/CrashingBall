using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class PlayerStartPosition : NetworkBehaviour {

    private NetworkStartPosition[] spawnPoints;

    // Use this for initialization
    void Start () {
        string player1 = @"^BigPlayer[\w\W]*";
        string player2 = @"^SmallPlayer[\w\W]*";
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            if (Regex.IsMatch(gameObject.name, player1))
            {
                transform.position = spawnPoints[0].transform.position;
            }
            else if(Regex.IsMatch(gameObject.name, player2))
            {
                transform.position = spawnPoints[1].transform.position;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
