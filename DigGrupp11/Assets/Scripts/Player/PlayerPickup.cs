using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public static event Action<bool, Transform> PickingUpSomething;

    [SerializeField] float     range;
    [SerializeField] LayerMask mask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] Transform handPivot;
    [SerializeField] Animator  handAnimator;
    Transform                  held;

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
                held.GetComponent<Groceries>().GetsPickedUp(true);                  
                held.GetComponent<Groceries>().SetShadow(true);
                PickingUpSomething?.Invoke(true, held);
            } 
        }
        else
        {
            held.parent                               = null;
            held.GetComponent<Groceries>().GetsPickedUp(false);
            held.GetComponent<Groceries>().SetShadow(false);
            held                                      = null;
            PickingUpSomething?.Invoke(false, held);
        }
            
            
    }
    void Attack()
    {
            handPivot.rotation = quaternion.Euler(handPivot.rotation.x, Random.Range(0, 360), handPivot.rotation.z);
            handAnimator.SetTrigger("Slap");
        
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