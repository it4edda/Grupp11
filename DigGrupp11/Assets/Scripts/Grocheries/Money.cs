using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Interaction
{
    protected override void InteractionActive()
    {
        base.InteractionActive();
        FindObjectOfType<PlayerMoney>().CurrentMoney++;
        Destroy(gameObject);
    }
}
