using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {
    Player[] players;
    public Text[] scores;

	void Start () {
        players = GameObject.FindObjectsOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        updateInfo();
	}

    public void updateInfo()
    {
        int playersNum = players.Length;
        for(int i=0;i<8;i++)
        {
            if (i < playersNum)
            {
                scores[i].text = "Player " + (i+1).ToString() + ": " + (int)players[i].points;
                scores[i].color = new Color(players[i].color.r, players[i].color.g, players[i].color.b, 1f);
            }
            else
                scores[i].text = "";
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene("startingScreen");
    }
}
