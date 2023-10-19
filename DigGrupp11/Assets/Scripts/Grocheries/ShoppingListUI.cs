using System.Collections.Generic;
using UnityEngine;

public class ShoppingListUI : MonoBehaviour
{
    [SerializeField] GameObject textPrefab;
    [SerializeField] Transform textParent;
    public List<SetShoppingListText> currentShoppingList;

    public void SetUpShoppingListText(List<ShoppingListItem> shoppingList)
    {
        foreach (ShoppingListItem item in shoppingList)
        {
            GameObject newGrocery = Instantiate(textPrefab, textParent);
            newGrocery.GetComponent<SetShoppingListText>().SetText(item);
            currentShoppingList.Add(newGrocery.GetComponent<SetShoppingListText>());
        }
    }

    public GameObject TestShoppingList(GameObject gameObject)
    {
        GameObject textWithRightItem = null;
        foreach (SetShoppingListText listText in currentShoppingList)
        {
            if (listText.CurrentShoppingListItem.item == gameObject)
            {
                textWithRightItem = listText.gameObject;
            }
        }

        return textWithRightItem;
    }
}
