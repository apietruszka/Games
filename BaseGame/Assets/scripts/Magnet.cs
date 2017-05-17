 using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {

    public ParticleSystem p1;
    public ParticleSystem p2;
    public Animator a;


    public bool active = true;
    public bool upDirection = true;
    public float strength;

    Vector3 dir;
    
	
	void Update ()
    {
	    //check for user interaction for turning Magnet on/off.
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (active)
                active = false;
            else
                active = true;
        }

        else if(Input.GetKey(KeyCode.W))
        {
            strength++;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            strength--;
        }
        if (active)
        {
            p1.enableEmission = true;
            p2.enableEmission = true;
            a.speed = 1f+strength/10f;
            p1.emissionRate = 100f+strength * 30f;
            p2.emissionRate = 100f+strength * 30f;

        }
        else
        {
            p1.enableEmission = false;
            p2.enableEmission = false;
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (active)
        {
            dir = (-collision.transform.position + transform.position).normalized;
            float dist = (-collision.transform.position + transform.position).sqrMagnitude;
            GameObject thing = collision.gameObject;
            Rigidbody rigidbody = thing.GetComponent<Rigidbody>();
            Vector3 velocity = rigidbody.velocity;
            if (upDirection)//TODO
            {
                //velocity.y += 0.60f;
                //collision.transform.position += dir/(5f+(dist*dist));
                velocity += dir *strength/ (0.2f + dist);
            }
            else
            {
                //velocity.y -= 0.60f;
            }

            rigidbody.velocity = velocity;
        }
    }

}
