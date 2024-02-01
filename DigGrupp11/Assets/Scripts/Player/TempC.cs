using System;
using Unity.Mathematics;
using UnityEngine;

public class TempC : MonoBehaviour
{
    //bool Leoiscool = true;
#region

    [SerializeField] float     mouseSensitivity  = 1500f;
    [SerializeField] float     mouseSensitivityY = 1500f;
    [SerializeField] Transform followPoint;

    float     xRotation = 0f;
    float     yRotation = 0f;
    Rigidbody rb;
    Transform playerBody;

    public bool isHoldingCart = false;
#endregion
    private void Awake()
    {
        playerBody = transform.parent.transform;
        rb = GetComponentInParent<Rigidbody>();
        Debug.Log(rb);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityY * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.parent.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
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
