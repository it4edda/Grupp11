using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;

    private void OnCollisionEnter(Collision other)
    {
        if (/*other is harmful*/ false)
        {
            health -= 1;

            if ( health <= 0 )
            {
                SceneManager.LoadScene("LoseScene");
            }
        }
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            health -= 1;

            if (health <= 0)
            {
                SceneManager.LoadScene("LoseScene");
            }
        }
    }*/
}
