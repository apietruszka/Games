using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTrainManager : MonoBehaviour
{
    public float arrivalFrequency = 60f;//seconds
    public Transform pathStart;
    public Transform deliverySpot;
    public bool isOnMap = false;//so we don't create 2nd train even if arrivalFrequency is very low
    public GameObject deliveryTrainPrefab;

    private void Start()
    {
        StartCoroutine(createTrain());
    }

    IEnumerator createTrain()
    {
        while (true)
        {
            if (!isOnMap)
            {
                isOnMap = true;
                Instantiate(deliveryTrainPrefab, pathStart.position, Quaternion.identity);//facing the direction of motion
                //the train will do the rest by itself and it will destroy itself and let us know the it has been destroyed
                yield return new WaitForSeconds(arrivalFrequency);
            }
            else //if it is on the map right now. Check again soon
                yield return new WaitForSeconds(3f);
        }
    }
}
