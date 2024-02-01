using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryLogic : MonoBehaviour
{
    [SerializeField] bool canWin;
    public bool CanWin { get => canWin; set => canWin = value; }
    void OnTriggerEnter(Collider other)
    {   
        if (canWin && other.CompareTag("Player")) SceneManager.LoadScene("WinScene");
    }
    
    public void CheckShoppingList()
    {
        ShoppingListUI shoppingListUI = FindObjectOfType<ShoppingListUI>();
        if (shoppingListUI.currentShoppingList.Count(text => text.Complete) >= shoppingListUI.currentShoppingList.Count)
        {
            canWin = true;
        }
        else
        {
            canWin = false;
        }
    }
}
