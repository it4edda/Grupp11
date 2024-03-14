using UnityEngine;

public class TempC : MonoBehaviour
{
    //bool Leoiscool = true;
#region

    [SerializeField] float     mouseSensitivity  = 1500f;
    [SerializeField] float     mouseSensitivityY = 1500f;
    [SerializeField] Transform followPoint;
    [SerializeField] bool      allowRotation = true;
    public bool AllowRotation
    {
        get => allowRotation;
        set => allowRotation = value;
    }
    float                      xRotation     = 0f;
    float                      yRotation     = 0f;
    public float YRotation
    {
        get => yRotation;
        set => yRotation = value;
    }
    Rigidbody                  rb;
    Transform                  playerBody;
    RotationThingy rotationThingy;

    public bool isHoldingCart = false;
#endregion
    private void Awake()
    {
        SetBody();
        rb = GetComponentInParent<Rigidbody>();
        Debug.Log(rb);
        rotationThingy = FindObjectOfType<RotationThingy>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!allowRotation) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityY * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (!isHoldingCart)
        {
            yRotation += mouseX;
            playerBody.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else //if (isHoldingCart)
        {
            if (mouseX <= 0 && rotationThingy.CanRotateRight)
            {
                //right
                yRotation += mouseX;
                playerBody.rotation = Quaternion.Euler(0, yRotation, 0);
            }
            else if (mouseX > 0 && rotationThingy.CanRotateLeft)
            {
                //left
                yRotation += mouseX;
                playerBody.rotation = Quaternion.Euler(0, yRotation, 0);
            }
        }
    }

    public void SetBody( Transform newRB)
    {
        playerBody = newRB;
    }
    public void SetBody()
    {
        playerBody = transform.parent.transform;
    }
    
    void FixedUpdate()
    {
        Look();
    }
    

    void Look()
    {
        /*float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        Vector3 torque = Vector3.up * mouseX;

        Debug.Log("Mouse X: "                  + mouseX);
        Debug.Log("Torque: "                   + torque);
        Debug.Log("Current Angular Velocity: " + rb.angularVelocity);

        rb.AddTorque(torque, ForceMode.VelocityChange);
        */
    }
}
