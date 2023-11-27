using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] float     range;
    [SerializeField] LayerMask mask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] Transform handPivot;
    Animator                   animator;
    Transform                  held;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Pickup();
        if (Input.GetKeyDown(KeyCode.Mouse0)) Attack();
    }
    void Pickup()
    {
        if (!held)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, mask))
            {
                held                     = hit.transform;
                hit.transform.parent     = transform;
                Rigidbody a;
                if (a = held.GetComponent<Rigidbody>())
                    a.isKinematic = true;
                BoxCollider b;
                if (b = held.GetComponent<BoxCollider>())
                    b.isTrigger = true;
                held.GetComponent<Groceries>().SetShadow(true);
                held.GetComponent<Groceries>().isPickedUp = true;
            } 
        }
        else
        {
            held.parent                               = null;
            Rigidbody a;
            if (a = held.GetComponent<Rigidbody>())
                a.isKinematic = false;
            BoxCollider b;
            if (b = held.GetComponent<BoxCollider>())
                b.isTrigger = false;
            held.GetComponent<Groceries>().SetShadow(false);
            held.GetComponent<Groceries>().isPickedUp = false;
            held                                      = null;
        }
            
            
    }
    void Attack()
    {
        handPivot.rotation = quaternion.Euler(handPivot.rotation.x, Random.Range(0, 360), handPivot.rotation.z);
        animator.SetTrigger("");
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitB, range, enemyMask))
        {
            Debug.Log("SLAP");
            hitB.transform.gameObject.GetComponent<EnemyAi>().Attacked();
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
    }
}
