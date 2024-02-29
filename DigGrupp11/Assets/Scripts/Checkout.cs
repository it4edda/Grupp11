using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkout : MonoBehaviour
{
    [SerializeField] string   cashTag;
    [SerializeField] string   groceryTag;
    [SerializeField] Animator payAnimator;

    bool canPay       = false;
    int  amountPayed  = 0;
    int  amountNeeded = 0;
    void Start()
    {
        amountNeeded = 5;
        Debug.Log("need a value for paying" + amountNeeded);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OTHER IS ON ME");
        
        if (other.CompareTag(cashTag))
        {
            amountPayed++;
            if (amountPayed >= amountNeeded) canPay = true;
            Destroy(other);
        }
        
        else if (other.CompareTag(groceryTag))
        {
            payAnimator.SetTrigger("PUShow");
            Debug.Log("HAHAH");
        }
    }
    void OnTriggerExit(Collider other) 
    { 
        if (other.CompareTag(groceryTag))
        {
            
        } 
    }
}
