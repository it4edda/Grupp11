using System;
using System.Collections.Generic;
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
        DontDestroyOnLoad(this.gameObject);
    }

    public void CheckShoppingList()
    {
        
    }
}
