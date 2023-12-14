using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoppingCart : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector3 checkPos;
    [SerializeField] Vector3 checkSize;
    [SerializeField] Vector3 playerPos;
    [SerializeField] float timePlayerToPos;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationMin;
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
        if (Input.GetKeyDown(KeyCode.Q) && ((transform.eulerAngles.x > rotationMin && transform.eulerAngles.x < (360 - rotationMin)) || (transform.eulerAngles.z > rotationMin && transform.eulerAngles.z < (360 - rotationMin)))) 
        {
            transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            transform.position += new Vector3(0, heightAfterFixingRotation, 0); 
        }
        /*if (Input.GetKeyDown(KeyCode.Q) && (Mathf.Abs(transform.eulerAngles.x) > 0.1f || Mathf.Abs(transform.eulerAngles.z) > 0.1f))
        {
            transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            transform.position += new Vector3(0, heightAfterFixingRotation, 0);
        }*/
        else if (Input.GetKeyDown(KeyCode.Q) && a.Length > 0 && !playerAttach)
        {
            //StartCoroutine(MovePlayer());
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
        
        while (currentTime < timePlayerToPos)
        {
            float b = currentTime / timePlayerToPos;
            print(b);
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        playerAttaching = false;
        yield return null;
    }
    //move over time
    //lerp a=player pos att start b=end pos t=how much time have past/total time
    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(checkPos, checkSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(playerPos, 0.1f);
    }
}
