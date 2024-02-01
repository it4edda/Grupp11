using UnityEngine;

public class CartChaser : EnemyAi
{
    [SerializeField] float power;
    Vector3                launchVector;

    protected override void Start()
    {
        base.Start();
        targetToChase = FindObjectOfType<ShoppingCart>().transform;
    }

    protected override bool Check()
    {
        if (!base.Check())
        {
            return false;
        }

        
        launchVector = targetToChase.position - transform.position;
        launchVector = Vector3.Normalize(launchVector);
        
        targetToChase.GetComponent<Rigidbody>().AddForce(launchVector * power + Vector3.up * 2, ForceMode.Impulse);
        
        Kill();
        return true;
    }
}
