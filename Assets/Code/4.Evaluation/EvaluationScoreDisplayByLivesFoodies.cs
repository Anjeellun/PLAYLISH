using UnityEngine;
using UnityEngine.UI;

public class EvaluationScoreDisplayByLivesFoodies : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public int scoreFontSize = 125;
    public int timeFontSize = 110;

    private void Start()
    {
        int lives = PlayerPrefs.GetInt("FoodiesGameLives", 0);
        float timeUsed = PlayerPrefs.GetFloat("FoodiesGameTimeUsed", 0f);

        float score = 0f;
        switch (lives)
        {
            case 8: score = 100f; break;
            case 7: score = 87.5f; break;
            case 6: score = 75f; break;
            case 5: score = 62.5f; break;
            case 4: score = 50f; break;
            case 3: score = 37.5f; break;
            case 2: score = 25f; break;
            case 1: score = 12.5f; break;
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
