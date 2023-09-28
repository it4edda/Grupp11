public class Groceries : Interaction
{
    protected override void Start()
    {
        base.Start();
        canInteract = true;
    }

    protected override void InteractionActive()
    {
        base.InteractionActive();
        GetsPickedUpp();
    }

    void GetsPickedUpp()
    {
        if (GameManager.Instance.shoppingList.Contains(gameObject))
        {
            GameManager.Instance.shoppingList.Remove(gameObject);
            gameObject.SetActive(false);
            GameManager.Instance.CheckShoppingList();
        }
    }
}
