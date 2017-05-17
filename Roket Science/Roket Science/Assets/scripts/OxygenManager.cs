using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenManager : MonoBehaviour {

    //brainstorm:
    //1) graph solution : problem - i don't know any graphs
    //2) every door has reference to two rooms : nice for oxygen redistribution, bad for pathfinding
    //3) every room has reference to all its' doors : can do ox redist and pathfinding. Problem: extremely slow algorithm

    Door[] doors;//all. We access all rooms via these
    public float oxygenFlowSpeed = 1f;
    
    public void setOxygenFlow(string x)
    {
        oxygenFlowSpeed = float.Parse(x);
    }

	void Start ()
    {
        doors = GameObject.FindObjectsOfType<Door>();
	}
	
	void Update ()
    {
        //do this less often
        balanceOxygen();
	}

    void balanceOxygen()
    {
        //calculate
        foreach(Door d in doors)
        {
            if (d.open)
                d.oxygenFrom1To2 = (d.r1.oxygenPercent - d.r2.oxygenPercent) * Time.deltaTime * oxygenFlowSpeed;
            else
                d.oxygenFrom1To2 = 0;
        }
        //execute
        foreach(Door d in doors)
        {
            //we might want to keep a fixed value or care about max V od room that is giving oxygen
            d.r1.oxygen.litres -= d.oxygenFrom1To2;
            d.r2.oxygen.litres += d.oxygenFrom1To2;
            d.oxygenFrom1To2 = 0;
        }
    }
}
