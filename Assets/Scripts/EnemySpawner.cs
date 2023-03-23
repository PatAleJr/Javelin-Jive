using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float nextSpawnTime;
    public float spawnPeriod = 0.5f;
    public bool isSpawning = false;
    public Transform[] spawnPoints;

    public GameObject enemyPrefab;

    void Start()
    {
        nextSpawnTime = Time.time + spawnPeriod;
    }

    void Update()
    {
        if (isSpawning)
        {
            if (Time.time >= nextSpawnTime)
            {
                spawn();
                nextSpawnTime = Time.time + spawnPeriod;
            }
        }
    }

    void spawn()
    {
        int spawnPositionIndex = (int)Random.Range(0, spawnPoints.Length);
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = spawnPoints[spawnPositionIndex].position;
    }
}
