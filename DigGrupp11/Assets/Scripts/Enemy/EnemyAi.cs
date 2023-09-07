using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform    player;
    NavMeshAgent agent;
    void Start()
    {
        agent     = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = player.position;
    }
}
