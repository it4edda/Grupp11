using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Money is health
 * When beginning to scan items starts a timer
 * need to scan all the stuff and pay under time limit?
 * if timer runs out either loose or ghost from spelunky?
 */

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
