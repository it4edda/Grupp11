using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] protected KeyCode   interactKey; 
    [SerializeField] protected float     radius;
    [SerializeField] protected bool      canInteract;
    [SerializeField]           bool      hasIcon = true;
    [SerializeField] protected Animator  interactIcon;
    [SerializeField]           bool      canMultiPickup = false;
    protected                  Transform target;
    public                     bool      isBeingLookedAt = false;
    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (hasIcon) interactIcon.SetBool("Showing", false);

    }
    protected virtual void Update()
    {
        InteractionPassive();

        bool inRange = Vector3.Distance(transform.position, target.position) < radius;
        
        Debug.Log(hasIcon + " " + canMultiPickup);
        if (hasIcon && canMultiPickup) interactIcon.SetBool("Showing", inRange);
        else if(inRange) interactIcon.SetBool("Showing", isBeingLookedAt); 
        
        if (canInteract && inRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                InteractionActive();
            }
        }
    }
    protected virtual void InteractionPassive()
    {
        
    }
    protected virtual void InteractionActive()
    {
        canInteract = false;
    }
    
    void OnDrawGizmos()
    {
     Gizmos.color = Color.red;
     Gizmos.DrawWireSphere(transform.position, radius);
    }
}
