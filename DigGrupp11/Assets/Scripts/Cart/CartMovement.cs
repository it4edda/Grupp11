using UnityEngine;

public class CartMovement : MonoBehaviour
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float forwardMax;
    [SerializeField] float backSpeed;
    [SerializeField] float backMax;
    [SerializeField] float sideSpeed;
    [SerializeField] float sideMax;
    [SerializeField] float rotateSpeed;

    [SerializeField] bool havePlayer = false;

    [SerializeField] Rigidbody rb;
    TempP player;
    RotationThingy rotationThingy;

    private void Start()
    {
        player = FindObjectOfType<TempP>();
        rb = GetComponent<Rigidbody>();
        rotationThingy = FindObjectOfType<RotationThingy>();
    }

    private void Update()
    {
        if (!havePlayer)
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.A) && rb.velocity.magnitude < sideSpeed)
        {
            rb.AddForce(-transform.right * sideSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D) && rb.velocity.magnitude < sideSpeed)
        {
            rb.AddForce(transform.right * sideSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.W) && rb.velocity.magnitude < forwardMax)
        {
            rb.AddForce(transform.forward * forwardSpeed, ForceMode.Force);
            //rb.velocity += transform.forward * forwardSpeed;
        }
        if (Input.GetKey(KeyCode.S) && rb.velocity.magnitude < backMax && (rotationThingy.CanRotateRight && rotationThingy.CanRotateLeft))
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
        if (havePlayer)
        {
            c.SetBody(GetComponent<Transform>());
        }
        else
        {
            c.SetBody();
        }
        b.CanMove = !a;
        //b.transform.parent   = a ? transform : null;
    }
}
