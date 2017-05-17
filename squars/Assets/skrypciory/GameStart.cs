using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    public GameObject Kropka;
    public GameObject GameManager;
    public GameObject[] Player;
    public GameObject Score;


    void Start ()
    {
        //StartingUI
        int dimX = StartingUI.xDim;
        int dimY = StartingUI.yDim;
        int numOfPlayers = StartingUI.players;
        if (numOfPlayers > 8) numOfPlayers = 8;
        Vector3 lastKropkaPos=Vector3.zero;

        
        for(int i=0;i<dimX;i++)
        {
            for(int j=0;j<dimY;j++)
            {
                Instantiate(Kropka, Vector3.right * i * 1f + Vector3.up * j * 1f, Quaternion.identity);
                if (i == dimX - 1 && j == dimY - 1)
                    lastKropkaPos = Vector3.right * i * 1f + Vector3.up * j * 1f;
            }
        }

        //first create players, cause manager draws players at start(maybe would work either way cuz same func?)
        for(int i=0;i<numOfPlayers;i++)
        {
            Instantiate(Player[i]);//TODO: color?
        }
        Instantiate(GameManager);

        Camera cam = GameObject.FindObjectOfType<Camera>();//set cam to center
        cam.transform.position = lastKropkaPos / 2f + new Vector3(0,0,-10f);//not perfect, but its foine
        Instantiate(Score);

        Destroy(gameObject);
    }
}
