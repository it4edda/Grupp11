using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> availableGroceriesToSpawn;
    public int numberOfDifferentGroceriesToSpawn;
    public MinMax numberOfSameGroceriesToSpawn;

    [SerializeField] int gameSceneIndex;

    [SerializeField] bool timerOn;
    float time = 0;
    SceneLoader sceneLoader;

    void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        { 
            Destroy(gameObject); 
        } 
        else 
        { 
            Instance = this; 
        }
        DontDestroyOnLoad(gameObject);
    }

    #region Timer
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == gameSceneIndex)
        {
            timerOn = true;
        }
        else
        {
            timerOn= false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            ResetTimer();
        }
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.TimerText(time);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (timerOn)
        {
            time += Time.deltaTime;
            sceneLoader.TimerText(time);
        }
    }

    private void ResetTimer()
    {
        time = 0;
    }
    #endregion

    public void CheckShoppingList()
    {
        ShoppingListUI shoppingListUI = FindObjectOfType<ShoppingListUI>();
        if (shoppingListUI.currentShoppingList.Count(text => text.Complete == true) >= shoppingListUI.currentShoppingList.Count)
        {
            
        }
    }
}
