using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMovement : MonoBehaviour
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float forwardMax;
    [SerializeField] float backSpeed;
    [SerializeField] float rotateSpeed;
    
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0)); 
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * forwardSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = transform.forward * -backSpeed;
        }
    }
}
