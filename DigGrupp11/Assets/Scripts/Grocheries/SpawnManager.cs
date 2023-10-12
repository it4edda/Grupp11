using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float repelRange;
    [Tooltip("The size of the sphere checking for nearest transform to initially spawned object of each type")]
    [SerializeField] float sameObjectSpawnRadius;
    [SerializeField, Range(0, 100)] float chanceToRepel;
    [SerializeField] LayerMask groceriesMask;
    [SerializeField] LayerMask spawnPointMask;
    [SerializeField] List<GameObject> spawnPoints = new();
    [SerializeField] List<ShoppingListItem> shoppingList;


    void Start()
    {
        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        shoppingList = ShoppingList.Instance.WriteList();
        RestockGroceries(shoppingList);
        FindObjectOfType<ShoppingListUI>().SetUpShoppingListText(shoppingList);
        
    }
    void RestockGroceries(List<ShoppingListItem> shoppingList)
    {
        foreach (ShoppingListItem shoppingListItem in shoppingList)
        {
            GameObject firstObjectSpawned = null;
            for (int i = 0; i < shoppingListItem.amount; i++)
            {
                if (i == 0)
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
                    
                    firstObjectSpawned = Instantiate(shoppingListItem.item, spawnPoint);
                    spawnPoints.Remove(spawnPoint.gameObject);
                }
                else
                {
                    Collider[] closeSpawnPointsColliders =
                        Physics.OverlapSphere(firstObjectSpawned.transform.position, sameObjectSpawnRadius, spawnPointMask, QueryTriggerInteraction.Collide);

                    List<Transform> closeSpawnPoints = closeSpawnPointsColliders.Select(closeSpawnPoint => closeSpawnPoint.transform).ToList();

                    Instantiate(shoppingListItem.item, closeSpawnPoints[Random.Range(0, closeSpawnPoints.Count)]);
                }
            }
        }
    }

    public void RemoveGroceryFromList(GameObject item)
    {
        Debug.Log("PickingUpItem");
        shoppingList.Find(listItem => listItem.item == item).DecreaseAmount(-1);
    }
}
