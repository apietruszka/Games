using UnityEngine;
using System.Collections;

public class hexTile : MonoBehaviour {

    public Transform hex;
    public bool active = false;
    Vector3 inactivePosition;
    Vector3 delta;
    Vector3 activePosition;
    float currentSinPosition = 0f;
    float power =1f;
    float maxHeight = 5;
    float speed = 1f;

    private void Start()
    {
        inactivePosition = hex.transform.position;
        activePosition = inactivePosition + Vector3.up * maxHeight*power;
        delta = activePosition - inactivePosition;
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
        hex.position = Vector3.Lerp(inactivePosition, activePosition, Mathf.Sin(currentSinPosition));
    }
    public void goDown()
    {
        
        float velocity = speed*0.5f / delta.sqrMagnitude;
        if (currentSinPosition <= 0f)
        {
            currentSinPosition = 0f;
        }
        else
        {
            currentSinPosition -= velocity;
        }
        hex.position = Vector3.Lerp(inactivePosition, activePosition, Mathf.Sin(currentSinPosition));
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
