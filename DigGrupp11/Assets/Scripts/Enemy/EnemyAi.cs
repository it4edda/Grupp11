using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField]           float     speed;
    [SerializeField] protected Transform targetToChase;
    [SerializeField]           float     distance    = 1;
    [SerializeField]           float     maxVelocity = 5;
    bool                                 canMove     = true;
    Rigidbody                            rb;
    Vector3                              movementVector = Vector3.zero;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        movementVector = targetToChase.position - transform.position;
        movementVector = Vector3.Normalize(movementVector);
        Movement();
        Check();
    }
    protected virtual void Movement()
    {
        if (!canMove) return;
            rb.AddForce(movementVector * speed, ForceMode.Force);
    }
    
    protected virtual bool Check()
    { 
        bool  returnVal = (Vector3.Distance(targetToChase.position, transform.position) < distance);
        return returnVal;
    }

    [SerializeField] float knockbackPower = 5f;
    int                    health         = 3;
    public virtual void Attacked()
    {
        StartCoroutine(Yield());
        if (--health < 1) Kill();
        else Knockback();
    }
    IEnumerator Yield()
    {
        canMove = false;
        yield return new WaitForSeconds(1);
        canMove = !canMove;
    }
    protected virtual void Knockback()
    {
        Debug.Log( transform.name                    + " damaged");
        rb.AddForce(-movementVector * knockbackPower + Vector3.up * 2, ForceMode.VelocityChange);
    }
    protected virtual void Kill()
    {
        Destroy(gameObject);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    //one that fucks with progress, cart, player
    
    //make ghosts slap-able???????
}

