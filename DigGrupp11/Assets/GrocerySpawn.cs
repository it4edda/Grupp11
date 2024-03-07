using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum ShelfType
{
    Shelf,
    Fridge
}

public class GrocerySpawn : MonoBehaviour
{
    public ShelfType shelfType;
    public bool available = true;
}
