using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Groceries : Interaction
{
    [Header("Grocery specific stuff")]
    [SerializeField] int price;
    [SerializeField] int score;
    [SerializeField] ShelfType type;
    [SerializeField] GameObject originalPrefab;
    [SerializeField] AudioClip[] collisionSounds;
    public int Price => price;
    public int Score => score;
    public ShelfType Type => type;
    public GameObject OriginalPrefab => originalPrefab;

    [SerializeField] LineRenderer lineRenderer;
    public GameObject text;
    public Transform spawnPoint;

    Rigidbody rb;
    Collider boxCollider;
    AudioSource audioSource;

    public bool isPickedUp;
    bool showShadow;
    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        canInteract = true;
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<Collider>();
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


    /*void OnTriggerEnter(Collider other)
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
    }*/
        void OnCollisionEnter(Collision other)
        {
            audioSource.PlayOneShot(collisionSounds[Random.Range(0, collisionSounds.Length)]);
        }

    public void GetsPickedUp(bool pickupStatus)
    {
        boxCollider.isTrigger = pickupStatus;
        rb.isKinematic = pickupStatus;
        isPickedUp = pickupStatus;
    }
}
