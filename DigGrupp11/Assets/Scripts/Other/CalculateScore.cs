using UnityEngine;
using TMPro;

public class CalculateScore : MonoBehaviour
{
    [SerializeField] int finalScore;
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        if (GameManager.Instance)
        {
            CalculateFinalScore(GameManager.Instance.score, GameManager.Instance.time);
        }
    }


    void CalculateFinalScore(int score, float time)
    {
        finalScore = (int)(score * time);
        SetScoreText();
    }

    void SetScoreText()
    {
        var formattedScore = finalScore.ToString("N");
        scoreText.text = formattedScore;
    }
}