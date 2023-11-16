using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaser : EnemyAi
{
    protected override bool Check()
    {
        if (!base.Check())
        {
            return false;
        }
        Debug.Log("i have damaged the player");
        Kill();
        return true;
    }
}
