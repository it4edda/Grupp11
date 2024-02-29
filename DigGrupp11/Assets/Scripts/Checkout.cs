using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkout : MonoBehaviour
{
    [SerializeField] LayerMask cashMask;
    [SerializeField] LayerMask groceryMask; 
    
    int  amountPayed  = 0;
    int  amountNeeded = 0;
    void Start()
    {
        amountNeeded = 5;
        Debug.Log("need a value for paying" + amountNeeded);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == cashMask)
        {
            amountPayed++;
            Destroy(other);
        }
        
        else if (other.gameObject.layer == groceryMask)
        {
            
        }
    }
    void OnCollisionExit(Collision other) 
    { 
        if (other.gameObject.layer == groceryMask)
        {
            
        } 
    }
}
