using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] protected KeyCode   interactKey; 
    [SerializeField] protected float     radius;
    [SerializeField] protected bool      canInteract;
    [SerializeField]           bool      hasIcon = true;
    [SerializeField] protected Animator  interactIcon;
    protected                  Transform target;
    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (hasIcon) interactIcon.SetBool("Showing", false);

    }
    protected virtual void Update()
    {
        InteractionPassive();

        Debug.Log(target.name);
        bool a = Vector3.Distance(transform.position, target.position) < radius;
        if (hasIcon) interactIcon.SetBool("Showing", a);
        
        if (canInteract && a)
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
