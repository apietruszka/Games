using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    public Card[] card = new Card[30];
    public int amount;

    private void Start()
    {
        Card[] startingCards = GetComponentsInChildren<Card>();
        amount = startingCards.Length;
        for (int i = 0; i < amount; i++)
        {
            card[i] = startingCards[i];
        }

        randomizeCards();
    }

    public Card takeCardFrom()
    {
        if (amount > 0)
        {
            Debug.Log("drawing a card from deck");
            Card cardToWithdraw = card[amount - 1];
            card[amount - 1] = null;
            amount--;
            return cardToWithdraw;
        }
        else
        {
            Debug.Log("no cards in deck to draw");
            return null;
        }
    }
    public void putCardIn(Card c)
    {
        if(amount<30)
        {
            Debug.Log("putting card in deck");
            card[amount] = c;
            amount++;
        }
        else
        {
            Debug.Log("too many cards in deck. Can't put new card in deck");
        }
    }
    public void randomizeCards()
    {
        int startingAmount = amount;
        Card[] newCardsToSwitchAfter = new Card[30];
        for(int i=0;i< startingAmount; i++)
        {
            Debug.Log(i);
            int randomID = Random.Range(0, amount - 1);
            newCardsToSwitchAfter[i] = card[randomID];
            card[randomID] = null;
            amount--;


            bool postNull = false;
            for (int j = 0; j < amount ; j++)
            {
                if (card[j] == null)
                {
                    postNull = true;
                    card[j] = card[j + 1];
                }
                else
                {
                    if (postNull)
                    {
                        card[j] = card[j + 1];
                    }
                }
            }
        }
        card = newCardsToSwitchAfter;
        for(int i=0;i<30;i++)
        {
            if(card[i]!=null)
            {
                amount++;
            }
        }
    }

}
