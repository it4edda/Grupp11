using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if ( health <= 0 )
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(1);
        }
    }
}
