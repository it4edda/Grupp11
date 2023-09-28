using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3[] trackPositions;
    [SerializeField] float detectionRange;
    [SerializeField] bool  detected;
    [SerializeField] bool  stasis;
    NavMeshAgent           agent;
    Vector3                currentlyTracked;
    int                    currentNum;
    void Start()
    {
        agent            = GetComponent<NavMeshAgent>();
        currentlyTracked = trackPositions[currentNum];
    }
    void Update()
    {
        detected = Vector3.Distance(transform.position,       player.position)  < detectionRange;
        if (!detected && Vector3.Distance(transform.position, currentlyTracked) < agent.stoppingDistance + 0.5) FindNext();
        Walk(stasis ? transform.position:  detected ? player.position : currentlyTracked); 
    }
    void FindNext()
    {
        currentlyTracked = trackPositions[currentNum = ++currentNum % trackPositions.Length];
    }
    void Walk(Vector3 target) => agent.destination = target;
    
    //if something is making noise in bigger circle, turn toward sound
    //if player is in vision cone, chase
    //if player is flees cone, stay for a few secs, then continue normal pathing.
    
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

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(currentlyTracked, 1);
    }
}
