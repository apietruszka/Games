using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeliveryStatus { needsDelivery, deliveryIncoming, noDeliveryAndNoNeedForOne}

public class TowerEconomy : MonoBehaviour {
    public Ammo ammo;
    public DeliveryStatus deliveryStatus = DeliveryStatus.noDeliveryAndNoNeedForOne;
    public bool autoOrderAmmo = true;
    public float orderDeliveryCheckRate = 3f;
    public TowerUI towerCanvas;

	
	void Start ()
    {
        ammo = GetComponent<Ammo>();
        StartCoroutine(orderDelivery());
	}

    void OnMouseUp()
    {
        TowerUI canvas = Instantiate(towerCanvas);
        canvas.tower = this.GetComponent<TowerShooter>();
    } 

    IEnumerator orderDelivery()
    {
        while(true)
        {
            if(autoOrderAmmo)
            {
                if (deliveryStatus != DeliveryStatus.deliveryIncoming && ammo.needsDelivery)
                    deliveryStatus = DeliveryStatus.needsDelivery;
                else if (!(ammo.needsDelivery))
                    deliveryStatus = DeliveryStatus.noDeliveryAndNoNeedForOne;
            }
            yield return new WaitForSeconds(orderDeliveryCheckRate);
        }
    }

    public void loadAmmo(Ammo a)
    {
        //if not enough, load all, remove all from a. If too much, load to fit, remove what you took from a.

        float ammoTowerCanUse = ammo.maxAmount-ammo.amount;
        float ammoToLoad = a.amount;
        if (ammoToLoad > ammoTowerCanUse)
        {
            ammoToLoad = ammoTowerCanUse;
        }
        ammo.amount += ammoToLoad;
        a.amount -= ammoToLoad;//take what you took from source
    }
}
