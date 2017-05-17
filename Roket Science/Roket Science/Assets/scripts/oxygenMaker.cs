using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oxygenMaker : MonoBehaviour {

    public Room room;
    public float generationSpeed = 1f;

	// Use this for initialization
	void Start () {
        room = GetComponent<Room>();
	}
	
	// Update is called once per frame
	void Update () {
        if (room.oxygenPercent < 1f)
        {
            room.oxygen.litres += Time.deltaTime * generationSpeed;
            if (room.oxygen.litres > room.litres)
                room.oxygen.litres = room.litres;
        }
	}
}
