using UnityEngine;

public class TempC : MonoBehaviour
{
    bool Leoiscool = true;
#region
    [SerializeField] float mouseSensitivity = 1500f;

    float xRotation = 0f;

    Transform playerBody;
#endregion
    private void Awake()
    {
        playerBody = transform.parent.transform;
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
        playerBody.Rotate(Vector3.up * mouseX);

        //up down
        xRotation               -= mouseY;
        xRotation               =  Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation =  Quaternion.Euler(xRotation, 0f, 0f);
    }
}
