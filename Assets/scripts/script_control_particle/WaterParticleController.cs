using UnityEngine;
using System.Collections;

public class WaterParticleController : MonoBehaviour {

    ParticleSystem _particleSystem;
    float speedx;
    float speed_max;
    public GameObject parent;

    // Use this for initialization
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        speedx = 0;
        speed_max = 2;
    }

    void Update()
    {
        if (parent.GetComponent<CollisionController>().attribute == 0 || parent.GetComponent<CollisionController>().attribute == 2)
        {
            _particleSystem.Stop();
            //Debug.Log("water stop");
        }
        else if (parent.GetComponent<CollisionController>().attribute == 1)
        {
            if (parent.GetComponent<Rigidbody2D>().velocity.x < 0)
                speedx = speed_max;
            else if ((parent.GetComponent<Rigidbody2D>().velocity.x > 0))
                speedx = -speed_max;
            else
                speedx = 0;

            _particleSystem.Play();
            //Debug.Log("water start");

        }

    }
    // Update is called once per frame
    void LateUpdate()
    {
        var vel = _particleSystem.velocityOverLifetime;
        vel.x = new ParticleSystem.MinMaxCurve(speedx);

    }
}
