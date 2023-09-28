using UnityEngine;
using UnityEngine.Serialization;

public class Interaction : MonoBehaviour
{
    [SerializeField] KeyCode interactKey; 
    [SerializeField] float          radius;
    [SerializeField] protected bool canInteract;
    [SerializeField] Animator       interactIcon;
    Transform                       target;
    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        interactIcon.SetBool("Showing", false);

    }
    void Update()
    {
        InteractionPassive();

        bool a = Vector3.Distance(transform.position, target.position) < radius;
        interactIcon.SetBool("Showing", a);
        
        if (canInteract && a)
        {
            if (Input.GetKeyDown(interactKey))
            {
                InteractionActive();
            }
        }
    }
    
    //MAKE ADDITIONAL INTERACTION COROUTINES?
    //item logic would profit from this
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
