using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TowerSpotUI : MonoBehaviour {
    
    public GameObject cannonPrefab;
    public GameObject machinePrefab;
    public GameObject heavyMachinePrefab;
    public GameObject rocketLauncherPrefab;
    public GameObject FlamePrefab;

    public TowerSpot spotThatCreatedThis;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Destroy(gameObject);
    }


    public void makeCannon()
    {
        if (Player.money >= 100f)
        {
            Player.money -= 100f;
            Instantiate(cannonPrefab, spotThatCreatedThis.transform.position, Quaternion.identity);
            Destroy(spotThatCreatedThis.gameObject);
            Destroy(gameObject);//close canvas
        }
        else
            Debug.Log("not enough money to buy");
    }

    public void makeMachineGun()
    {
        if (Player.money >= 180f)
        {
            Debug.Log(spotThatCreatedThis.name);
            Player.money -= 180f;
            Instantiate(machinePrefab, spotThatCreatedThis.transform.position, Quaternion.identity);
            Destroy(spotThatCreatedThis.gameObject);
            Destroy(gameObject);//close canvas
        }
        else
            Debug.Log("not enough money to buy");
    }

    public void makeHeavyMachineGun()
    {
        if (Player.money >= 300f)
        {
            Player.money -= 300f;
            Instantiate(heavyMachinePrefab, spotThatCreatedThis.transform.position, Quaternion.identity);
            Destroy(spotThatCreatedThis.gameObject);
            Destroy(gameObject);//close canvas
        }
        else
            Debug.Log("not enough money to buy");
    }

    public void makeRocketLauncher()
    {
        if (Player.money >= 300f)
        {
            Player.money -= 300f;
            Instantiate(rocketLauncherPrefab, spotThatCreatedThis.transform.position, Quaternion.identity);
            Destroy(spotThatCreatedThis.gameObject);
            Destroy(gameObject);//close canvas
        }
        else
            Debug.Log("not enough money to buy");
    }

    public void makeFlameThrower()
    {
        if (Player.money >= 300f)
        {
            Player.money -= 300f;
            Instantiate(FlamePrefab, spotThatCreatedThis.transform.position, Quaternion.identity);
            Destroy(spotThatCreatedThis.gameObject);
            Destroy(gameObject);//close canvas
        }
        else
            Debug.Log("not enough money to buy");
    }
}
