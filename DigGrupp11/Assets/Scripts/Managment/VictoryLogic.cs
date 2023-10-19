using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryLogic : MonoBehaviour
{
    [SerializeField] bool canWin;
    public bool CanWin { get => canWin; set => canWin = value; }
    void OnTriggerEnter(Collider other)
    {
        if (canWin && other.CompareTag("Player")) SceneManagerExtended.ReloadScene();
    }
}
