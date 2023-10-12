using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] float     range;
    [SerializeField] LayerMask mask;
    Transform                  held;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }
    void Pickup()
    {
        if (!held)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, mask))
            {
                held                     = hit.transform;
                hit.transform.parent     = transform;
                hit.rigidbody.useGravity = false;
            } 
                
        }
        else
        {
            held.parent                               = null;
            held.GetComponent<Rigidbody>().useGravity = true;  
            held                                      = null;
            
        }
            
            
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
    }
}
