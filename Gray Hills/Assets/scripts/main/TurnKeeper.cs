using UnityEngine;
using System.Collections;

public class TurnKeeper : MonoBehaviour {

    public Player p1, p2;
    public PoisonEffect poisonPrefab;

    private void Start()
    {
        p1.myTurn = true;
        p2.myTurn = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if (p1.myTurn == true)
                p1.myTurn = false;
            else
                p1.myTurn = true;
            if (p2.myTurn == true)
                p2.myTurn = false;
            else
                p2.myTurn = true;

            if(p1.myTurn)
            {
                if (p1.maxMana < 10)
                    p1.maxMana++;
                p1.mana = p1.maxMana;
                p1.drawCard();
                for(int i=0;i<8;i++)
                {
                    if(!p1.cardSpot[i].empty)
                    {
                        p1.cardSpot[i].card.hasAttacked = false;
                        if (p1.cardSpot[i].card.poison != 0)
                        {
                            Instantiate(poisonPrefab, p1.cardSpot[i].transform);
                            p1.cardSpot[i].card.hp -= p1.cardSpot[i].card.poison;
                            if (p1.cardSpot[i].card.hp < 0)
                            {
                                p1.grave.putCardIn(p1.cardSpot[i].takeCard());
                            }
                        }
                    }
                }
            }
            else if (p2.myTurn)
            {
                if (p2.maxMana < 10)
                    p2.maxMana++;
                p2.mana = p2.maxMana;
                p2.drawCard();
                for (int i = 0; i < 8; i++)
                {
                    if (!p2.cardSpot[i].empty)
                    {
                        p2.cardSpot[i].card.hasAttacked = false;
                        if (p2.cardSpot[i].card.poison != 0)
                        {
                            Instantiate(poisonPrefab, p2.cardSpot[i].transform);
                            p2.cardSpot[i].card.hp -= p2.cardSpot[i].card.poison;
                            if (p2.cardSpot[i].card.hp <= 0)
                            {
                                p2.grave.putCardIn(p2.cardSpot[i].takeCard());
                            }
                        }
                    }
                }
            }
        }
    }
}
