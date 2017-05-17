using UnityEngine;
using System.Collections;

public class Vector : MonoBehaviour {
    public Vector2 origin = new Vector2(0, 0);
    public Vector2 v;

    public float speed = 1f;
    public Vector3 desiredPosition= Vector3.zero;

    private void Start()
    {
    }

    void Update ()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, desiredPosition, Time.deltaTime * speed);
	}
}
