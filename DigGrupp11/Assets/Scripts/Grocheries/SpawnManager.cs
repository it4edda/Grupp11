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

    GameObject firstObjectSpawned = null;
    void Start()
    {
        shoppingList = ShoppingList.Instance.WriteList();
        RestockMandatoryGroceries(shoppingList);
        RestockBackgroundGroceries();
        shoppingListUI = FindObjectOfType<ShoppingListUI>();
        shoppingListUI.SetUpShoppingListText(shoppingList);
    }

    //TODO add more stuff
    void RestockMandatoryGroceries(List<ShoppingListItem> shoppingList)
    {
        foreach (ShoppingListItem shoppingListItem in shoppingList)
        {
            firstObjectSpawned = null;
            ShelfType itemShelfType = shoppingListItem.shelfType;
            GameObject item = shoppingListItem.item;
            spawnPoints = new List<GrocerySpawnPoint>(FindObjectsOfType<GrocerySpawnPoint>()
                .Where(point => point.available && point.shelfType == shoppingListItem.shelfType));
            for (int i = 0; i < shoppingListItem.amount; i++)
            {
                SpawningGroceries(i, item, itemShelfType, true);
            }
        }
    }
    
    void RestockBackgroundGroceries()
    {
        List<GrocerySpawnPoint> availableSpawns =
            new List<GrocerySpawnPoint>(FindObjectsOfType<GrocerySpawnPoint>().Where(point => point.available));
        for (int i = 0; i < availableSpawns.Count / 10; i++)
        {
            firstObjectSpawned = null;
            GameObject objectToSpawn = availableGroceriesToSpawn[Random.Range(0, availableGroceriesToSpawn.Count)];
            int amountOfBackgroundObjects = Random.Range(1, 6);
            spawnPoints = new List<GrocerySpawnPoint>(FindObjectsOfType<GrocerySpawnPoint>()
                .Where(point => point.available && point.shelfType == objectToSpawn.GetComponent<Groceries>().Type));
            for (int j = 0; j < amountOfBackgroundObjects; j++)
            {
                SpawningGroceries(j, objectToSpawn, objectToSpawn.GetComponent<Groceries>().Type, false);
            }
        }
    }

    void SpawningGroceries(int i, GameObject item, ShelfType shelfType, bool repel)
    {
        Debug.Log(i);
        if (i == 0)
        {
            GrocerySpawnPoint spawnPoint;
            bool rerollSpawnPoint;
            int breaker = 0;

            do
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                rerollSpawnPoint = repel &&
                                   Physics.CheckSphere(spawnPoint.transform.position, repelRange, groceriesMask) &&
                                   Random.Range(0, 100) < chanceToRepel;
                breaker++;
            } while (rerollSpawnPoint && breaker < 50);

            firstObjectSpawned = Instantiate(item, spawnPoint.transform);
            Groceries groceries = firstObjectSpawned.GetComponent<Groceries>();

            groceries.text = item;
            groceries.spawnPoint = spawnPoint.transform;
            spawnPoint.available = false;
        }
        else
        {
            Debug.Log(firstObjectSpawned);
            Collider[] closeSpawnPointsColliders =
                Physics.OverlapSphere(firstObjectSpawned.transform.position, sameObjectSpawnRadius, spawnPointMask);

            List<GrocerySpawnPoint> closeSpawnPoints = closeSpawnPointsColliders
                .Select(closeSpawnPoint => closeSpawnPoint.GetComponent<GrocerySpawnPoint>()).Where(point => point.available && point.shelfType == shelfType).ToList();
            if (closeSpawnPoints.Count(point => point.available && point.shelfType == shelfType) <= 0) { return;}
            foreach (GrocerySpawnPoint points in closeSpawnPoints.Where(point =>
                         point.available == false && point.shelfType != shelfType))
            {
                closeSpawnPoints.Remove(points);
                if (closeSpawnPoints.Count <= 0)
                {
                    return;
                }
            }
            
            GrocerySpawnPoint chosenSpawnPoint = closeSpawnPoints[Random.Range(0, closeSpawnPoints.Count)];
            GameObject spawnedObject = Instantiate(item, chosenSpawnPoint.transform);
            chosenSpawnPoint.available = false;
            spawnedObject.GetComponent<Groceries>().text = item;
            spawnedObject.GetComponent<Groceries>().spawnPoint = chosenSpawnPoint.transform;
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
