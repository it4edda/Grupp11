using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (GameManager.Instance.groceriesToCollect.Contains(gameObject))
        {
            GameManager.Instance.groceriesToCollect.Remove(gameObject);
        }
    }
}
