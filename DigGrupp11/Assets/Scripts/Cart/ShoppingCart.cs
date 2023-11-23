using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoppingCart : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector3 checkPos;
    [SerializeField] Vector3 checkSize;
    [SerializeField] Vector3 playerPos;
    [SerializeField] float moveSpeed;

    bool playerAttach = false;
    public bool PlayerAttach{ get => playerAttach; }

    TempP player;
    Vector3 _checkPos;
    Vector3 _playerPos;
    Rigidbody rb;
    [SerializeField] float playerSpeed;

    private void Update()
    {
        _checkPos = transform.rotation * checkPos + transform.position;
        _playerPos = transform.rotation * playerPos + transform.position;

        player = FindObjectOfType<TempP>();
        rb = GetComponent<Rigidbody>();

        PlayerCheck();

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void PlayerCheck()
    {
        Collider[] a = Physics.OverlapBox(_checkPos, checkSize / 2, transform.rotation, playerLayer);
        if (Input.GetKeyDown(KeyCode.Q) && (Mathf.Abs(transform.eulerAngles.x) > 0.1f || Mathf.Abs(transform.eulerAngles.z) > 0.1f)) 
        {
            transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && a.Length > 0 && !playerAttach)
        {
            player.transform.position = new Vector3(_playerPos.x, player.transform.position.y, _playerPos.z);
            player.transform.forward = new Vector3(transform.position.x - player.transform.position.x, 0, transform.position.z - player.transform.position.z);
            rb.isKinematic = true;
            gameObject.transform.parent = a[0].transform;
            playerSpeed = player.MovementSpeed;
            player.MovementSpeed = moveSpeed;
            playerAttach = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && playerAttach)
        {
            gameObject.transform.parent = null;
            rb.isKinematic = false;
            player.MovementSpeed = playerSpeed;
            playerAttach = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(checkPos, checkSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(playerPos, 0.1f);
    }
}
