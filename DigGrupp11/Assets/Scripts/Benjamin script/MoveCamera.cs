using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform CameraPosition;

    // Update is called once per frame
    private void Update()
    {
        transform.position = CameraPosition.position; 
    }
}
