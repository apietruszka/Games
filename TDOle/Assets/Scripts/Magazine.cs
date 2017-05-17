using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour {
    //TODO: deliveryTrain, deliveryDrones
    public Ammo[] ammo;
    //DeliveryTrain deliveryTrain;
    DeliveryDrone[] deliveryDrones;
    DispatchDrone[] dispatchDrones;
    TowerEconomy[] towers;
    public float referenceCheckRate = 5f;
    public bool autoBuyAmmo = false;
    public int maxDrones = 15;
    //We might want to keep reference only to usable dudes and make them send their usability info to this object in the future. Right now we cache them all

    public GameObject magazineCanvasPrefab;

	void Start () {
        StartCoroutine(checkForNewReferences());
        ammo = GetComponents<Ammo>();
	}

    public void loadAmmo(Ammo a)
    {
        //we take our ammo or ammo that's already paid for.
        Ammo ammoToAddTo=null;
        foreach (Ammo am in ammo)
            if (am.ammoType == a.ammoType)
                ammoToAddTo = am;
        ammoToAddTo.amount += a.amount;
        if (ammoToAddTo.amount > ammoToAddTo.maxAmount)
            ammoToAddTo.amount = ammoToAddTo.maxAmount;//get rid off overflows. They just disappear at this point.
    }

    private void OnMouseUp()
    {
        Instantiate(magazineCanvasPrefab);
    }

    //coroutine for optimalization strictly
    IEnumerator checkForNewReferences()
    {
        while(true)
        {
            //periodically checks all towers, drones - just in case they don't let you know of their creation. Might get rid of this in the future
            deliveryDrones = GameObject.FindObjectsOfType<DeliveryDrone>();
            dispatchDrones = GameObject.FindObjectsOfType<DispatchDrone>();
            towers = GameObject.FindObjectsOfType<TowerEconomy>();

            sendDrones();

            yield return new WaitForSeconds(referenceCheckRate);
        }
    }

    public void buyAmmo(AmmoPrice[] prices)
    {
        if (autoBuyAmmo)
        {
            //find the cost of all the things you want to buy.
            //if you have more than that, buy all
            //else buy money/costOfAll *100% of how much you want of each type

            float combinedCost = 0;
            float percentageToBuy = 1f;

            foreach (Ammo a in ammo)
            {
                if (a.needsDelivery)
                {
                    float costOfThatType = 0;
                    foreach (AmmoPrice p in prices)//TODO: we might not have the prices match for some reason(a bug)
                        if (p.ammoType == a.ammoType)//types match
                            costOfThatType = p.cost;
                    combinedCost += (a.maxAmount * a.orderDeliveryRate - a.amount) * costOfThatType;
                }

                if (combinedCost > Player.money)//we can't afford it all
                    percentageToBuy = Player.money / combinedCost;

                //buy percentageToBuy of all ammo types (floored)
            }
            foreach (Ammo a in ammo)
            {
                if (a.needsDelivery)
                {
                    //get it
                    a.amount += (a.maxAmount * a.orderDeliveryRate - a.amount) * percentageToBuy;
                    //Right now we use floats as money so no need for rounding.

                    //pay for it
                    float costOfThatType = 0;
                    foreach (AmmoPrice p in prices)//TODO: we might not have the prices match for some reason(a bug)
                        if (p.ammoType == a.ammoType)//types match
                            costOfThatType = p.cost;
                    Player.money -= (a.maxAmount * a.orderDeliveryRate - a.amount) * percentageToBuy * costOfThatType;
                }
            }
        }
    }

    void sendDrones()
    {
        //find all dispatch drones you can use.
        Stack<DispatchDrone> dronesReadyToWork = new Stack<DispatchDrone>(); //all drones currently in the magazine
        foreach (DispatchDrone d in dispatchDrones)
            if (d.droneStatus == DroneStatus.waitingInMagazine)//TODO: do this in drone class
                dronesReadyToWork.Push(d);

        //find all towers that need tending to
        Stack<TowerEconomy> towersInNeed = new Stack<TowerEconomy>(); //all towers with ammo/max ammo < activation rate
        foreach (TowerEconomy t in towers)
            if (t.deliveryStatus == DeliveryStatus.needsDelivery)
                towersInNeed.Push(t);

        //TODO: you might want to apply some sorting based of amount of ammo ascending or distance or sth

        while(dronesReadyToWork.Count > 0 && towersInNeed.Count > 0)
        {
            //load references
            TowerEconomy towerToHelp = towersInNeed.Pop();
            //make sure we have this tower's ammo before we go forward;
            AmmoType ammoType = towerToHelp.ammo.ammoType;
            //check if we even have this ammo type here
            bool weHaveThisAmmo = false;
            Ammo ammoToLoadToDrone = null;//this might cause an error
            foreach(Ammo a in ammo)
            {
                if (ammoType == a.ammoType)
                    if (a.amount > 0)
                    {
                        weHaveThisAmmo = true;
                        ammoToLoadToDrone = a;
                    }
            }
            if (weHaveThisAmmo)
            {
                DispatchDrone droneToGo = dronesReadyToWork.Pop();

                //fill that drone with ammo of the right ammo type
                droneToGo.loadAmmo(ammoToLoadToDrone);

                //set tower as target for drone
                droneToGo.orderDispatch(towerToHelp);

                //tell the tower we send her some ammo, so she doesn't ask for ammo next frame
                towerToHelp.deliveryStatus = DeliveryStatus.deliveryIncoming;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Player.money += 1000;
        }
    }
}
