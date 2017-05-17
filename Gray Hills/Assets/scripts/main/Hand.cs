using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    public Card[] card=new Card[10];
    public int amount=0;
    

    private void Start()
    {
        Card[] startingCards = GetComponentsInChildren<Card>();
        amount = startingCards.Length;
        for(int i=0;i<amount;i++)
        {
            card[i] = startingCards[i];
        }
    }

    private void Update()
    {
        float distBetweenCards = 1.5f;
        float distForFirstCard=(amount-1f)*distBetweenCards/2f;
        for(int i=0;i<amount;i++)
        {
            card[i].desiredPosition = this.transform.position + distForFirstCard * Vector3.right + distBetweenCards * Vector3.left * i;
        }
    }

    

    public void putCardIn(Card c)
    {
        if (amount < 10)
        {
            Debug.Log("drawing a card to hand");
            card[amount] = c;
            amount++;
        }
        else
        {
            Debug.Log("too many cards in hand to draw");
        }
    }

    public Card takeCardFrom(int id)
    {
        if (card[id] != null)
        {
            Debug.Log("giving a card from hand");
            Card cardToGive = card[id];
            card[id] = null;//TODO: reassemble the card[] array
            reassembleArray();
            amount--;
            return cardToGive;
        }
        else
        {
            Debug.Log("this spot in hand is empty");
            return null;
        }
    }

    void reassembleArray()
    {
        bool postNull = false;
        for(int i=0;i<amount-1;i++)
        {
            if (card[i] == null)
            {
                postNull = true;
                card[i] = card[i + 1];
            }
            else
            {
                if (postNull)
                {
                    card[i] = card[i + 1];
                }
            }
        }
        card[amount - 1] = null;
    }

    public int findSelectedCard()
    {
            for (int i = 0; i < amount; i++)
            {
                if (card[i].selected)
                {
                    return i;
                }
            }
        return 10;
    }


}
