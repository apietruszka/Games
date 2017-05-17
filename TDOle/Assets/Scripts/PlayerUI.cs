using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Text HP;
    public Text Cash;

    private void Update()
    {
        HP.text = "Lives: " + Player.lives;
        Cash.text = "Cash: " + Player.money;
    }
}
