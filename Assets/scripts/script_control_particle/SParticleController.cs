using UnityEngine;
using System.Collections;

public class SParticleController : MonoBehaviour {


    ParticleSystem _particleSystem;
    public GameObject parent;

    // Use this for initialization
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (parent.GetComponent<MagnetismController>().magnetism == 0 || parent.GetComponent<MagnetismController>().magnetism == 1)
        {
            _particleSystem.Stop();
            Debug.Log("S stop");
        }
        else if (parent.GetComponent<MagnetismController>().magnetism == 2)
        {
            _particleSystem.Play();
            Debug.Log("S start");

        }

    }
}
