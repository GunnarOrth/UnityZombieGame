using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{

    public GameObject spawnPoint;
    public GameObject spawnPoint2;
    public int Round = 1;
    public int amountToSpawn = 5;
    public int amountSpawned = 0;
    public int amountKilled = -1;
    public int totalEnemiesKilled = 0;
    public TextMeshProUGUI roundDisplay;
    public float spawnDelay = 5.0f;
    private bool walter = true;
    // Update is called once per frame
    void Update()
    {
        if(amountToSpawn > amountSpawned)
        {
            StartCoroutine(Spawn());
        }
        else if(amountKilled == amountSpawned)
        {
            Round++;
            amountToSpawn += 3;
            amountKilled = 0;
            amountSpawned = 0;
            spawnDelay = 10.0f;
        }
        
        roundDisplay.SetText(Round + " ");
    }

    IEnumerator Spawn()
    {
        GameObject Enemy = ObjectPool.sharedInstance.getPooledObject();
        if(Enemy != null)
        {
            if(walter)
            {
                Enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp (spawnPoint.transform.position);
                //Enemy.transform.position = spawnPoint.transform.position;
                walter = false;
            }
            else{
                Enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().Warp (spawnPoint2.transform.position);
                //Enemy.transform.position = spawnPoint2.transform.position;
                walter = true;
            }
            Enemy.GetComponent<Enemy>().Health = 90 + (Round*10);
            //Enemy.GetComponent<EnemyMovement>().Revive();
            //Enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            Enemy.SetActive(true);
            Enemy.GetComponent<EnemyMovement>().Revive();
            Enemy.GetComponent<Enemy>().dead = false;
            amountSpawned ++;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
