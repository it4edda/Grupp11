using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMovement : MonoBehaviour
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float forwardMax;
    [SerializeField] float backSpeed;
    [SerializeField] float backMax;
    [SerializeField] float rotateSpeed;

    [SerializeField] bool havePlayer = false;

    Rigidbody rb;
    TempP     player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /*if (!havePlayer)
        {
            return;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0)); 
        }
        if (Input.GetKey(KeyCode.D))
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
        }*/
    }


    public void SetHavePlayer(bool a)
    {
        havePlayer       = a;
        Debug.Log("did cart stuff" + " ---  holding cart =" + a);
        transform.parent = havePlayer ? FindObjectOfType<TempP>().transform : null;
    }
}
