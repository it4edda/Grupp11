using UnityEngine;

public class ShoppingCart : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector3 checkPos;
    [SerializeField] Vector3 checkSize;
    [SerializeField] Vector3 plyerPos;

    private void Update()
    {
        PlayerCheck();
    }

    private void PlayerCheck()
    {
        Collider[] a = Physics.OverlapBox(gameObject.transform.position + checkPos, checkSize / 2, Quaternion.identity, playerLayer);
        if (a.Length > 0 && Input.GetKeyDown(KeyCode.K))
        {
            a[0].transform.parent = gameObject.transform;
            a[0].transform.position = plyerPos + gameObject.transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(gameObject.transform.position + checkPos, checkSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(gameObject.transform.position + plyerPos, 0.2f);
    }
}
