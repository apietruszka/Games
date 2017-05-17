using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AmmoPrice
{
    public AmmoType ammoType;
    public float cost;
}

public class DeliveryTrain : MonoBehaviour {

    //CHECK ME: right now we assume the train has infinite ammo supplies. You might want to do this diffrently in the future.
    //CHECK ME: public float acceleration = 1f; //this will be way harder to implement. We would need to calculate when to use breaks in order not to overshoot and apply some friction that would be stronger the faster we went so we wouldnt accelerate into inifnite speed.
    //CHECK ME: right now we just go linearly. You might want some curvy moves in the future. btw make sure the line doesn't cross any buildings or anything like that


    public float speed = 5f;
    public Transform pathStart;
    public Transform deliverySpot;
    public float cargoUnloadTime = 3f;
    public bool hasDelivered = false;
    public List<AmmoPrice> ammoPrices = new List<AmmoPrice>();

    private void Start()
    {
        AmmoPrice shells, rockets, napalm;
        shells.ammoType = AmmoType.shells;
        rockets.ammoType = AmmoType.rockets;
        napalm.ammoType = AmmoType.napalm;

        shells.cost = 0.1f;
        rockets.cost = 1f;
        napalm.cost = 0.5f;

        ammoPrices.Add(shells);
        ammoPrices.Add(rockets);
        ammoPrices.Add(napalm);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(!hasDelivered)
        {
            Vector3 dir = deliverySpot.transform.position - this.transform.position;
            this.transform.position += dir.normalized * speed * Time.fixedDeltaTime;

            if(dir.sqrMagnitude<1f)
            {
                GameObject.FindObjectOfType<Magazine>().buyAmmo(ammoPrices.ToArray());
                hasDelivered = true;
            }
        }
        else
        {
            cargoUnloadTime -= Time.fixedDeltaTime;
            if(cargoUnloadTime<0)//we can depart
            {
                Vector3 dir = pathStart.transform.position - this.transform.position;
                this.transform.position += dir.normalized * speed * Time.fixedDeltaTime;

                if (dir.sqrMagnitude < 1f)
                {
                    //Debug.Log("departing");
                    GameObject.FindObjectOfType<DeliveryTrainManager>().isOnMap = false;//let them know we're disappearing
                    Destroy(this.gameObject);
                }
            }
        }
	}
}
