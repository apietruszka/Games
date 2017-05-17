using UnityEngine;
using System.Collections;

public class alternativeHexTile : MonoBehaviour {

    public Transform hex;
    public bool active = false;
    public float speed = 1f;

    Vector3 delta;
    Vector3 inactivePosition;
    Vector3 currentPosition;
    Vector3 desiredPosition;
    float maxHeight = 5f;
    float currentSinPosition = 0f;

    private void Start()
    {
        inactivePosition = hex.transform.position;
        delta = Vector3.up*maxHeight;
    }


    private void Update()
    {
        currentPosition = transform.position;
        if(desiredPosition.y>currentPosition.y)
        {
            goUp();
        }
        else if(desiredPosition.y<currentPosition.y)
        {
            goDown();
        }
    }

    public void setDesiredPosition(float y)
    {
        desiredPosition = inactivePosition + Vector3.up * maxHeight * y;
    }


    public void goUp()
    {
        float velocity = speed / delta.sqrMagnitude;
        if (currentSinPosition >= Mathf.PI / 2f)
        {
            currentSinPosition = Mathf.PI / 2f;
        }
        else
        {
            currentSinPosition += velocity;
        }
    }
    public void goDown()
    {

        float velocity = speed * 0.5f / delta.sqrMagnitude;
        if (currentSinPosition <= 0f)
        {
            currentSinPosition = 0f;
        }
        else
        {
            currentSinPosition -= velocity;
        }
        hex.position -= Vector3.up * velocity;
    }
    public void OnMouseOver()
    {
        active = true;
    }
    private void FixedUpdate()
    {
        active = false;
    }


}