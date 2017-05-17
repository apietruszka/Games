using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTowerCosts : MonoBehaviour
{
    public static TowerCost[] towerCosts;

    private void Start()
    {
        List<TowerCost> costs = new List<TowerCost>();
        TowerCost c1 = new TowerCost();
        TowerCost c2 = new TowerCost();
        TowerCost c3 = new TowerCost();
        TowerCost c4 = new TowerCost();
        TowerCost c5 = new TowerCost();

        c1.towerType = TowerType.cannon;
        c1.cost = 100f;
        c2.towerType = TowerType.machineGun;
        c2.cost = 180;
        c3.towerType = TowerType.heavyMachineHun;
        c3.cost = 300f;
        c4.towerType = TowerType.rocketLauncher;
        c4.cost = 300f;
        c5.towerType = TowerType.flameThrower;
        c5.cost = 300f;

        costs.Add(c1);
        costs.Add(c2);
        costs.Add(c3);
        costs.Add(c4);
        costs.Add(c5);

        towerCosts = costs.ToArray();
    }
}

public struct TowerCost
{
    public TowerType towerType;
    public float cost;
}

public class TowerSpot : MonoBehaviour {

    public TowerSpotUI canvas;

    private void OnMouseUp()
    {
        //shut down all canvases
        TowerSpotUI[] allCanvases = GameObject.FindObjectsOfType<TowerSpotUI>();
        foreach (TowerSpotUI c in allCanvases)
            Destroy(c.gameObject);

        canvas.spotThatCreatedThis = this;
        Instantiate(canvas);
    }
    
}
