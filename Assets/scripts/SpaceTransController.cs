using UnityEngine;
using System.Collections;

public class SpaceTransController : MonoBehaviour {

	void Start () {
        Invoke("Turnover", 5.0f);
    }
	
	void Update () {
	
	}


    void Turnover()
    {
        GameObject bg2 = GameObject.Find("bg2");
        GameObject bg4 = GameObject.Find("bg4");
        Vector3 bg2Pos = bg2.transform.position;
        Vector3 bg4pos = bg4.transform.position;
        bg2.transform.position = bg4pos;
        bg4.transform.position = bg2Pos;
        bg2.transform.Rotate(new Vector3(0, 0, 180));
        bg4.transform.Rotate(new Vector3(0, 0, 180));
    }
}
