using UnityEngine;
using UnityEngine.UI;

public class EvaluationScoreDisplayByLives : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public int scoreFontSize = 125;
    public int timeFontSize = 110;

    private void Start()
    {
        int lives = PlayerPrefs.GetInt("WeatherGameLives", 0);
        float timeUsed = PlayerPrefs.GetFloat("WeatherGameTimeUsed", 0f);

        float score = 0f;
        switch (lives)
        {
            case 7: score = 100f; break;
            case 6: score = 85.71f; break;
            case 5: score = 71.43f; break;
            case 4: score = 57.14f; break;
            case 3: score = 42.86f; break;
            case 2: score = 28.57f; break;
            case 1: score = 14.29f; break;
            default: score = 0f; break;
        }

        if (scoreText != null) {
            scoreText.text = $"Your Score: {score}";
            scoreText.resizeTextForBestFit = false;
            scoreText.fontSize = scoreFontSize;
        }
        if (timeText != null) {
            timeText.text = $"Time Used: {timeUsed:0.##} seconds";
            timeText.resizeTextForBestFit = false;
            timeText.fontSize = timeFontSize;
        }
    }
}
