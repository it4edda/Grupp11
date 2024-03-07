using System.Collections;
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
        Debug.Log("SOMETHING IS IS ON ME (Cash-out)");
        
        if (other.CompareTag(cashTag))
        {
            Debug.Log("MONEY IS IS ON ME (Cash-out)");
            amountPayed++;
            if (amountPayed >= amountNeeded) canPay = true;
            StartCoroutine(DestroyMoney(other.gameObject));
            Destroy(other);
        }
        
        else if (other.CompareTag(groceryTag))
        {
            payAnimator.SetTrigger("PUShow");
            Debug.Log("GROCERY TOUCHED ME (Cash-out)");
        }
    }
    IEnumerator DestroyMoney(GameObject money)
    {
        yield return new WaitForSeconds(1);
        Destroy(money);
    }
    void OnTriggerExit(Collider other) 
    { 
        if (other.CompareTag(groceryTag))
        {
            
        } 
    }
}
