using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryLogic : MonoBehaviour
{
    [SerializeField] bool canWin;
    public bool CanWin { get => canWin; set => canWin = value; }
    void OnTriggerEnter(Collider other)
    {   
        if (canWin && other.CompareTag("Player")) FindObjectOfType<SceneLoader>().LoadSceneWithString("WinScene");
    }
    
    // public void CheckShoppingList()
    // {
    //     ShoppingListUI shoppingListUI = FindObjectOfType<ShoppingListUI>();
    //     canWin = shoppingListUI.currentShoppingList.Count(text => text.Complete) >= shoppingListUI.currentShoppingList.Count;
    // }
}
