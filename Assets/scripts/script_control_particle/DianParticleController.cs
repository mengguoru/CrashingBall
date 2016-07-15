using UnityEngine;
using System.Collections;

public class DianParticleController : MonoBehaviour {

    ParticleSystem _particleSystem;
    public GameObject parent;

    // Use this for initialization
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!MagnetismController.trick)
        {
            _particleSystem.Stop();
            //Debug.Log("N stop");
        }
        else
        {
            _particleSystem.Play();
            //Debug.Log("N start");

        }

    }
}
