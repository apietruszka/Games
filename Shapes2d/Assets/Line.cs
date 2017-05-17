using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {
    public bool I = true;
    public float normalDist = 0;
    float aI = 0;

    public float lineA = 0;

    public Vector3 desiredPosition;
    public Vector2 desiredRotation;

    public float positionSpeed = 1f;
    public float turnSpeed = 1f;

    private void Start()
    {
        desiredPosition = this.transform.position;
        desiredRotation = this.transform.rotation.eulerAngles;
    }

    void Update ()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, desiredPosition, Time.deltaTime * positionSpeed);
        
        
        Quaternion destRot = Quaternion.Euler(0,0,Mathf.Atan(lineA)/(Mathf.PI*2f)*360f+90f);// 90 - odchylenie fazy ( linie defaultowo sa w pionie a nie na poczatku pierwszej cwiartki)
        //Debug.Log(destRot.eulerAngles);
        if (destRot.eulerAngles.z<0)//&&transform.rotation.eulerAngles.z>0)
        {
            this.transform.rotation = destRot;
        }
        else
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, destRot, Time.deltaTime * turnSpeed);
        //Debug.Log(lineA);
    }
}
