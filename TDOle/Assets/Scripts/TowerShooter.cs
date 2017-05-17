using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType { cannon, machineGun, heavyMachineHun, rocketLauncher, flameThrower}
public enum ShootingType { closest, strongest, furthest}

public class TowerShooter : MonoBehaviour {
    public ShootingType shootingType = ShootingType.closest;
    public Bullet bulletPrefab;//TO LINK
    public float fireCooldown = 1f;
    public float range = 300f;
    public Ammo ammo;
    public int kills = 0;
    public Transform top;

    private void Start()
    {
        ammo = GetComponent<Ammo>();
        StartCoroutine(findAndShoot());
    }

    IEnumerator findAndShoot()
    {
        while (true)
        {
            Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
            if (enemies.Length > 0 && ammo.amount>0)
            {
                if (shootingType == ShootingType.closest)
                {
                    Enemy closestEnemy = enemies[0];
                    float dist = Vector3.Distance(this.transform.position, enemies[0].transform.position);
                    for (int i = 0; i < enemies.Length; i++)
                        if (Vector3.Distance(this.transform.position, enemies[i].transform.position) < dist)
                        {
                            dist = Vector3.Distance(this.transform.position, enemies[i].transform.position);
                            closestEnemy = enemies[i];
                        }
                    top.LookAt(closestEnemy.transform);
                    if(dist<range)
                    {
                        Bullet bullet = (Bullet)Instantiate(bulletPrefab, transform.position, transform.rotation);
                        bullet.enemy = closestEnemy;
                        ammo.amount--;
                    }
                }
            }
            yield return new WaitForSeconds(fireCooldown);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
