using System.Xml.Xsl;
using Unity.Mathematics;
using UnityEngine;

public class PlayerChaser : EnemyAi
{
    [SerializeField] int        damage = 1;
    [SerializeField] GameObject moneyPrefab;
    Transform                   playerTransform;
    Transform                   checkoutTransform;
    Rigidbody                   currentMoney;
    
    protected override void Start()
    {
        base.Start();
        targetToChase     = FindObjectOfType<TempP>().transform;
        checkoutTransform = FindObjectOfType<Checkout>().transform;
        playerTransform   = targetToChase;
    }

    protected override bool Check()
    {
        if (!base.Check()) return false;
        if (targetToChase == checkoutTransform) Kill();
        else targetToChase.GetComponent<PlayerMoney>().MoneyTheft();
        targetToChase                   =  checkoutTransform;
        currentMoney                    =  Instantiate(moneyPrefab, transform.position, quaternion.identity, transform).GetComponent<Rigidbody>();
        currentMoney.isKinematic        =  true;
        currentMoney.useGravity         =  false;
        currentMoney.transform.position += Vector3.up;
        return true;
    }
    public override void Attacked()
    {
        if (targetToChase != playerTransform)
        {
            currentMoney.transform.parent = null;
            currentMoney.useGravity       = true;
            currentMoney.isKinematic      = false;
            targetToChase                 = playerTransform;
        }
        base.Attacked();
    }
}