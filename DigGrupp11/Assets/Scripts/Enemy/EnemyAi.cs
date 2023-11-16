using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : Interaction
{
    [Header("Enemy")]
    [SerializeField]           float     speed;
    [SerializeField] protected Transform targetToChase;
    [SerializeField]           float     distance = 1;

    void Update()
    {
        Movement();
        Check();
    }
    protected virtual void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetToChase.position, speed * Time.deltaTime);
        
    }
    protected virtual bool Check()
    { 
        bool  returnVal = (Vector3.Distance(targetToChase.position, transform.position) < distance);
        return returnVal;
    }

    protected virtual void Kill()
    {
        Destroy(gameObject);
    }
    protected override void InteractionActive()
    {
        base.InteractionActive();
        Kill();
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    //one that fucks with progress, cart, player
    
    //make ghosts slap-able???????
}

