using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {

    public GameObject[] slider;

   /* void Awake() {
        if(volumn.Length == 0)
        {
            volumn = new float[2] { 0.5f, 0.5f };
            Debug.Log(volumn[0]);
        }
    }*/

    void Start()
    {
        for (int i = 0; i < slider.Length; i++)
        {
            slider[i].GetComponent<Slider>().value = VolumnController.volumn[i];
        }
    }

    void Update () {
        for (int i = 0; i < slider.Length; i++)
        {
            VolumnController.volumn[i] = slider[i].GetComponent<Slider>().value;
        }
	}
}
