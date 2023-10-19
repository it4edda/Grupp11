using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShoppingListUI : MonoBehaviour
{
    [SerializeField] GameObject textPrefab;
    [SerializeField] Transform textParent;
    List<SetShoppingListText> currentShoppingList;

    public void SetUpShoppingListText(List<ShoppingListItem> shoppingList)
    {
        foreach (ShoppingListItem item in shoppingList)
        {
            GameObject newGrocery = Instantiate(textPrefab, textParent);
            newGrocery.GetComponent<SetShoppingListText>().SetText(item);
            //currentShoppingList.Add(newGrocery.GetComponent<SetShoppingListText>());
        }
    }
}
