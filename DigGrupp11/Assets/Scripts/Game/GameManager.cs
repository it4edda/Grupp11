using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            time = 0f;
            timerOn = true;
        }
        else
        {
            timerOn= false;
            Cursor.lockState = CursorLockMode.None;
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
    #endregion
}
