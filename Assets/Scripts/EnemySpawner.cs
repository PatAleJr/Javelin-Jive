using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float nextSpawnTime;
    public float spawnPeriod = 0.5f;
    public Transform[] spawnPoints;

    public GameObject enemyPrefab;

    public int minHealth = 2;
    public int maxHealth = 5;
        
    void Start() {
        nextSpawnTime = Time.time + spawnPeriod;
    }

    void Update()
    {
        if (GameManager.instance.gameState != GameManager.GameState.Play) return;

        if (Time.time >= nextSpawnTime) {
            spawn();
            nextSpawnTime = Time.time + spawnPeriod;
        }   
    }

    void spawn() {
        int spawnPositionIndex = (int)Random.Range(0, spawnPoints.Length);
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.position = spawnPoints[spawnPositionIndex].position;
        newEnemy.GetComponent<Enemy>().health = (int)Random.Range(minHealth, maxHealth+1);
    }
}
