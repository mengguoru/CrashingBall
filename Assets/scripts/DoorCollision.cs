using UnityEngine;
using System.Collections;

public class DoorCollision : MonoBehaviour {

    // Use this for initialization
    public int num;
    void Start () {
        num = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.name == "door1")
        {
            num = 1;
        }
        else if(obj.gameObject.name == "door2")
        {
            num = 2;
        }
        else if(obj.gameObject.name == "door3")
        {
            num = 3;
        }
        Debug.Log(num);
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        num = 0;
    }
}
