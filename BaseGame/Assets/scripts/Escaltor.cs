using UnityEngine;
using System.Collections;

public class Escaltor : MonoBehaviour
{
    public Vector3[] destination;
    Vector3 dir;
    public int currentDestination;
    bool active = false;
    public float currentSpeed=5f;
    public float speed = 50f;
    public float acceleration = 1f;
	
    void Start()
    {
        dir = (destination[currentDestination] - transform.position).normalized;
    }

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (active)
                active = false;
            else
                active = true;
        }

        if (active&&currentSpeed<speed)
        {
            currentSpeed += Time.deltaTime * acceleration;
            if (currentSpeed > speed)
                currentSpeed = speed;
            
        }
        else if (!active&&currentSpeed > 0)
        {
            currentSpeed -= Time.deltaTime * acceleration;
            if (currentSpeed < 0)
                currentSpeed = 0;
        }

            

            if(currentSpeed>0)
                transform.position += dir * currentSpeed / 10f * Time.deltaTime;
            if ((transform.position - destination[currentDestination]).sqrMagnitude < 0.01f)
            {
                currentDestination++;
                if (currentDestination == 8)
                    currentDestination = 0;
                dir = (destination[currentDestination] - transform.position).normalized;
            }
        }
        
	
}
