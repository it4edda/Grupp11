using UnityEngine;
using UnityEngine.Serialization;

public class Groceries : Interaction
{
    [SerializeField] float price;
    [FormerlySerializedAs("typte")] [SerializeField] ShelfType type;

    public ShelfType Type
    {
        get => type;
        set => type = value;
    }
    public float Price => price;

    [SerializeField] LineRenderer lineRenderer;
    public GameObject text;
    public Transform spawnPoint;

    Rigidbody rb;
    BoxCollider boxCollider;

    public bool isPickedUp;
    bool showShadow;
    protected override void Start()
    {
        base.Start();
        canInteract = true;
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    protected override void Update()
    {
        InteractionPassive();
        if (showShadow)
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
        showShadow = isPicked;
        lineRenderer.gameObject.SetActive(isPicked);
    }

    void DropShadow()
    {
        var position = transform.position;
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 100000))
        {
            lineRenderer.SetPosition(0, position);
            Vector3 lineVector = new(position.x, hit.transform.position.y, position.z);
            lineRenderer.SetPosition(1, lineVector);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
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

    public void GetsPickedUp(bool pickupStatus)
    {
        boxCollider.isTrigger = pickupStatus;
        rb.isKinematic = pickupStatus;
        isPickedUp = pickupStatus;
    }
}
