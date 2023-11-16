using System;
using Unity.VisualScripting;
using UnityEngine;

public class Groceries : Interaction
{
    [SerializeField] LineRenderer lineRenderer;
    PlayerHand playerHand;
    public GameObject text;

    bool isPickedUp;
    protected override void Start()
    {
        base.Start();
        canInteract = true;
        playerHand = FindObjectOfType<PlayerHand>();
    }

    protected override void Update()
    {
        InteractionPassive();
        if (isPickedUp)
        {
            DropShadow();
        }
        
        bool a = Vector3.Distance(transform.position, target.position) < radius;
        interactIcon.SetBool("Showing", a);
        
        if (canInteract && a)
        {
            if (Input.GetKeyDown(interactKey))
            {
                InteractionActive();
            }
        }
    }

    public void SetShadow(bool isPicked)
    {
        isPickedUp = isPicked;
        lineRenderer.gameObject.SetActive(isPicked);
    }

    void DropShadow()
    {
        var position = transform.position;
        Physics.Raycast(position, Vector3.down, out RaycastHit hit, 100000);
        lineRenderer.SetPosition(0, position);
        Vector3 lineVector = new(position.x, hit.transform.position.y, position.z);
        lineRenderer.SetPosition(1, lineVector);
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("CartZone"))
        {
            Debug.Log(text);
            FindObjectOfType<SpawnManager>().RemoveGroceryFromList(text);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CartZone"))
        {
            Debug.Log(text);
            FindObjectOfType<SpawnManager>().AddGroceryToList(text);
        }
    }
}
