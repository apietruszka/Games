using UnityEngine;
using System.Collections;

public class Grave : MonoBehaviour
{
    Card[] card = new Card[50];
    public int amount = 0;

    private void Update()
    {
        for(int i=0;i<50;i++)
        {
            if(card[i]!=null)
            {
                card[i].desiredPosition = this.transform.position;
            }
        }
    }

    public void putCardIn(Card c)
    {
        if (amount < 50)
        {
            Debug.Log("card going to grave");
            card[amount] = c;
            amount++;
        }
        else
        {
            Debug.Log("too many cards in graveyard to put new");
        }
    }
}
