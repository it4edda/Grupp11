using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct ShoppingListItem
{ 
    public GameObject item;
    public int amount;
}

public class ShoppingList : MonoBehaviour
{
    public static ShoppingList Instance;
    
    [SerializeField] List<ShoppingListItem> shoppingList = new();

    void Awake()
    {
        Instance = this;
    }

    public List<ShoppingListItem> WriteList()
    {
        shoppingList.Clear();
        List<GameObject> tempGroceriesAvailable = GameManager.Instance.availableGroceriesToSpawn;
        for (int i = 0; i < GameManager.Instance.numberOfDifferentGroceriesToSpawn; i++)
        {
            GameObject theChosenObject = tempGroceriesAvailable[Random.Range(0, tempGroceriesAvailable.Count)];
            tempGroceriesAvailable.Remove(theChosenObject);
            ShoppingListItem newItem;
            newItem.item = theChosenObject;
            newItem.amount = GameManager.Instance.numberOfSameGroceriesToSpawn.GetRandom();
            shoppingList.Add(newItem);
        }
        return shoppingList;
    }
}
