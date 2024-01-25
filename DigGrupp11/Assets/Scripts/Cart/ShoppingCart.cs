using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoppingCart : MonoBehaviour
{
    [Header("Cart Stuff")] 
    [SerializeField] CartMovement cartMovement;
    
    [Header("player stuff")]
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector3 checkPos;
    [SerializeField] Vector3 checkSize;
    [SerializeField] Vector3 playerPos;
    [SerializeField] float timePlayerToPos;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationMinDegrees;
    [SerializeField] float rotattionMaxDistance;
    [SerializeField] float heightAfterFixingRotation;

    bool playerAttaching = false;
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
        rb = player.GetComponent<Rigidbody>();

        PlayerCheck();

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void PlayerCheck()
    {
        Collider[] a = Physics.OverlapBox(_checkPos, checkSize / 2, transform.rotation, playerLayer);
        if (Input.GetKeyDown(KeyCode.Q) && 
            ((transform.eulerAngles.x > rotationMinDegrees && transform.eulerAngles.x < (360 - rotationMinDegrees)) || (transform.eulerAngles.z > rotationMinDegrees && transform.eulerAngles.z < (360 - rotationMinDegrees))) &&
            Vector3.Distance(transform.position, player.transform.position) < rotattionMaxDistance) 
        {
            transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            transform.position += new Vector3(0, heightAfterFixingRotation, 0); 
        }
        else if (Input.GetKeyDown(KeyCode.Q) && a.Length > 0 && !playerAttach)
        {
            player.CanMove = false;
            rb.isKinematic = true;
            playerSpeed = player.MovementSpeed;
            player.MovementSpeed = moveSpeed;
            StartCoroutine(MovePlayer());
        }
        else if (Input.GetKeyDown(KeyCode.Q) && playerAttach && !playerAttaching)
        {
            player.transform.parent = null;
            //gameObject.transform.parent = null;
            rb.isKinematic = false;
            player.MovementSpeed = playerSpeed;
            playerAttach = false;
            cartMovement.SetHavePlayer(playerAttach);
        }
        /*
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
        }*/
    }

    IEnumerator MovePlayer()
    {
        playerAttaching = true;
        float currentTime = 0;
        Vector3 pPos = player.transform.position;
        //Vector3 pRo = player.transform.forward;

        while (currentTime < timePlayerToPos)
        {
            float b = currentTime / timePlayerToPos;

            player.transform.position = Vector3.Lerp(pPos, new Vector3(_playerPos.x, player.transform.position.y, _playerPos.z), b);
            //player.transform.forward = Vector3.Lerp(pRo, new Vector3(transform.position.x - player.transform.position.x, 0, transform.position.z - player.transform.position.z), b);

            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        player.transform.parent = transform;
        //gameObject.transform.parent = player.transform;
        playerAttaching = false;
        playerAttach = true;
        cartMovement.SetHavePlayer(playerAttach);
        //player.CanMove = true;
        yield return null;
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
