using System;
using TMPro;
using UnityEngine;

public class SetShoppingListText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    
    [SerializeField] ShoppingListItem currentShoppingListItem;
    public ShoppingListItem CurrentShoppingListItem => currentShoppingListItem;
    [SerializeField] int amountCollected;
    public int AmountCollected { get => amountCollected; set => amountCollected = value; }
    public bool Complete { get; private set; }

    VictoryLogic victoryLogic;

    void Start()
    {
        victoryLogic = FindObjectOfType<VictoryLogic>();
    }

    public void SetText(ShoppingListItem newItem)
    {
        currentShoppingListItem = newItem;
        SetText();
    }
    public void SetText()
    {
        text.text = currentShoppingListItem.item.name + ":" + (currentShoppingListItem.amount - amountCollected) + "st";
        if (currentShoppingListItem.amount - amountCollected <= 0)
        {
            text.fontStyle = FontStyles.Strikethrough;
            Debug.Log("done");
            Complete = true;
        }
        else
        {
            text.fontStyle = FontStyles.Normal;
            Complete = false;
        }

        if (victoryLogic)
        {
            victoryLogic.CheckShoppingList();
        }
    }
}
