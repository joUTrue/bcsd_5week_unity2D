using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPos;

    public GameObject player;

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
        int ranPoint = Random.Range(0, spawnPos.Length);

       GameObject  enem =  Instantiate(enemyObjs[ranEnemy], spawnPos[ranPoint].position, spawnPos[ranPoint].rotation);

        Rigidbody2D rigidbody2D = enem.GetComponent<Rigidbody2D>();
        Enemy enemyL = enem.GetComponent<Enemy>();
        enemyL.player = player;

        if(ranPoint == 5||ranPoint == 7)
        {
            enem.transform.Rotate(Vector3.back * 90);
            rigidbody2D.velocity = new Vector2(enemyL.speed , -1);
        }
        else if (ranPoint == 6 || ranPoint == 8)
        {
            enem.transform.Rotate(Vector3.forward * 90);
            rigidbody2D.velocity = new Vector2(-enemyL.speed , -1);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, -enemyL.speed);
        }
    }

    public void RespwanPlayer()
    {
        Invoke("RespwanPlayerExe", 2);
    }
    public void RespwanPlayerExe()
    {
        player.transform.position = Vector3.down * (3.5f);
        player.SetActive(true);
    }
}
