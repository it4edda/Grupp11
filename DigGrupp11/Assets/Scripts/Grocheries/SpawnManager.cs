using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> availableGroceriesToSpawn;
    public int numberOfDifferentGroceriesToSpawn;
    public MinMax numberOfSameGroceriesToSpawn;
    
    [SerializeField] float repelRange;
    [Tooltip("The size of the sphere checking for nearest transform to initially spawned object of each type")]
    [SerializeField] float sameObjectSpawnRadius;
    [SerializeField, Range(0, 100)] float chanceToRepel;
    [SerializeField] LayerMask groceriesMask;
    [SerializeField] LayerMask spawnPointMask;
    [SerializeField] List<GameObject> spawnPoints = new();
    [SerializeField] List<ShoppingListItem> shoppingList;
    ShoppingListUI shoppingListUI;


    void Start()
    {
        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoint"));
        shoppingList = ShoppingList.Instance.WriteList();
        RestockGroceries(shoppingList);
        shoppingListUI = FindObjectOfType<ShoppingListUI>();
        shoppingListUI.SetUpShoppingListText(shoppingList);
    }

    void RestockGroceries(List<ShoppingListItem> shoppingList)
    {
        foreach (ShoppingListItem shoppingListItem in shoppingList)
        {
            GameObject firstObjectSpawned = null;
            List<Transform> alreadySpawnedList = new ();
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
                        rerollSpawnPoint = Physics.CheckSphere(spawnPoint.position, repelRange, groceriesMask) &&
                                           Random.Range(0, 100) < chanceToRepel;

                        breaker++;
                    } 
                    while (rerollSpawnPoint && breaker < 50);
                    
                    firstObjectSpawned = Instantiate(shoppingListItem.item, spawnPoint);
                    firstObjectSpawned.GetComponent<Groceries>().text = shoppingListItem.item;
                    firstObjectSpawned.GetComponent<Groceries>().spawnPoint = spawnPoint;
                    alreadySpawnedList.Add(spawnPoint);
                    spawnPoints.Remove(spawnPoint.gameObject);
                }
                else
                {
                    Collider[] closeSpawnPointsColliders =
                        Physics.OverlapSphere(firstObjectSpawned.transform.position, sameObjectSpawnRadius, spawnPointMask, QueryTriggerInteraction.Collide);

                    List<Transform> closeSpawnPoints = closeSpawnPointsColliders.Select(closeSpawnPoint => closeSpawnPoint.transform).ToList();
                    foreach (Transform points in alreadySpawnedList.Where(points => closeSpawnPoints.Contains(points)))
                    {
                        closeSpawnPoints.Remove(points);
                    }

                    Transform chosenSpawnPoint = closeSpawnPoints[Random.Range(0, closeSpawnPoints.Count)];
                    alreadySpawnedList.Add(chosenSpawnPoint);
                    GameObject spawnedObject = Instantiate(shoppingListItem.item, chosenSpawnPoint);
                    spawnedObject.GetComponent<Groceries>().text = shoppingListItem.item;
                    spawnedObject.GetComponent<Groceries>().spawnPoint = chosenSpawnPoint;
                }
            }
        }
    }

    public void RemoveGroceryFromList(GameObject item)
    {
        Debug.Log("PickingUpItem");
        SetShoppingListText text = shoppingListUI.currentShoppingList.FirstOrDefault(n => n.CurrentShoppingListItem.item == item);
        text.AmountCollected++;
        text.SetText();
    }

    public void AddGroceryToList(GameObject item)
    {
        Debug.Log("PickingUpItem");
        SetShoppingListText text = shoppingListUI.currentShoppingList.FirstOrDefault(n => n.CurrentShoppingListItem.item == item);
        text.AmountCollected--;
        if (text.AmountCollected <= 0)
        {
            text.AmountCollected = 0;
        }
        text.SetText();
    }
}
