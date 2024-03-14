using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct ShoppingListItem
{ 
    public GameObject item;
    public int amount;
    public ShelfType shelfType;

    public void DecreaseAmount(int amountToIncrease)
    {
        amount += amountToIncrease;
    }
}

public class ShoppingList : MonoBehaviour
{
    public static ShoppingList Instance;
    
    [SerializeField] List<ShoppingListItem> shoppingList = new();

    int cumulativePrice;
    
    SpawnManager spawnManager;
    
    void Awake()
    {
        Instance = this;
    }

    //TODO fix this shit
    //Make it so objects can only spawn in their designated shelves
    public List<ShoppingListItem> WriteList()
    {
        cumulativePrice = 0;
        shoppingList.Clear();
        spawnManager = FindObjectOfType<SpawnManager>();
        List<GameObject> tempGroceriesAvailable = spawnManager.availableGroceriesToSpawn;
        for (int i = 0; i < spawnManager.numberOfDifferentGroceriesToSpawn; i++)
        {
            GameObject theChosenObject = tempGroceriesAvailable[Random.Range(0, tempGroceriesAvailable.Count)];
            tempGroceriesAvailable.Remove(theChosenObject);
            ShoppingListItem newItem;
            newItem.item = theChosenObject;
            newItem.amount = spawnManager.numberOfSameGroceriesToSpawn.GetRandom();
            newItem.shelfType = theChosenObject.GetComponent<Groceries>().Type;
            shoppingList.Add(newItem);
            cumulativePrice += theChosenObject.GetComponent<Groceries>().Price;
            Checkout checkout = FindObjectOfType<Checkout>();
            checkout.AmountNeeded = cumulativePrice;
        }
        return shoppingList;
    }
}
