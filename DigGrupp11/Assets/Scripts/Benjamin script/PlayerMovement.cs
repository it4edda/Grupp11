using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    // IMPORTANT! moveSpeed and walkSpeed have to be the same
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float walkSpeed = 5f;

    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float crouchSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] float groundDrag;

    [Header("Ground Check")]

    [SerializeField] LayerMask Ground;
    bool isGrounded = true;

    [SerializeField] Transform orientation;

    float horizantalInput;
    float verticalInput;

    bool isCrouching = false;
  

    Vector3 moveDirection;

    Rigidbody playerRb;


    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
    }

    private void Update()
    {
        Jump();
        Crouch();
        MyInput();
        SpeedControl();

        if (isGrounded)
            playerRb.drag = groundDrag;
        else
            playerRb.drag = 0;

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizantalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizantalInput;

        playerRb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            playerRb.velocity = new Vector3(limitedVel.x, playerRb.velocity.y, limitedVel.z);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = crouchSpeed;
        }

        else
        {
            moveSpeed = walkSpeed;
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Crouching();
        }

        else
        {
            StandingUp();
        }
    }

    private void Crouching()
    {
        isCrouching = true;
        transform.localScale = new Vector3(1, 0.5f, 1);
        moveSpeed = crouchSpeed;
    }

    private void StandingUp()
    {
        isCrouching = false;
        transform.localScale = new Vector3(1, 1, 1);
        moveSpeed = walkSpeed;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            isGrounded = false;
        }

    }
}
