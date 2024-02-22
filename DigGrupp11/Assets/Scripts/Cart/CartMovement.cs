using UnityEngine;

public class CartMovement : MonoBehaviour
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float forwardMax;
    [SerializeField] float backSpeed;
    [SerializeField] float backMax;
    [SerializeField] float rotateSpeed;

    [SerializeField] bool havePlayer = false;

    [SerializeField] Rigidbody rb;
    TempP player;
    RotationThingy RotationThingy;

    private void Start()
    {
        player = FindObjectOfType<TempP>();
        rb = GetComponent<Rigidbody>();
        RotationThingy = FindObjectOfType<RotationThingy>();
    }

    private void Update()
    {
        if (!havePlayer)
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.A) && RotationThingy.CanRotateRight)
        {
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0)); 
        }
        if (Input.GetKey(KeyCode.D) && RotationThingy.CanRotateLeft)
        {
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.W) && rb.velocity.magnitude < forwardMax)
        {
            rb.AddForce(transform.forward * forwardSpeed, ForceMode.Force);
            //rb.velocity += transform.forward * forwardSpeed;
        }
        if (Input.GetKey(KeyCode.S) && rb.velocity.magnitude < backMax)
        {
            rb.AddForce(-transform.forward * backSpeed, ForceMode.Force);
        }
    }


    public void SetHavePlayer(bool a)
    {
        havePlayer = a;
        Debug.Log("did cart stuff" + " ---  holding cart =" + a);
        TempP b = FindObjectOfType<TempP>();
        TempC c = FindObjectOfType<TempC>();
        c.isHoldingCart = a;
        b.CanMove = !a;
        //b.transform.parent   = a ? transform : null;
    }
}
