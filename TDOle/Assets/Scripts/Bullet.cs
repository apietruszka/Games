using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Enemy enemy;
    public float speed = 25f;
    public int damage = 1;

	public Bullet(Enemy target)
    {
        enemy = target;
    }
	
	void FixedUpdate ()
    {
        if (enemy == null)//has been killed or reached the goal
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 dir = enemy.transform.position - this.transform.position;
            this.transform.position += dir.normalized * Time.fixedDeltaTime * speed;

            if (dir.sqrMagnitude < 2f)
            {
                enemy.takeDamage(damage);
                Destroy(gameObject);
            }
        }
	}
}
