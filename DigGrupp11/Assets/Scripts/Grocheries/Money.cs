using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Interaction
{
    [SerializeField] bool canBePickedUp = true;
    protected override void InteractionActive()
    {
        base.InteractionActive();

        if (canBePickedUp)
        {
            FindObjectOfType<PlayerMoney>().CurrentMoney++;
            Destroy(gameObject);
        }
    }
}
