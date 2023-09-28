using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float repelRange;
    [SerializeField, Range(0, 100)] float chanceToRepel;
    [SerializeField] private LayerMask groceriesMask;
    [SerializeField] GameObject[] availableGroceriesToSpawn;
    [SerializeField] int numberOfGroceriesToSpawn;
    [SerializeField] List<GameObject> spawnPoints = new();

    void Start()
    {
        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));

        RestockGroceries();
    }
    //TODO not spawning to close to each other
    void RestockGroceries()
    {
        numberOfGroceriesToSpawn = numberOfGroceriesToSpawn > spawnPoints.Count ? spawnPoints.Count : numberOfGroceriesToSpawn;
        for (int i = 0; i < numberOfGroceriesToSpawn; i++)
        {
            Transform spawnPoint;
            bool rerollSpawnPoint;
            int breaker = 0;

            do
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
                if (Physics.CheckSphere(spawnPoint.position, repelRange, groceriesMask))
                {
                    rerollSpawnPoint = Random.Range(0, 100) < chanceToRepel;
                    Debug.Log("rolls");
                }
                else
                {
                    rerollSpawnPoint = false;
                    Debug.Log("DoesNotRe-roll");
                }

                breaker++;
            } 
            while (rerollSpawnPoint && breaker < 50);
            
            GameObject groceriesToSpawn = availableGroceriesToSpawn[Random.Range(0, availableGroceriesToSpawn.Length)];
            GameObject groceriesThatSpawned = Instantiate(groceriesToSpawn, spawnPoint);
            GameManager.Instance.shoppingList.Add(groceriesThatSpawned);
            spawnPoints.Remove(spawnPoint.gameObject);
        }
    }
}
