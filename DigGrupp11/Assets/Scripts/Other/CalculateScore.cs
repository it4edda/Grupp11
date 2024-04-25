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
            CalculateFinalScore(GameManager.Instance.score, GameManager.Instance.scoreMult);
        }
    }


    void CalculateFinalScore(int score, float mult)
    {
        finalScore = (int)(score * mult);
        SetScoreText();
    }

    void SetScoreText()
    {
        var formattedScore = finalScore.ToString("N0");
        scoreText.text = formattedScore;
    }
}