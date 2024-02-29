using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool  localRotation = true;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(localRotation ? Vector3.up : transform.up, speed * Time.deltaTime);
    }
}
