using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float knockbackPower = 5f;
    [SerializeField] int   health         = 3;
    
    [Header("Enemy")]
    [SerializeField] protected Transform targetToChase;
    [SerializeField] float distance = 1;
    [SerializeField] float speed = 2.36f;
    bool                   canMove = true;
    protected Rigidbody              rb;
    Vector3                movementVector = Vector3.zero;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Movement();
        Check();
    }
    Vector3 velocity = Vector3.zero;
    [SerializeField] float     drag     = 0.6f;
    [SerializeField] float     spring   = 0.4f;
    protected virtual void Movement()
    {
        movementVector = targetToChase.position - transform.position;
        movementVector = Vector3.Normalize(movementVector);
        if (!canMove) return;
        
        //rb.AddForce(movementVector * speed, ForceMode.Force);
        
        velocity    += (movementVector - rb.velocity) * spring;
        velocity    -= drag                           * velocity;
        rb.velocity += velocity * speed; 
/*
Velocity += (targetPos - currentPos) * spring;
Velocity -= drag * velocity;
CurrentPos += Velocity
drag ~0.6
spring ~0.4
*/
    }
    
    protected virtual bool Check()
    { 
        bool  returnVal = (Vector3.Distance(targetToChase.position, transform.position) < distance);
        return returnVal;
    }

    
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
}

