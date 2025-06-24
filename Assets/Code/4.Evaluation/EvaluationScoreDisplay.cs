using UnityEngine;
using UnityEngine.UI;

public class EvaluationScoreDisplay : MonoBehaviour
{
    public Text scoreText; 
    public Text wrongText; 
    public int fixedFontSize = 115;

    private void Start()
    {
        int score = PlayerPrefs.GetInt("WeatherGameScore", 0);
        int wrong = PlayerPrefs.GetInt("WeatherGameWrong", 0);

        if (scoreText != null) {
            scoreText.text = $"Right Answers: {score}";
            scoreText.resizeTextForBestFit = false;
            scoreText.fontSize = fixedFontSize;
        }

        if (wrongText != null) {
            wrongText.text = $"Wrong Answers: {wrong}";
            wrongText.resizeTextForBestFit = false;
            wrongText.fontSize = fixedFontSize;
        }
    }
}
