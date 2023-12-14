using UnityEngine;

public class TempC : MonoBehaviour
{
    //kys leo
    //bool Leoiscool = true;
#region
    [SerializeField] float mouseSensitivity = 1500f;

    float xRotation = 0f;

    Rigidbody rb;
    Transform playerBody;
#endregion
    private void Awake()
    {
        playerBody = transform.parent.transform;
        rb = GetComponentInParent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        look();
    }

    void look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //GetAxisRaw is instant, otherwise it lerps
        //left right
        
        
        
        //works 
        playerBody.Rotate(Vector3.up * mouseX);

        
        
        //Vector3 a = new Vector3(mouseX,0, 0);
        //rb.AddTorque(a * 100, ForceMode.Force);
        Debug.Log("WILLIAM JOBBAR HÄR");

        //up down
        xRotation               -= mouseY;
        xRotation               =  Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation =  Quaternion.Euler(xRotation, 0f, 0f);
    }
}
