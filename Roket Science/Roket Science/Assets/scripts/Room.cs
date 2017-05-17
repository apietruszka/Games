using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public Oxygen oxygen;
    public float litres;
    public float oxygenPercent;
    public TextMesh percentage;

    void Start()
    {
        oxygen = new Oxygen(100f);
    }

    void Update()
    {
        //do less often!
        oxygenPercent = oxygen.litres / litres;
        percentage.text = (100f*oxygenPercent).ToString("0")+"%";

        Debug.Log(oxygenPercent);
        GetComponent<Renderer>().material.color = new Color(1f-oxygenPercent, 1f-oxygenPercent, 1f);
    }
}
