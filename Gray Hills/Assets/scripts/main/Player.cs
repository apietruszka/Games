using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Deck deck;
    public Hand hand;
    public CardSpot[] cardSpot = new CardSpot[8];
    public CardSpot[] enemyCardSpot = new CardSpot[8];//those must be linked from editor
    public Grave grave;
    public int maxMana = 1;
    public int mana=1;
    public bool myTurn;
    
    public int selectType=0;//1 for hand, 2 for cardspot
    public int selectID =10;
    public int targetType =0;//2 for cardspot, 3 for enemyCardSpot
    public int targetID =10;

    public HealingEffect healPrefab;
    public FireskinEffect fireskinPrefab;

    public Card hoveredCard=null;
    public GameObject hoveredCardGraphic = null;

    private void Start()
    {
        deck = GetComponentInChildren<Deck>();
        hand = GetComponentInChildren<Hand>();
        cardSpot = GetComponentsInChildren<CardSpot>();
    }
    void findHoveredCard()
    {
        //scan our hand, our battlefield, enemy battlefield
        for(int i=0;i<hand.amount;i++)
        {
            if(hand.card[i].hover)
            {
                hoveredCard = hand.card[i];
                return;
            }
        }
        for(int i=0;i<8;i++)
        {
            if(!cardSpot[i].empty)
            {
                if(cardSpot[i].card.hover)
                {
                    hoveredCard = cardSpot[i].card;
                    return;
                }
            }
        }
        for (int i = 0; i < 8; i++)
        {
            if (!enemyCardSpot[i].empty)
            {
                if (enemyCardSpot[i].card.hover)
                {
                    hoveredCard = enemyCardSpot[i].card;
                    return;
                }
            }
        }
        hoveredCard = null;
    }

    void displayHoveredCard()
    {
        Vector3 hoverPos=Vector3.zero;
        hoverPos.Set(10f, 10f, 7f);
        hoveredCard.transform.position=hoverPos;
        
    }

    void findSelected()
    {
        if(selectType==0)
        {
            selectID = hand.findSelectedCard();
            if (selectID != 10)//found something on hand
            {
                selectType = 1;
            }
            for(int i=0;i<8;i++)//scan cardSpots
            {
                if (cardSpot[i].selected && cardSpot[i].empty==false)
                {
                    selectType = 2;
                    selectID = i;
                }
            }
        }
    }
    void findTargeted()
    {
        if(targetType==0)
        {
            for (int i = 0; i < 8; i++)//scan cardSpots
            {
                if (cardSpot[i].selected && cardSpot[i].empty)
                {
                    
                    targetType = 2;
                    targetID = i;
                }
            }
            for (int i = 0; i < 8; i++)//scan enemy cardSpots
            {
                if (enemyCardSpot[i].selected)// && enemyCardSpot[i].card != null)
                {
                    targetType = 3;
                    targetID = i;
                }
            }
        }
    }
    void clearSearch()
    {
        int cardsInHand = hand.amount;
        for(int i=0;i<cardsInHand;i++)
        {
            hand.card[i].selected = false;
        }
        for(int i=0;i<8;i++)
        {
            cardSpot[i].selected = false;
            enemyCardSpot[i].selected = false;
        }
    }

    public void drawCard()
    {
        if (deck.amount > 0)
        {
            Card cardToDraw = deck.takeCardFrom();
            hand.putCardIn(cardToDraw);
        }
    }

    void putCardOnCardSpot(int cardID, int cardSpotID)
    {
        if (cardSpot[cardSpotID].card == null && hand.card[cardID] != null)
        {
            Debug.Log("putting card on cardspot");
            Card cardToPut = hand.takeCardFrom(cardID);
            cardSpot[cardSpotID].putCard(cardToPut);
            cardToPut.desiredPosition = cardSpot[cardSpotID].transform.position-Vector3.up*0.01f;
        }
        else
        {
            Debug.Log("can't put card on cardspot. Either cardspot occupied, or card empty");
        }
    }
    void attack(int attackerID, int attackedID)
    {
        int enemyHpBefore = enemyCardSpot[attackedID].card.hp;
        Debug.Log("attacking");
        enemyCardSpot[attackedID].card.takeDmg(cardSpot[attackerID].card.damage);
        int enemyHpAfter = enemyCardSpot[attackedID].card.hp;
        
        if(enemyCardSpot[attackedID].card.fireskin!=0)
        {
            Instantiate(fireskinPrefab, enemyCardSpot[attackedID].transform);
            cardSpot[attackerID].card.hp -= enemyCardSpot[attackedID].card.fireskin;
        }
        if(cardSpot[attackerID].card.lifesteal!=0)
        {
            int hpDiff = enemyHpBefore - enemyHpAfter;
            if(hpDiff>0)
            {
                Instantiate(healPrefab, cardSpot[attackerID].transform);
                cardSpot[attackerID].card.hp += hpDiff* cardSpot[attackerID].card.lifesteal;
                if (cardSpot[attackerID].card.hp > cardSpot[attackerID].card.hpMax)
                    cardSpot[attackerID].card.hp = cardSpot[attackerID].card.hpMax;
            }
        }
        if(cardSpot[attackerID].card.infection != 0)
        {
            int hpDiff = enemyHpBefore - enemyHpAfter;
            if (hpDiff > 0)
            {
                if(!enemyCardSpot[attackedID].empty)//if our attack didnt kill it already, poison it
                {
                    enemyCardSpot[attackedID].card.poison = cardSpot[attackerID].card.infection;
                }
            }
        }
        if (enemyCardSpot[attackedID].card.hp <= 0)
        {
            Card cardToGrave = enemyCardSpot[attackedID].takeCard();
            grave.putCardIn(cardToGrave);
        }
        if (cardSpot[attackerID].card.hp <= 0)
        {
            Card cardToGrave = cardSpot[attackerID].takeCard();
            grave.putCardIn(cardToGrave);
        }

    }
    void clearAll()
    {
        selectID = 10;
        targetID = 10;
        selectType = 0;
        targetType = 0;
    }
    private void LateUpdate()
    {
        
        
        if (myTurn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                drawCard();
            }
            if (selectType == 0)
            {
                findSelected();
            }
            else
            {
                findTargeted();
                if (targetType != 0)
                {
                    //not being entered at the moment after clicking cardSpot
                    if (selectType == 1 && targetType == 2)
                    {
                        if (cardSpot[targetID].forMelee&&hand.card[selectID].isMelee)
                        {
                            if (mana >= hand.card[selectID].cost && cardSpot[targetID].empty)//can afford it and spot is empty
                            {
                                mana = mana - hand.card[selectID].cost;
                                if (hand.card[selectID].charge)
                                    hand.card[selectID].hasAttacked = false;
                                putCardOnCardSpot(selectID, targetID);
                            }
                        }
                        else if(!cardSpot[targetID].forMelee && !hand.card[selectID].isMelee)
                        {
                            if (mana >= hand.card[selectID].cost && cardSpot[targetID].empty)//can afford it and spot is empty
                            {
                                mana = mana - hand.card[selectID].cost;
                                if (hand.card[selectID].charge)
                                    hand.card[selectID].hasAttacked = false;
                                putCardOnCardSpot(selectID, targetID);
                            }
                        }
                    }
                    else if (selectType == 2 && targetType == 2)
                    {
                        if (cardSpot[selectID].transform.position.x - cardSpot[targetID].transform.position.x < 0.05f && cardSpot[selectID].transform.position.x - cardSpot[targetID].transform.position.x > -0.05f )
                        {
                            if (!cardSpot[selectID].card.hasAttacked || cardSpot[selectID].card.mobile)
                            {
                                if(!cardSpot[selectID].card.mobile)
                                    cardSpot[selectID].card.hasAttacked = true;
                                Debug.Log("putting card on cardspot");
                                Card cardToPut = cardSpot[selectID].takeCard();
                                cardSpot[targetID].putCard(cardToPut);
                                cardToPut.desiredPosition = cardSpot[targetID].transform.position - Vector3.up * 0.01f;
                            }
                        }
                    }
                    else if (selectType == 2 && targetType == 3)
                    {
                        if (enemyCardSpot[targetID].card.isFlyer==false||(enemyCardSpot[targetID].card.isFlyer&&(cardSpot[selectID].card.isFlyer|| cardSpot[selectID].card.isMelee==false)))
                        {
                            if (((cardSpot[selectID].transform.position.z - enemyCardSpot[targetID].transform.position.z < 0.05f && cardSpot[selectID].transform.position.z - enemyCardSpot[targetID].transform.position.z > -0.05f)||cardSpot[selectID].card.sniper) && !cardSpot[selectID].card.hasAttacked)//same row
                            {
                                if (cardSpot[selectID].forMelee == false)//ranged attacker
                                {
                                    if (cardSpot[selectID].card.hasAttacked == false && enemyCardSpot[targetID].empty == false)//didn't attack already and enemy spot occupied
                                    {
                                        cardSpot[selectID].card.hasAttacked = true;
                                        attack(selectID, targetID);
                                    }
                                }
                                else//melee attacker
                                {
                                    if (enemyCardSpot[targetID].forMelee)
                                    {
                                        if (cardSpot[selectID].card.hasAttacked == false && enemyCardSpot[targetID].empty == false)//didn't attack already and enemy spot occupied
                                        {
                                            cardSpot[selectID].card.hasAttacked = true;
                                            attack(selectID, targetID);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    clearSearch();
                    clearAll();
                }
            }
        }
    }
}
