using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FoodiesDragDropGame : MonoBehaviour
{
    public GameObject[] plates;
    public GameObject[] foods;
    public Button btnPlayAgain;
    public Button btnFinish;
    public Text timerText;
    public float timeLeft = 240f;
    public GameObject gameOverPopup;

    private bool timerRunning = false;
    private Vector3[] originalFoodPositions;
    private bool[] isCorrectlyPlaced;

    private void Start()
    {
        PlayerPrefs.SetString("LastPlayedGameScene", SceneManager.GetActiveScene().name);

        originalFoodPositions = new Vector3[foods.Length];
        isCorrectlyPlaced = new bool[foods.Length];

        for (int i = 0; i < foods.Length; i++)
        {
            originalFoodPositions[i] = foods[i].transform.position;
            foods[i].GetComponent<Button>()?.onClick.AddListener(StartTimer);
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
        for (int i = 0; i < foods.Length; i++)
        {
            foods[i].transform.position = originalFoodPositions[i];
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

        for (int i = 0; i < foods.Length; i++)
        {
            if (foods[i] == null)
            {
                wrongCount++;
                continue;
            }

            FoodiesDragDrop dragDropScript = foods[i].GetComponent<FoodiesDragDrop>();
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

        PlayerPrefs.SetInt("FoodAndDrinkGameScore", correctCount);
        PlayerPrefs.SetInt("FoodAndDrinkGameWrong", wrongCount);

        timerRunning = false;

        if (correctCount == 12)
            SceneManager.LoadScene("Foodies-Game-Evaluation-1");
        else if (correctCount >= 8 && correctCount <= 11)
            SceneManager.LoadScene("Foodies-Game-Evaluation-2");
        else if (correctCount >= 5 && correctCount <= 7)
            SceneManager.LoadScene("Foodies-Game-Evaluation-3");
        else
            SceneManager.LoadScene("Foodies-Game-Evaluation-4");
    }

    public void OnDrop(GameObject food, GameObject plate)
    {
        int foodIndex = System.Array.IndexOf(foods, food);
        int plateIndex = System.Array.IndexOf(plates, plate);

        if (foodIndex == plateIndex)
        {
            isCorrectlyPlaced[foodIndex] = true;
            food.transform.position = plate.transform.position;
        }
        else
        {
            isCorrectlyPlaced[foodIndex] = false;
            food.transform.position = originalFoodPositions[foodIndex];
        }
    }

    private void OnDestroy()
    {
        timerRunning = false;

        if (foods != null)
        {
            foreach (var food in foods)
            {
                if (food != null)
                {
                    var dragDropScript = food.GetComponent<FoodiesDragDrop>();
                    if (dragDropScript != null)
                        dragDropScript.enabled = false;
                }
            }
        }

        if (btnPlayAgain != null) btnPlayAgain.onClick.RemoveAllListeners();
        if (btnFinish != null) btnFinish.onClick.RemoveAllListeners();
    }
}
