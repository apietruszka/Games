using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
    public int cost=3;
    public bool selected = false;
    public bool hover = false;
    public bool hasAttacked = true;
    public int damage=2, hp=5;
    public int hpMax = 5;

    public Vector3 startingPosition;
    public Vector3 desiredPosition;
    public TextMesh hpText;
    public TextMesh damageText;
    public TextMesh costText;

    bool isMoving = false;
    Vector3 startMove;
    Vector3 delta;
    public float speed = 0.1F;
    float currentSinPos = 0;
    public bool isMelee = true;
    public bool isFlyer = false;

    public int lifesteal = 0;
    public int infection = 0;
    public int fireskin = 0;
    public bool taunt = false;
    public bool charge = false;
    public bool mobile = false;
    public bool sniper = false;

    public int poison = 0;

    private void Start()
    {
        startingPosition = this.transform.position;
        desiredPosition = startingPosition;
        hp = hpMax;
    }
    private void Update()
    {
        hpText.text = hp.ToString();
        damageText.text = damage.ToString();
        costText.text = cost.ToString();

        if (this.transform.position!=desiredPosition&&!isMoving)
        {
            isMoving = true;
            startingPosition = this.transform.position;
            startMove = this.transform.position;
            delta = desiredPosition - this.transform.position;
        }
        if(isMoving)
        {
            float velocity = speed / delta.sqrMagnitude;
            if (currentSinPos >= Mathf.PI / 2f)
            {
                currentSinPos = Mathf.PI / 2f;
            }
            else
            {
                currentSinPos += velocity;
            }
            this.transform.position =  Vector3.Lerp(startMove, desiredPosition, currentSinPos);
            if(Vector3.Distance(startMove,this.transform.position)<0.05f)
            {
                isMoving = false;
                this.transform.position = desiredPosition;
                currentSinPos=0;
            }
        }
    }

    public void takeDmg(int dmg)
    {
        hp -= dmg;
    }

    private void OnMouseUp()
    {
        Debug.Log("card selected");
        selected = true;
    }
    private void OnMouseExit()
    {
        selected = false;
        hover = false;
    }
    private void OnMouseEnter()
    {
        hover = true;
    }
}
