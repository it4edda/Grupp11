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
                Rigidbody a;
                if (a = held.GetComponent<Rigidbody>())
                    a.isKinematic = true;
                BoxCollider b;
                if (b = held.GetComponent<BoxCollider>())
                    b.isTrigger = true;
                held.GetComponent<Groceries>().SetShadow(true);
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
            held                                      = null;
        }
            
            
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
    }
}
