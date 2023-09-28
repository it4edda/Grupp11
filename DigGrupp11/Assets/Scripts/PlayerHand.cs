using System;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] GameObject playerInventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject thing = RemoveFromHand();
        }
    }

    public bool AddToHand(GameObject objectToAdd)
    {
        if (playerInventory != null) return false;
        
        playerInventory = objectToAdd;
        return true;
    }

    public GameObject RemoveFromHand()
    {
        GameObject playerHandObject = playerInventory;
        playerInventory = null;
        return playerHandObject;
    }
}
