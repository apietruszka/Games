using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int hp = 1;
    public DeathSpawner[] deathSpawners;
    public Transform target;
    public int damageToPlayer = 1;
    public int moneyWorth = 5;



    private void FixedUpdate()
    {
        if((target.position-this.transform.position).sqrMagnitude<7f)
        {
            //you reached the end of the patch
            Player.takeLives(damageToPlayer);
            Destroy(gameObject);//die without calling DeathSpawner
        }
    }

    public void takeDamage(int damage)
    {
        hp -= damage;
        if(hp<0)
        {
            fallApart();
            Destroy(this.gameObject);
            Player.money += moneyWorth;
        }
    }

    public void fallApart()
    {
        foreach(DeathSpawner d in deathSpawners)
        {
            for(int i =0; i < d.enemiesAmount; i++)
            {
                Instantiate(d.enemy, this.transform.position, Quaternion.identity);//Might want to add some randomization with position spawning
            }
        }
    }
}
