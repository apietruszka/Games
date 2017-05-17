using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour
{
    public Animation opening;
    float timeSinceLastAnimation = 0;
    bool isOpen = false;
    public ParticleSystem p1;
    public ParticleSystem p2;


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isOpen)
                close();
            else
                open();
        }

        timeSinceLastAnimation += Time.deltaTime;


    }

    public void open()
    {
        if (timeSinceLastAnimation >= 1.5f&&!isOpen)
        {
            isOpen = true;
            timeSinceLastAnimation = 0;
            opening.Play("DoorOpening");
            p1.Play();
            p2.Play();
        }
    }
    public void close()
    {
        if (timeSinceLastAnimation >= 1.5f&&isOpen)
        {
            isOpen = false;
            timeSinceLastAnimation = 0;
            opening.Play("DoorClosing");

            p1.Stop();
            p2.Stop();
        }
    }
    
}
