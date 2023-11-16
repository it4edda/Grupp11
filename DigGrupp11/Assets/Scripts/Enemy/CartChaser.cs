using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CartChaser : EnemyAi
{
    [SerializeField] float power;
    Vector3                launchVector;
    protected override bool Check()
    {
        if (base.Check())
        {
            return false;
        }

        
        launchVector = transform.position - targetToChase.position;
        launchVector = Vector3.Normalize(launchVector);
        
        targetToChase.GetComponent<Rigidbody>().AddForce(launchVector * power, ForceMode.Impulse);
        
        Kill();
        return true;
    }
}
