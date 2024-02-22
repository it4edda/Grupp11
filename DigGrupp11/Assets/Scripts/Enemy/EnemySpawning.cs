using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] float intervalBetweenSpawns = 10f;
    [SerializeField] int spawnsBetweenIncreases = 3;
    [SerializeField] int increaseInSpawns = 2;
    [SerializeField] int baseEnemiesToSpawn = 1;
    [SerializeField] List<GameObject> enemiesToSpawn = new();
    [SerializeField] List<Transform> enemySpawnPoints = new();

    int baseSpawnsBetweenIncreases;

    void Start()
    {
        baseSpawnsBetweenIncreases = spawnsBetweenIncreases;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < baseEnemiesToSpawn; i++)
        {
            Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)],
                enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)]);
        }

        
        spawnsBetweenIncreases--;
        if (spawnsBetweenIncreases <= 0)
        {
            baseEnemiesToSpawn += increaseInSpawns;
            spawnsBetweenIncreases = baseSpawnsBetweenIncreases;
        }
        yield return new WaitForSeconds(intervalBetweenSpawns);
        StartCoroutine(SpawnEnemies());
    }
}
