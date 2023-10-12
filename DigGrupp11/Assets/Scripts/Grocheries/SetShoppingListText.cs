using TMPro;
using UnityEngine;

public class SetShoppingListText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    
    [SerializeField] ShoppingListItem currentShoppingListItem;

    public void SetText(ShoppingListItem newItem)
    {
        currentShoppingListItem = newItem;
        SetText();
    }
    public void SetText()
    {
        text.text = currentShoppingListItem.item.name + ":" + currentShoppingListItem.amount + "st";
    }
}
