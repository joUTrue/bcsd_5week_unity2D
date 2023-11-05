using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPos;

    public float maxSpawnDelay = 10;
    public float curSpawnDelay = 0;

    private void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay )
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 3f);
            curSpawnDelay = 0;
        }
    }

    void SpawnEnemy()
    {
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 5);

        Instantiate(enemyObjs[ranEnemy], spawnPos[ranPoint].position, spawnPos[ranPoint].rotation);
    }
}
