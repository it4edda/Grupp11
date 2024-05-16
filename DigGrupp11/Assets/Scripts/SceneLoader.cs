using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Animator animator;
    string queuedScene;
    static readonly int FadeOut = Animator.StringToHash("FadeOut");

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void LoadQueuedScene()
    {
        SceneManager.LoadScene(queuedScene);
    }

    public void LoadSceneWithString(string sceneToLoad)
    {
        queuedScene = sceneToLoad;
        animator.SetTrigger(FadeOut);
    }

    public void LoadStartScene()
    {
        queuedScene = "StartScene";
        animator.SetTrigger(FadeOut);
        //SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        queuedScene = "GameScene Level 1";
        animator.SetTrigger(FadeOut);
        //SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
        print("Quit");
    }

    public void SetTime(int timeToSet)
    {
        GameManager.Instance.SetGivenTime(timeToSet);
    }

    public void SetMult(float mult)
    {
        GameManager.Instance.SetMult(mult);
    }
    
    public void TimerText(float time)
    {
        if (timerText == null) return;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
