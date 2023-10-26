using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoppingCart : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector3 checkPos;
    [SerializeField] Vector3 checkSize;
    [SerializeField] Vector3 playerPos;

    Vector3 _checkPos;
    Vector3 _playerPos;

    private void Update()
    {
        _checkPos = transform.rotation * checkPos + transform.position;
        _playerPos = transform.rotation * playerPos + transform.position;
        PlayerCheck();

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void PlayerCheck()
    {
        Collider[] a = Physics.OverlapBox(_checkPos, checkSize / 2, transform.rotation, playerLayer);
        if (Input.GetKeyDown(KeyCode.M))
        {
            a[0].transform.parent = null;

            GetComponent<CartMovement>().SetHavePlayer(false);
        }
        if (a.Length > 0 && Input.GetKeyDown(KeyCode.K))
        {
            a[0].transform.parent = gameObject.transform;
            a[0].transform.position = _playerPos;
            a[0].GetComponent<Rigidbody>().isKinematic = true;

            GetComponent<CartMovement>().SetHavePlayer(true);
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
