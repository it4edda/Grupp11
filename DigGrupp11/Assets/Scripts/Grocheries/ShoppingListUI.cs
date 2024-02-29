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
        foreach (ShoppingListItem item in shoppingList)
        {
            GameObject newGrocery = Instantiate(textPrefab, textParent);
            newGrocery.GetComponent<SetShoppingListText>().SetText(item);
            currentShoppingList.Add(newGrocery.GetComponent<SetShoppingListText>());
        }
        SetCosts();
    }

    public void SetCosts()
    {
        
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
