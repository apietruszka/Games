using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform obj;
    public Vector3 distortion;


    public float distFromCam = 10f;
    Vector3 point;
    public float str = 0.01f;
    public float lerpRate = 1f;
    


    /*void Update()
    {
        point = obj.transform.position + obj.transform.forward * distFromCam;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Debug.Log("X: " + mouseX + "   Y: " + mouseY);


        point += (-Input.mousePosition.x * obj.transform.right * str + -Input.mousePosition.y * obj.transform.up * str);

        Quaternion targetRotation = Quaternion.LookRotation(point - obj.position);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * lerpRate);
    }*/
    
    void FixedUpdate()
    {
        Vector3 distortionDirection = -(obj.transform.forward+obj.transform.right).normalized;
        //Debug.Log(distortionDirection.sqrMagnitude);//ok
        this.transform.position = Vector3.Lerp(this.transform.position, obj.transform.position + distortion.sqrMagnitude*distortionDirection+distortion, Time.fixedDeltaTime * lerpRate);
        this.transform.LookAt(obj);
    }
}