using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3[] trackPositions;
    [SerializeField] bool      detected;
    NavMeshAgent               agent;
    void Start()
    {
        agent     = GetComponent<NavMeshAgent>();
    }

    void Update() { Walk(detected ? player.position : transform.position /*fix here */); 
    }
    void FindClosest()
    {
        Vector3 closestPoint = trackPositions[0];

        foreach (Vector3 i in trackPositions)
        {
            float a = Vector3.Distance(transform.position, closestPoint);
            if (Vector3.Distance(transform.position, i) < a) closestPoint = i;
        }
    }
    void Walk(Vector3 target)
    {
        agent.destination = target;
    }
    
    
    // if distance is lower than detection range to player / if player is within detection -> change target to player
    //if distance is lowe than detection range to current target (point to point movement (a -> b -> c -> a))
    
    void OnDrawGizmosSelected()
    {
        if (trackPositions.Length < 1) return;
        
        var a = trackPositions[^1];
        Gizmos.color = Color.red;

        foreach (var i in trackPositions)
        {
            Gizmos.DrawWireSphere(i, 0.4f);
            Gizmos.DrawLine(a, i);
            a = i;
        }
    }
}
