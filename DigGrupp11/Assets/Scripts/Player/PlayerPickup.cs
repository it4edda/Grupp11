using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerPickup : MonoBehaviour
{
    public static event Action<bool, Transform> PickingUpSomething;

    [SerializeField] float      range;
    [SerializeField] float      throwPower;
    [SerializeField] LayerMask  mask;
    [SerializeField] LayerMask  enemyMask;
    [SerializeField] Transform  handPivot;
    [SerializeField] Animator   handAnimator;
    [SerializeField] GameObject handPrefab;
    [SerializeField] GameObject moneyPrefab;
    PlayerMoney                 playerMoney;
    Transform                   held;
    GameObject                  instantiatedHand;
    void Start()
    {
        playerMoney = FindObjectOfType<PlayerMoney>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Pickup();
        if (Input.GetKeyDown(KeyCode.Mouse0)) Attack();
        if (Input.GetKeyDown(KeyCode.Mouse1)) ThrowMoney();
    }
    void Pickup()
    {
        if (!held)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, mask))
            {
                held                              = hit.transform;
                instantiatedHand                  = Instantiate(handPrefab, hit.transform.position + Vector3.up * 0.2f, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
                instantiatedHand.transform.parent = hit.transform;
                hit.transform.parent              = transform;
                held.GetComponent<Groceries>().GetsPickedUp(true);                  
                held.GetComponent<Groceries>().SetShadow(true);
                PickingUpSomething?.Invoke(true, held);
            } 
        }
        else
        {
            Destroy(instantiatedHand);
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
    void ThrowMoney()
    {
        if (--playerMoney.CurrentMoney <= -1) return;
        var a = Instantiate(moneyPrefab, transform.position + transform.forward * 1, quaternion.identity);
        a.GetComponent<Rigidbody>().AddForce(transform.forward * throwPower);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
    }
}