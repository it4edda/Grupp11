using System.Collections.Generic;
using UnityEngine;

public class GroceriesContainer : Interaction
{
    [SerializeField] List<GameObject> pickedUpGroceries = new();
    PlayerHand playerHand;
    protected override void Start()
    {
        base.Start();
        playerHand = FindObjectOfType<PlayerHand>();
    }

    protected override void Update()
    {
        InteractionPassive();

        Debug.Log(target.name);
        bool a = Vector3.Distance(transform.position, target.position) < radius && playerHand.IsHoldingSomething;
        interactIcon.SetBool("Showing", a);
        
        if (canInteract && a)
        {
            if (Input.GetKeyDown(interactKey))
            {
                InteractionActive();
            }
        }
    }

    protected override void InteractionActive()
    {
        DepositGroceries();
    }

    void DepositGroceries()
    {
        pickedUpGroceries.Add(playerHand.RemoveFromHand());
    }
}
