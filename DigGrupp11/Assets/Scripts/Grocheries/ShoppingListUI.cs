using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShoppingListUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI costText;
    
    [SerializeField] GameObject textPrefab;
    [SerializeField] Transform textParent;
    public List<SetShoppingListText> currentShoppingList;

    public void SetUpShoppingListText(List<ShoppingListItem> shoppingList)
    {
        float cumulativeCost = 0;
        foreach (ShoppingListItem item in shoppingList)
        {
            GameObject newGrocery = Instantiate(textPrefab, textParent);
            newGrocery.GetComponent<SetShoppingListText>().SetText(item);
            cumulativeCost += item.item.GetComponent<Groceries>().Price * item.amount;
            currentShoppingList.Add(newGrocery.GetComponent<SetShoppingListText>());
        }
        SetCosts(cumulativeCost);
    }

    public void SetCosts(float cost)
    {
        costText.text = cost.ToString();
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
