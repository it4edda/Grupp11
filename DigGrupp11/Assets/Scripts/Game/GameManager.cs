using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> availableGroceriesToSpawn;
    public int numberOfDifferentGroceriesToSpawn;
    public MinMax numberOfSameGroceriesToSpawn;

    void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            Instance = this; 
        }
    }

    public void CheckShoppingList()
    {
        ShoppingListUI shoppingListUI = FindObjectOfType<ShoppingListUI>();
        if (shoppingListUI.currentShoppingList.Count(text => text.Complete == true) >= shoppingListUI.currentShoppingList.Count)
        {
            
        }
    }
}
