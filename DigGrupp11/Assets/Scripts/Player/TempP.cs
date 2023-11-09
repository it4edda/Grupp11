using System;
using UnityEngine;

public class TempP : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float crouchSpeed = 2f;
    [SerializeField] float crouchSize = 0.5f;
    [SerializeField] float gravity = -29.46f; //-9.82
    [SerializeField] float jumpHeight = 1.5f;
    //[SerializeField] float dashPower = 3f;

    [Header("Ground Check")]
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    float xInput;
    float zInput;
    Vector3 movement;
    Vector3 velocity;
    bool isGrounded;
    //bool canDash;
    bool isCrouching;
    bool canMove = true;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ground();
        Crouch();
        Move();
        Jump();
    }
    void Ground()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            transform.localScale = new Vector3(1, crouchSize, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching= false;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Move()
    {
        if (!CanMove)
            return;
        float movementSpeed = 1;
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        movement = Vector3.ClampMagnitude((transform.right * xInput) + (transform.forward * zInput), 1f);

        if (Input.GetKey(KeyCode.LeftShift) && !FindObjectOfType<ShoppingCart>().PlayerAttach)
        {
            movementSpeed = runSpeed;
        }
        else if (isCrouching)
        {
            movementSpeed = crouchSpeed;
        }
        else
        {
            movementSpeed = walkSpeed;
        }

       
        if (velocity.magnitude < movementSpeed) rb.AddForce(movement * movementSpeed, ForceMode.Impulse);
        //characterController.Move(movement * (movementSpeed * Time.deltaTime));x
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = MathF.Sqrt(jumpHeight * -2f * gravity);   
        }
    }
    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }
    public float MovementSpeed
    {
        get => walkSpeed;
        set => walkSpeed = value;
    }
}
