public class Groceries : Interaction
{
    PlayerHand playerHand;
    protected override void Start()
    {
        base.Start();
        canInteract = true;
        playerHand = FindObjectOfType<PlayerHand>();
    }

    protected override void InteractionActive()
    {
        base.InteractionActive();
        GetsPickedUpp();
    }

    void GetsPickedUpp()
    {
        if (GameManager.Instance.shoppingList.Contains(gameObject) && playerHand.AddToHand(gameObject))
        {
            GameManager.Instance.shoppingList.Remove(gameObject);
            gameObject.SetActive(false);
            GameManager.Instance.CheckShoppingList();
        }
    }
}
