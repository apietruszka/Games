using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType { shells, rockets, napalm}

public class Ammo : MonoBehaviour {
    public float amount;
    public float maxAmount;
    public AmmoType ammoType;
    public float orderDeliveryRate = 0.5f;
    public bool needsDelivery = false;

    private void Start()
    {
        StartCoroutine(checkForDeliveries());
    }

    IEnumerator checkForDeliveries()//we're not informing our parents directly. Although it could be better to implement an interface to do so
    {
        while(true)
        {
            if (amount / maxAmount < orderDeliveryRate)
                needsDelivery = true;
            else
                needsDelivery = false;
            yield return new WaitForSeconds(5f);
        }
    }
}
