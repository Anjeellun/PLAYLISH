using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Button playAgainButton;
    public GameObject[] panels;

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(PlayAgain);
    }

    private void ResetGame()
    {
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(i == 0);

        WeatherGame weatherGame = FindObjectOfType<WeatherGame>();
        if (weatherGame != null)
            weatherGame.ResetGame();
    }

    public void PlayAgain()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        ResetGame();
    }
}
