using UnityEngine;

public class Groceries : Interaction
{
    PlayerHand playerHand;
    protected override void Start()
    {
        base.Start();
        canInteract = true;
        playerHand = FindObjectOfType<PlayerHand>();
    }

    protected override void Update()
    {
        InteractionPassive();

        Debug.Log(target.name);
        bool a = Vector3.Distance(transform.position, target.position) < radius && !playerHand.IsHoldingSomething;
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
        GetsPickedUpp();
    }

    void GetsPickedUpp()
    {
        // if (GameManager.Instance.shoppingList.Contains(gameObject) && playerHand.AddToHand(gameObject))
        // {
        //     GameManager.Instance.shoppingList.Remove(gameObject);
        //     gameObject.SetActive(false);
        //     GameManager.Instance.CheckShoppingList();
        //     canInteract = false;
        // }
    }
}
