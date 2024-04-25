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
    [Header("Don't change")]
    public float scoreMult;
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

    public void SetGivenTime(int timeToGive)
    {
        givenTimeSec = timeToGive;
    }

    public void SetMult(float mult)
    {
        scoreMult = mult;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.TimerText(time);
        Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "GameScene Level 1")
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
