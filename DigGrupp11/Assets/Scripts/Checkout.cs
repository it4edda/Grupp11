using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Checkout : MonoBehaviour
{
    [SerializeField] string   cashTag;
    [SerializeField] string   groceryTag;
    [SerializeField] Animator payAnimator;
    [SerializeField] TextMeshProUGUI checkoutText;
    [SerializeField] TextMeshProUGUI coinsInSystemText;
    [SerializeField] List<Groceries> paidGroceriesList = new();
    
    [Header("DONT CHANGE")]
    [SerializeField] List<GameObject> itemsInCheckout    = new List<GameObject>();
    [SerializeField] int  amountNeededInCart = 0;
    [SerializeField] int  amountNeeded       = 0;

    public int AmountNeeded { get => amountNeeded; set => amountNeeded = value; }

    //Temporary until i find a better storage/solution
    int tempScore;
    
    bool canPay       = false;
    int  amountPayed  = 0;
    void Start()
    {
        amountNeeded = 5;
        Debug.Log("need a value for paying" + amountNeeded);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(cashTag))
        {
            //if (!canPay) { Debug.Log("MAX MONEY REACHED"); return; }
            Debug.Log("MONEY TOUCHED ME (Cash-out)");
            amountPayed++;
            if (amountPayed >= amountNeededInCart) BuyItems();
            StartCoroutine(DestroyMoney(other.gameObject));
            Destroy(other);
            UpdateText();
        }
        
        else if (other.CompareTag(groceryTag))
        {
            amountNeededInCart += other.GetComponent<Groceries>().Price;
            itemsInCheckout.Add(other.gameObject);
            if (amountPayed >= amountNeededInCart) BuyItems();
            if (amountNeededInCart >= amountNeeded) payAnimator.SetTrigger("PUShow");
            Debug.Log("GROCERY TOUCHED ME (Cash-out)");
            UpdateText();
        }
    }
    IEnumerator DestroyMoney(GameObject money)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(money);
    }

    void BuyItems()
    {
        foreach (GameObject groceries in itemsInCheckout)
        {
            amountPayed -= groceries.GetComponent<Groceries>().Price;
            tempScore += groceries.GetComponent<Groceries>().Score;
            paidGroceriesList.Add(groceries.GetComponent<Groceries>());
            groceries.SetActive(false);
            amountNeededInCart = 0;
        }

        itemsInCheckout.Clear();
        UpdateText();
    }

    //Useless
    void OpenVictoryCondition()
    {
        Debug.Log("VICTORY CONDITION REACHED");
        canPay                                  = false;
        FindObjectOfType<VictoryLogic>().CanWin = true;
    }
    void OnTriggerExit(Collider other) 
    { 
        if (other.CompareTag(groceryTag))
        {
            amountNeededInCart -= other.GetComponent<Groceries>().Price;
            itemsInCheckout.Remove(other.gameObject);
            UpdateText();
        } 
    }
    void UpdateText()
    {
        checkoutText.text = ("COST     " + amountNeededInCart);
        coinsInSystemText.text = "Coins Inserted: " + amountPayed;
    }
}
//TODO score to grocery/display score when you exit
//TODO Theives enemies that steal money and delivers it to the cash register or something/
//Fix when you take the money the enemy is holding
//TODO myltipickup or only pick up one thing att a time / pickup indication
//TODO Take controll of cart looking angle