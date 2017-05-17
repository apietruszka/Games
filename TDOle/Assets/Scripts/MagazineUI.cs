using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineUI : MonoBehaviour {

    public Magazine mag;
    public Text dronesAmount;
    public Text shellsAmount;
    public Text rocketsAmount;
    public Text napalmAmount;
    public GameObject upgradePanel;
    public GameObject resourcePanel;
    public GameObject dronePrefab;

	public void openUpgradePanel()
    {
        resourcePanel.SetActive(false);
        upgradePanel.SetActive(true); 
    }
    
    public void openResourcePanel()
    {
        resourcePanel.SetActive(true);
        upgradePanel.SetActive(false);
    }

    public void buyDrone()
    {
        DispatchDrone[] drones = GameObject.FindObjectsOfType<DispatchDrone>();
        if(drones.Length < mag.maxDrones && Player.money >= 150)//150 - drone cost. We might want to keep this somewhere else for expanding
        {
            Player.money -= 150;
            Instantiate(dronePrefab, mag.transform.position, Quaternion.identity);//might not work properly. Check.
        }
    }

    private void Start()
    {
        mag = GameObject.FindObjectOfType<Magazine>();
    }

    private void Update()//TODO: use a coroutine instead, update once a sec or so. Make sure to use destructor to remove it when exiting
    {
        DispatchDrone[] drones = GameObject.FindObjectsOfType<DispatchDrone>();
        dronesAmount.text = "Drones: " + drones.Length + "/" + mag.maxDrones;

        foreach(Ammo a in mag.ammo)
        {
            if(a.ammoType==AmmoType.shells)
            {
                //update shells msg
                shellsAmount.text = a.amount + "/" + a.maxAmount;
            }
            else if(a.ammoType==AmmoType.rockets)
            {
                rocketsAmount.text = a.amount + "/" + a.maxAmount;
            }
            else if(a.ammoType==AmmoType.napalm)
            {
                napalmAmount.text = a.amount + "/" + a.maxAmount;
            }
        }

        if (Input.GetKey(KeyCode.Escape))
            Destroy(gameObject);
    }

    public void updateResupplyAuto(bool toggle)
    {
        mag.autoBuyAmmo = toggle;
    }

    public void updateResupplyRateShells(float value)
    {
        foreach (Ammo a in mag.ammo)//TODO: learn and use Linq for stuff like that
            if (a.ammoType == AmmoType.shells)
                a.orderDeliveryRate = value;
    }

    public void updateResupplyRateRockets(float value)
    {
        foreach (Ammo a in mag.ammo)
            if (a.ammoType == AmmoType.rockets)
                a.orderDeliveryRate = value;
    }

    public void updateResupplyRateNapalm(float value)
    {
        foreach (Ammo a in mag.ammo)
            if (a.ammoType == AmmoType.napalm)
                a.orderDeliveryRate = value;
    }
}
