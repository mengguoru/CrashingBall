using UnityEngine;
using System.Collections;

public class NParticleController : MonoBehaviour {


    ParticleSystem _particleSystem;
    public GameObject parent;

    // Use this for initialization
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (parent.GetComponent<MagnetismController>().magnetism == 0 || parent.GetComponent<MagnetismController>().magnetism == 2)
        {
            _particleSystem.Stop();
            //Debug.Log("N stop");
        }
        else if (parent.GetComponent<MagnetismController>().magnetism == 1)
        {
            _particleSystem.Play();
            //Debug.Log("N start");

        }

    }
}
