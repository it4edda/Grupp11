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
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            
            a[0].transform.parent = null;
            a[0].GetComponent<Rigidbody>().isKinematic = false;

            GetComponent<CartMovement>().SetHavePlayer(false);
        }
        if (a.Length > 0 && Input.GetKeyDown(KeyCode.K))
        {
            a[0].transform.parent = gameObject.transform;
            a[0].transform.position = _playerPos;
            a[0].GetComponent<Rigidbody>().isKinematic = true;

            GetComponent<CartMovement>().SetHavePlayer(true);
        }*/

        if (a.Length > 0 && Input.GetKeyDown(KeyCode.Q) && !playerAttach)
        {
            player.transform.position = _playerPos;
            rb.isKinematic = true;
            gameObject.transform.parent = a[0].transform;
            playerSpeed = player.MovementSpeed;
            player.MovementSpeed = moveSpeed;
            playerAttach = true;
        }
        else if (a.Length > 0 && Input.GetKeyDown(KeyCode.Q) && playerAttach)
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
        Gizmos.DrawWireSphere(playerPos, 0.2f);
    }
}
