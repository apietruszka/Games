using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Level[] levels;
    public int currentLevel = 0;
    bool canStartNewLevel = true;

    private void Start()
    {
    }

    public void levelEnded()
    {
        //currentLevel++;
        if (currentLevel < levels.Length)
        {
            //start new level. It will autolaunch after some time or you can implement starting by pressing a button
            canStartNewLevel = true;
        }
        else
        {
            Debug.Log("You've finished the game. You can go home now :)");
        }
    }

    public void startNewLevel()
    {
        //if(canStartNewLevel)
        {
            canStartNewLevel = false;
            Instantiate(levels[currentLevel]);
            currentLevel++;
        }
    }
}
