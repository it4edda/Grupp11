using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] UnityEvent onInteractEvent;
    [SerializeField] float radius;
    [SerializeField] protected bool canInteract;
    [SerializeField] bool hasActivated;
    [SerializeField] Animator interactIcon;
    Transform target;
    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        InteractionPassive();

        canInteract = Vector2.Distance(transform.position, target.position) < radius;
        
        if (canInteract && !hasActivated)
        {
            interactIcon.SetBool("Showing", canInteract);
            if (Input.GetKeyDown(KeyCode.Space))
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
        hasActivated = true;
        onInteractEvent?.Invoke();
    }
    void OnDrawGizmosSelected()
    {
     Gizmos.color = Color.red;
     Gizmos.DrawWireSphere(transform.position, radius);
    }
}

