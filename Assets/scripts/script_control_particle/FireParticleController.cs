using UnityEngine;
using System.Collections;

public class FireParticleController : MonoBehaviour {

    ParticleSystem _particleSystem;
    float speedx;
    float speedy;
    float speed_max;
    public GameObject parent;

    // Use this for initialization
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        speedx = 0;
        speedy = 0;
        speed_max = 2;
    }

    void Update()
    {
        if (parent.GetComponent<CollisionController>().attribute == 0 || parent.GetComponent<CollisionController>().attribute == 1)
        {
            _particleSystem.Stop();
            //Debug.Log("fire stop");
        }
        else if(parent.GetComponent<CollisionController>().attribute == 2)
        {
            if (parent.GetComponent<Rigidbody2D>().velocity.x < 0)
                speedx = speed_max;
            else if (parent.GetComponent<Rigidbody2D>().velocity.x > 0)
                speedx = -speed_max;
            else
                speedx = 0;

            if (parent.GetComponent<Rigidbody2D>().velocity.y < 0)
                 speedy = speed_max;
            else if ((parent.GetComponent<Rigidbody2D>().velocity.y > 0))
                  speedy = -speed_max;
            else
                 speedy = 0;


            _particleSystem.Play();
            //Debug.Log("fire start");

        }

    }
    // Update is called once per frame
    void LateUpdate()
    {
        var vel = _particleSystem.velocityOverLifetime;
        vel.x = new ParticleSystem.MinMaxCurve(speedx);
        vel.z = new ParticleSystem.MinMaxCurve(speedy);

    }
}
