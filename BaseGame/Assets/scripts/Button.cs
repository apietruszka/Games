using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    bool active = false;

    Vector3 plateStartPosition;

    public Transform plate;
    public DoorControl door;

    void Start()
    {
        plateStartPosition = plate.position;
    }

    void Update()
    {
        Vector3 plateNewPosition;

        if (active)
        {
            plateNewPosition = plateStartPosition - Vector3.up * 0.15f;
            door.open();
        }
        else
        {
            plateNewPosition = plateStartPosition;
            door.close();
        }

        plate.position = Vector3.Lerp(plate.position, plateNewPosition, Time.deltaTime * 4f);
    }

    void FixedUpdate()
    {
        active = false;
    }

    void OnTriggerStay(Collider collision)
    {
        if(collision.name!="button1"&&collision.name!="button2"&&collision.name!="Floor")
        active = true;
    }
}
