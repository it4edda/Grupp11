using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> shoppingList = new List<GameObject>();
    
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
        DontDestroyOnLoad(this.gameObject);
    }

    public void CheckShoppingList()
    {
        if (shoppingList.Count <= 0)
        {
            Debug.Log("Win?");
        }
    }
}
