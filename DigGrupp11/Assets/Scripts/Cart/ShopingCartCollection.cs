using UnityEngine;

public class ShopingCartCollection : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Groceries"))
        {
            Debug.Log(other.gameObject);
            FindObjectOfType<SpawnManager>().RemoveGroceryFromList(other.gameObject);
        }
    }
}
