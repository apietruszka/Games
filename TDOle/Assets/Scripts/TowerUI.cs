using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour {

    public Text killsText;
    public Text ammoText;
    public TowerShooter tower;//make tower link that to us when it makes us

	void Update ()//TODO: coroutinezzz
    {
        killsText.text = "Kills: " + tower.kills;

        if (Input.GetKey(KeyCode.Escape))
            Destroy(gameObject);
        ammoText.text = "Ammo: " + tower.GetComponent<Ammo>().amount + "/" + tower.GetComponent<Ammo>().maxAmount;
	}

    public void sellTower()
    {
        Debug.Log("selling tower(TODO:do it)");
    }

    public void updateAutoOrderAmmo(bool toggle)
    {
        tower.GetComponent<TowerEconomy>().autoOrderAmmo = toggle;
    }

    public void updateAutoOrderAmmoRate(float value)
    {
        tower.ammo.orderDeliveryRate = value;
    }
}
