using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] float intervalBetweenSpawns = 10f;
    [SerializeField] int increaseInSpawnsBetweenSpawns = 2;
    [SerializeField] int numberOfEnemiesToSpawn = 1;
    [SerializeField] List<GameObject> enemiesToSpawn = new();
    [SerializeField] List<Transform> enemySpawnPoints = new();

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)],
                enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)]);
        }

        numberOfEnemiesToSpawn += increaseInSpawnsBetweenSpawns;
        yield return new WaitForSeconds(intervalBetweenSpawns);
        StartCoroutine(SpawnEnemies());
    }
}
