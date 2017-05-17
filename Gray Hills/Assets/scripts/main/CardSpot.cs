using UnityEngine;
using System.Collections;

public class CardSpot : MonoBehaviour
{
    public Card card;
    public bool selected = false;
    public bool empty = true;
    public bool forMelee = true;

    private void Start()
    {

    }

    public void putCard(Card c)
    {
        if (empty)
        {
            Debug.Log("putting a card on CardSpot");
            empty = false;
            card = c;
        }
        else
        {
            Debug.Log("cardspot is occupied");
        }
    }
    

    public Card takeCard()
    {
        if (!empty)
        {
            Debug.Log("taking a card from the cardspot");
            empty = true;
            return card;
        }
        else
        {
            Debug.Log("no card on cardspot to take");
            return null;
        }
    }
    private void OnMouseUp()
    {
        Debug.Log("cardSpot selected");
        selected = true;
    }
    private void Update()
    {
    }
    private void OnMouseExit()
    {
        selected = false;
    }

}
