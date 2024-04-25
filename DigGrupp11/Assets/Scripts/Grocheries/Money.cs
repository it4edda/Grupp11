using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Money : Interaction
{
    [SerializeField] public bool canBePickedUp = true;
    [SerializeField] AudioClip[] clingSounds;
    private AudioSource audioSource;
    protected override void Start()
    {
        audioSource = GetComponent<AudioSource>();
        base.Start();
    }

    protected override void InteractionActive()
    {
        base.InteractionActive();

        if (canBePickedUp)
        {
            FindObjectOfType<PlayerMoney>().CurrentMoney++;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        audioSource.PlayOneShot(clingSounds[Random.Range(0, clingSounds.Length)]);
    }
}
