using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DroneStatus { goingToTarget, returningToMagazine, waitingInMagazine}

public class DispatchDrone : MonoBehaviour
{
    public DroneStatus droneStatus=DroneStatus.returningToMagazine;
    public TowerEconomy targetTower;
    public Ammo ammo;
    public float speed = 5f;
    public Magazine mag;

    private void Start()
    {
        ammo = GetComponent<Ammo>();
        mag = GameObject.FindObjectOfType<Magazine>();
    }

    public void loadAmmo(Ammo a)
    {
        float ammoDroneCanUse = a.amount;
        ammo.ammoType = a.ammoType;//set the right type of ammo;
        float ammoToLoad = ammo.maxAmount;
        if(ammoToLoad>ammoDroneCanUse)
        {
            ammoToLoad = ammoDroneCanUse;
        }
        ammo.amount = ammoToLoad;
        a.amount -= ammoToLoad;//take as much from magazine as drone just loaded
    }

    public void orderDispatch(TowerEconomy t)
    {
        droneStatus = DroneStatus.goingToTarget;
        targetTower = t;
    }


    private void FixedUpdate()//Does motion calculations if drone is supposed to move
    {
        if(droneStatus==DroneStatus.goingToTarget)
        {
            //go towards the target tower
            Vector3 dir = targetTower.transform.position - this.transform.position;
            this.transform.position += dir.normalized * speed * Time.fixedDeltaTime;

            //check if we reached our destination
            if(dir.sqrMagnitude<1f)
            {
                Debug.Log("drone " + this.name + " reached " + targetTower.name + ". Unloading and returning to magazine.");
                targetTower.loadAmmo(ammo);//unload ammo
                targetTower.deliveryStatus = DeliveryStatus.noDeliveryAndNoNeedForOne;
                
                droneStatus = DroneStatus.returningToMagazine;
                targetTower = null;
            }
        }
        else if(droneStatus==DroneStatus.returningToMagazine)
        {
            //go towards the magazine
            Vector3 dir = mag.transform.position - this.transform.position;
            this.transform.position += dir.normalized * speed * Time.fixedDeltaTime;

            //check if we reached our destination
            if (dir.sqrMagnitude < 1f)
            {
                Debug.Log("drone " + this.name + " reached " + mag.name + ". Unloading and waiting for new dispatch order.");
                if (ammo != null)//this can happen sometimes
                {
                    mag.loadAmmo(ammo);//unload ammo
                    ammo.amount = 0;
                }

                droneStatus = DroneStatus.waitingInMagazine;
            }
        }
    }
}
