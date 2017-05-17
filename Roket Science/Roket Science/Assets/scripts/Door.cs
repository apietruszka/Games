using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public bool open = true;
    public Room r1, r2;//reference to rooms that it is between
    public float oxygenFrom1To2 = 0;
    

    void OnMouseUp()
    {
        if(open)
        {
            transform.position += Vector3.up * 3f;
            open = false;
        }
        else
        {
            transform.position -= Vector3.up * 3f;
            open = true;
        }
    }
}
