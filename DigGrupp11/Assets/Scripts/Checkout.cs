using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkout : MonoBehaviour
{
    [SerializeField] string   cashTag;
    [SerializeField] string   groceryTag;
    [SerializeField] Animator payAnimator;

    [Header("DONT CHANGE")]
    [SerializeField] List<GameObject> itemsInCheckout    = new List<GameObject>();
    [SerializeField] int              amountNeededInCart = 0;

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
        if (other.CompareTag(cashTag))
        {
            if (!canPay) { Debug.Log("MAX MONEY REACHED"); return; }
            Debug.Log("MONEY TOUCHED ME (Cash-out)");
            amountPayed++;
            if (amountPayed >= amountNeeded) OpenVictoryCondition();
            StartCoroutine(DestroyMoney(other.gameObject));
            Destroy(other);
        }
        
        else if (other.CompareTag(groceryTag))
        {
            amountNeededInCart += other.GetComponent<Groceries>().Price;
            itemsInCheckout.Add(other.gameObject);
            if (amountNeededInCart >= amountNeeded) payAnimator.SetTrigger("PUShow");
            Debug.Log("GROCERY TOUCHED ME (Cash-out)");
        }
    }
    IEnumerator DestroyMoney(GameObject money)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(money);
    }
    void OpenVictoryCondition()
    {
        Debug.Log("VICTORY CONDITION REACHED");
        canPay = false;
    }
    void OnTriggerExit(Collider other) 
    { 
        if (other.CompareTag(groceryTag))
        {
            amountNeededInCart -= other.GetComponent<Groceries>().Price;
            itemsInCheckout.Remove(other.gameObject);
        } 
    }
}
