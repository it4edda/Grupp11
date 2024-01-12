using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaser : EnemyAi
{
    [SerializeField] int damage = 1;
    protected override void Start()
    {
        base.Start();
        targetToChase = FindObjectOfType<TempP>().transform;
    }

    protected override bool Check()
    {
        if (!base.Check())
        {
            return false;
        }
        targetToChase.GetComponent<PlayerHealth>().TakeDamage(damage);
        Kill();
        return true;
    }
}
