
using UnityEngine;

public class Playerscrap : MonoBehaviour
{
   // [SerializeField] float mouseSensetivity = 3f;
    [SerializeField] float movementSpeed = 3f;
    [SerializeField] float mass = 3f;
    [SerializeField] Transform cameraTransform;

    CharacterController controller;
    Vector3 velocity;
    Vector2 look;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
    }
    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    void UpdateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.forward * x;
        input = Vector3.ClampMagnitude(input, 1f);

        controller.Move((input * movementSpeed + velocity) * Time.deltaTime);
    }

    // Update is called once per frame
    void UpdateLook()
    {
        look.x += Input.GetAxis("Mouse X");
        look.y += Input.GetAxis("Mouse Y");

        look.y = Mathf.Clamp(look.y, -85f, 85f);

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }
}
