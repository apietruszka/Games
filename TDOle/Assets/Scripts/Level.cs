using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float moneyPrize = 100f;
    public EnemySpawner spawner;
    bool isStarted = false;

    //a level is started by pressing the start level button or by the time running out

    // Use this for initialization
    void Start()
    {
        StartCoroutine(checkLevelStatus());
        startLevel();
    }

    public void startLevel()
    {
        isStarted = true;
        Instantiate(spawner, this.transform.position, Quaternion.identity);
    }

    IEnumerator checkLevelStatus()
    {
        while(true)
        {
            if (isStarted && GameObject.FindObjectsOfType<Enemy>().Length == 0 && GameObject.FindObjectsOfType<EnemySpawner>().Length == 0) //no enemies and no spawners left
            {
                Debug.Log("you finished the level!");
                Player.money += moneyPrize;
                GameObject.FindObjectOfType<LevelManager>().levelEnded();
                StopCoroutine(checkLevelStatus());
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
