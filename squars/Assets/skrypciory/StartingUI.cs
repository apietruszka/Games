using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class StartingUI : MonoBehaviour {

    static public int xDim = 8;
    static public int yDim = 8;
    static public int players = 2;

    public void ChangePlayers(string arg)
    {
        players = Int32.Parse(arg);
    }
    public void ChangeXDim(string arg)
    {
        xDim = Int32.Parse(arg);
    }
    public void ChangeYDim(string arg)
    {
        yDim = Int32.Parse(arg);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("dototos");
    }
}
