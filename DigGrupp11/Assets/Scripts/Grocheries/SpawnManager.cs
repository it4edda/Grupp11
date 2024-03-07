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
    [SerializeField] List<GrocerySpawnPoint> spawnPoints = new();
    [SerializeField] List<ShoppingListItem> shoppingList;
    ShoppingListUI shoppingListUI;


    void Start()
    {
        shoppingList = ShoppingList.Instance.WriteList();
        RestockMandatoryGroceries(shoppingList);
        shoppingListUI = FindObjectOfType<ShoppingListUI>();
        shoppingListUI.SetUpShoppingListText(shoppingList);
    }

    //TODO add more stuff
    void RestockMandatoryGroceries(List<ShoppingListItem> shoppingList)
    {
        foreach (ShoppingListItem shoppingListItem in shoppingList)
        {
            GameObject firstObjectSpawned = null;
            spawnPoints = new List<GrocerySpawnPoint>(FindObjectsOfType<GrocerySpawnPoint>()
                .Where(point => point.available && point.shelfType == shoppingListItem.shelfType));
            for (int i = 0; i < shoppingListItem.amount; i++)
            {
                if (i == 0)
                {
                    GrocerySpawnPoint spawnPoint;
                    bool rerollSpawnPoint;
                    int breaker = 0;

                    do
                    {
                        spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                        rerollSpawnPoint = Physics.CheckSphere(spawnPoint.transform.position, repelRange, groceriesMask) &&
                                           Random.Range(0, 100) < chanceToRepel;

                        breaker++;
                    } 
                    while (rerollSpawnPoint && breaker < 50);
                    
                    firstObjectSpawned = Instantiate(shoppingListItem.item, spawnPoint.transform);
                    Groceries groceries = firstObjectSpawned.GetComponent<Groceries>();
                    
                    groceries.text = shoppingListItem.item;
                    groceries.spawnPoint = spawnPoint.transform;
                    spawnPoint.available = false;
                }
                else
                {
                    Collider[] closeSpawnPointsColliders =
                        Physics.OverlapSphere(firstObjectSpawned.transform.position, sameObjectSpawnRadius, spawnPointMask, QueryTriggerInteraction.Collide);

                    List<GrocerySpawnPoint> closeSpawnPoints = closeSpawnPointsColliders.Select(closeSpawnPoint => closeSpawnPoint.GetComponent<GrocerySpawnPoint>()).ToList();
                    foreach (GrocerySpawnPoint points in closeSpawnPoints.Where(point => point.available == false && point.shelfType != shoppingListItem.shelfType))
                    {
                        closeSpawnPoints.Remove(points);
                        if (closeSpawnPoints.Count <= 0)
                        {
                            break;
                        }
                    }

                    GrocerySpawnPoint chosenSpawnPoint = closeSpawnPoints[Random.Range(0, closeSpawnPoints.Count)];
                    GameObject spawnedObject = Instantiate(shoppingListItem.item, chosenSpawnPoint.transform);
                    chosenSpawnPoint.available = false;
                    spawnedObject.GetComponent<Groceries>().text = shoppingListItem.item;
                    spawnedObject.GetComponent<Groceries>().spawnPoint = chosenSpawnPoint.transform;
                }
            }
        }
    }

    void RestockBackgroundGroceries()
    {
        
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
