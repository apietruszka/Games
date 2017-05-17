using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static float money = 1000f;
    public static int lives = 100;

	public static void takeLives(int _lives)
    {
        lives -= _lives;
        if(lives<0)
        {
            Debug.Log("you died.");
            //TODO: death screen scene/UI
        }
    }

    

}
