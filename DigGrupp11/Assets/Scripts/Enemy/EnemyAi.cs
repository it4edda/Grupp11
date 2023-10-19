using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    /*
    [SerializeField] Transform player;
    [SerializeField] Vector3[] trackPositions;
    [SerializeField] float     detectionRange;
    [SerializeField] float     chaseRange;
    [SerializeField] bool      detected;
    [SerializeField] bool      stasis;
    [SerializeField] LayerMask playerMask;
    NavMeshAgent               agent;
    Vector3                    currentlyTrackedPathTarget;
    int                        currentNum;
    void Start()
    {
        agent            = GetComponent<NavMeshAgent>();
        currentlyTrackedPathTarget = trackPositions[currentNum];
    }
    void Update()
    {
        //detected = Vector3.Distance(transform.position,       player.position)  < detectionRange; //CHANGED IN UPDATED
        DetectionScan();
        if (!detected && Vector3.Distance(transform.position, currentlyTrackedPathTarget) < agent.stoppingDistance + 0.5) FindNext();
        Walk(stasis ? transform.position:  detected ? player.position : currentlyTrackedPathTarget); 
    }
    void FindNext()
    {
        currentlyTrackedPathTarget = trackPositions[currentNum = ++currentNum % trackPositions.Length];
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
        Gizmos.DrawWireSphere(currentlyTrackedPathTarget, 1);
    }


#region UPDATED
    
    void DetectionScan() //call on collision with "imaginary cone"
    {
        var distance = Vector3.Distance(player.position, player.position);
        if (Vector3.Distance(agent.destination, transform.position) < 2) detected = false;
        if (distance > detectionRange) return; //add stasis here

        
        Vector3 direction =(transform.position - player.position).normalized;
        detected = Physics.Raycast(transform.position, direction, detectionRange, playerMask);
        Walk(detected ? player.position : currentlyTrackedPathTarget);

    }
    
#endregion
*/
    [SerializeField] bool      stasis;
    [SerializeField] Vector3[] trackPositions;
    [SerializeField] float     detectionRange;
    [SerializeField] float     chaseRange;
    [SerializeField] Transform player;
    Vector3                    currentTarget;
    bool                       inChaseRange;
    bool                       playerDetected;
    bool                       chasing;
    int                        currentTrackNumber;
    NavMeshAgent               agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        //set values
        playerDetected = Vector3.Distance(transform.position, player.position) < detectionRange; //TODO CHANGE THIS TO CONE
        inChaseRange  = Vector3.Distance(transform.position, player.position) < chaseRange;
        
        //set current target (for walking back and forth)
        if (!chasing && Vector3.Distance(transform.position, trackPositions[currentTrackNumber]) < 0.5f) 
            currentTrackNumber = ++currentTrackNumber % trackPositions.Length;

        //should he be chasing?
        if (playerDetected) chasing           = true;
        if (!inChaseRange) chasing = false;
        
        //pick target
        currentTarget = chasing ? player.position : trackPositions[currentTrackNumber];
        
        //walk to target
        agent.destination = stasis  ? transform.position :
                            chasing ? player.position : trackPositions[currentTrackNumber];
    }
    void OnDrawGizmos()//Selected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        
        
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
        Gizmos.DrawWireSphere(currentTarget, 1);
    }
}

