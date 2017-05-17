using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {
    public Player[] players;
    public int currentPlayerIndex = 0;
    float timer = 0;
    
	void Start () {
        players = GameObject.FindObjectsOfType<Player>();//Possible problem: creating players in menu
	}
	
	public void setNextPlayer()
    {
        players[currentPlayerIndex].findMyCombos(4);
        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Length)
            currentPlayerIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            players[currentPlayerIndex].findMyCombos(4);
        }
    }
}
