using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kropka : MonoBehaviour {
    bool isTaken = false;
    public ParticleSystem PS;

    private void Start()
    {
        PS = GameObject.FindObjectOfType<ParticleSystem>();
    }

    void OnMouseUp()
    {
        if (!isTaken)
        {
            isTaken = true;
            var gManager = GameObject.FindObjectOfType<GameManager>();
            PS.startColor = new Color(gManager.players[gManager.currentPlayerIndex].color.r, gManager.players[gManager.currentPlayerIndex].color.g, gManager.players[gManager.currentPlayerIndex].color.b,1f);
            PS.transform.position = this.transform.position;
            PS.Play();
            gManager.players[gManager.currentPlayerIndex].mojeKropy.Add(this);
            GetComponent<Renderer>().material.color = gManager.players[gManager.currentPlayerIndex].color;//set color to mathc player
            //TODO: some animation, maybe matching player color

            gManager.setNextPlayer();
        }
    }
}
