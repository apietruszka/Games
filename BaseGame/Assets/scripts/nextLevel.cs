using UnityEngine;
using System.Collections;

public class nextLevel : MonoBehaviour
{
    public Camera cam;
    public Transform door;
    bool active = false;
    bool rotationComplete = false;
    float speed2 = 0f;
    float speed = 0f;
    private void Update()
    {
        if(active)
        {
            
            Quaternion targetRot = Quaternion.LookRotation(door.position - cam.transform.position);

            Vector3 camRotation = cam.transform.eulerAngles;
            Vector3 targetRotation = targetRot.eulerAngles;
            if (Vector3.Distance(camRotation,targetRotation)<0.1f)
            {
                cam.transform.LookAt(door);
                rotationComplete = true;
            }
            else
            {
                speed += Time.deltaTime;
                cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, targetRot, Time.deltaTime * speed);
            }


            if(rotationComplete)
            {
                speed2 += Time.deltaTime;
                Vector3 dir = (door.position - cam.transform.position);
                cam.transform.position += 0.01f*dir*speed2;

                if((cam.transform.position-door.position).sqrMagnitude<1.5f)
                {
                    Debug.Log("new scene");
                }
            }
        }
    }


    private void OnMouseDown()
    {
        active = true;
    }
}
