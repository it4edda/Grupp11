using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotationThingy : MonoBehaviour
{
    //CHECKS IF THERE IS A WALL IN THE WAY OF THE ROTATION
    [SerializeField] bool gizmos;

    [SerializeField] Vector3 size;
    [SerializeField] Vector3 rigthPos;
    [SerializeField] Vector3 leftPos;
    [SerializeField] LayerMask layer;


    bool canRotateRight;
    public bool CanRotateRight { get => canRotateRight; }
    bool canRotateLeft;
    public bool CanRotateLeft { get => canRotateLeft; }

    Vector3 _rigthPos;
    Vector3 _leftPos;

    private void Update()
    {
        _rigthPos = transform.rotation * rigthPos + transform.position;
        _leftPos = transform.rotation * leftPos + transform.position;

        Collider[] R = Physics.OverlapBox(_rigthPos, size / 2, transform.rotation, layer);
        if (R.Length == 0) { canRotateRight = true; }
        else { canRotateRight = false; }
        //print("R = " + R.Length);

        Collider[] L = Physics.OverlapBox(_leftPos, size / 2, transform.rotation, layer) ;
        if (L.Length == 0) { canRotateLeft = true;  }
        else { canRotateLeft = false; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        if (!gizmos) { return; }

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(rigthPos, size);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(leftPos, size);
    }

}
