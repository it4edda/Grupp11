using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShoppingListUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textPrefab;
    [SerializeField] Transform textParent;
    List<TextMeshProUGUI> currentShoppingList;

    public void SetUpShoppingListText(List<ShoppingListItem> shoppingList)
    {
        foreach (ShoppingListItem item in shoppingList)
        {
            TextMeshProUGUI newGrocery = Instantiate(textPrefab, textParent);
            newGrocery.GetComponent<SetShoppingListText>().SetText(item);
        }
    }
}
