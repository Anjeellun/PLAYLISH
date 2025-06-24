using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeatherDragDropGame : MonoBehaviour
{
    public GameObject[] bases; 
    public GameObject[] buttons;
    public Button btnPlayAgain;
    public Button btnFinish;
    public Text timerText;
    public float timeLeft = 240f;
    public GameObject gameOverPopup;

    private bool timerRunning = false;
    private Vector3[] originalButtonPositions;
    private bool[] isCorrectlyPlaced;

    private void Start()
    {
        PlayerPrefs.SetString("LastPlayedGameScene", SceneManager.GetActiveScene().name);

        originalButtonPositions = new Vector3[buttons.Length];
        isCorrectlyPlaced = new bool[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            originalButtonPositions[i] = buttons[i].transform.position;
            buttons[i].GetComponent<Button>().onClick.AddListener(StartTimer);
        }

        if (btnPlayAgain != null)
            btnPlayAgain.onClick.AddListener(ResetGame);

        if (btnFinish != null)
            btnFinish.onClick.AddListener(OnFinishClick);

        ResetGame();
    }

    private void Update()
    {
        if (timerRunning && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerText();
        }
        else if (timeLeft <= 0 && timerRunning)
        {
            timerRunning = false;
            if (gameOverPopup != null)
                gameOverPopup.SetActive(true);
        }
    }

    private void StartTimer()
    {
        if (!timerRunning) timerRunning = true;
    }

    private void UpdateTimerText()
    {
        timerText.text = Mathf.Round(timeLeft).ToString();
    }

    private void ResetGame()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.position = originalButtonPositions[i];
            isCorrectlyPlaced[i] = false;
        }

        timeLeft = 240f;
        timerRunning = false;
        UpdateTimerText();

        if (gameOverPopup != null)
            gameOverPopup.SetActive(false);
    }

    private void OnFinishClick()
    {
        int correctCount = 0;
        int wrongCount = 0;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] == null)
            {
                wrongCount++;
                continue;
            }

            WeatherDragDrop dragDropScript = buttons[i].GetComponent<WeatherDragDrop>();
            if (dragDropScript != null)
            {
                if (dragDropScript.correctDropZone == null)
                {
                    wrongCount++;
                    continue;
                }

                if (dragDropScript.IsCorrectlyPlaced())
                    correctCount++;
                else
                    wrongCount++;
            }
            else
            {
                wrongCount++;
            }
        }

        PlayerPrefs.SetInt("WeatherGameScore", correctCount);
        PlayerPrefs.SetInt("WeatherGameWrong", wrongCount);

        timerRunning = false;

        if (correctCount == 12)
            SceneManager.LoadScene("Game-Evaluation-1");
        else if (correctCount >= 8 && correctCount <= 11)
            SceneManager.LoadScene("Game-Evaluation-2");
        else if (correctCount >= 5 && correctCount <= 7)
            SceneManager.LoadScene("Game-Evaluation-3");
        else
            SceneManager.LoadScene("Game-Evaluation-4");
    }

    public void OnDrop(GameObject button, GameObject baseObject)
    {
        int buttonIndex = System.Array.IndexOf(buttons, button);
        int baseIndex = System.Array.IndexOf(bases, baseObject);

        if (buttonIndex == baseIndex)
        {
            isCorrectlyPlaced[buttonIndex] = true;
            button.transform.position = baseObject.transform.position;
        }
        else
        {
            isCorrectlyPlaced[buttonIndex] = false;
            button.transform.position = originalButtonPositions[buttonIndex];
        }
    }

    private void OnDestroy()
    {
        timerRunning = false;

        if (buttons != null)
        {
            foreach (var button in buttons)
            {
                if (button != null)
                {
                    var dragDropScript = button.GetComponent<WeatherDragDrop>();
                    if (dragDropScript != null)
                        dragDropScript.enabled = false;
                }
            }
        }

        if (btnPlayAgain != null) btnPlayAgain.onClick.RemoveAllListeners();
        if (btnFinish != null) btnFinish.onClick.RemoveAllListeners();
    }
}
