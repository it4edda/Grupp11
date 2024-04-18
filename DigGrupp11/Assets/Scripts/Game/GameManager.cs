using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region SetInstance
    public static GameManager Instance { get; private set; }
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
    #endregion

    [SerializeField] int gameSceneIndex;

    [SerializeField] bool timerOn;
    [SerializeField] float givenTimeSec;
    public int score;
    public float time = 0;
    SceneLoader sceneLoader;


    #region MyRegion
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion
    #region Timer

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.TimerText(time);

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            time = givenTimeSec;
            timerOn = true;
        }
        else
        {
            timerOn = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    private void Update()
    {
        if (timerOn)
        {
            time -= Time.deltaTime;
            if (time < 1)
            {
                SceneManager.LoadScene("LoseScene");
            }
            sceneLoader.TimerText(time);
        }
    }
    #endregion
}
