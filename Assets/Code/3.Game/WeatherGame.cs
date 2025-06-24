
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeatherGame : MonoBehaviour
{
    public GameObject[] panels;
    public InputField[] inputFields;
    public Button[] weatherButtons;
    public Button btnClosePopup;
    public Text[] timerTexts;

    public float timeLeft = 300f;
    private bool timerRunning = false;
    public GameObject gameOverPanel;
    public GameObject incompleteAnswersPopup;

    private string[] userAnswers;
    public string[] correctAnswers;

    private void Start()
    {
        PlayerPrefs.SetString("LastPlayedGameScene", SceneManager.GetActiveScene().name);

        if (weatherButtons.Length != inputFields.Length || inputFields.Length != correctAnswers.Length)
            return;

        for (int i = 0; i < panels.Length; i++)
        {
            Button nextBtn = panels[i].transform.Find("BtnNext")?.GetComponent<Button>();
            if (nextBtn != null && i < panels.Length - 1)
            {
                int idx = i;
                nextBtn.onClick.AddListener(() =>
                {
                    SaveAllAnswers();
                    panels[idx].SetActive(false);
                    panels[idx + 1].SetActive(true);
                    LoadAllAnswers();
                    UpdateTimerText();
                });
            }

            Button backBtn = panels[i].transform.Find("BtnBack")?.GetComponent<Button>();
            if (backBtn != null && i > 0)
            {
                int idx = i;
                backBtn.onClick.AddListener(() =>
                {
                    SaveAllAnswers();
                    panels[idx].SetActive(false);
                    panels[idx - 1].SetActive(true);
                    LoadAllAnswers();
                    UpdateTimerText();
                });
            }

            Button finishBtn = panels[i].transform.Find("BtnFinish")?.GetComponent<Button>();
            if (finishBtn != null && i == panels.Length - 1)
            {
                finishBtn.onClick.AddListener(OnFinishClick);
            }
        }

        for (int i = 0; i < weatherButtons.Length; i++)
        {
            int index = i;
            weatherButtons[i].onClick.AddListener(() =>
            {
                OnClickWeather(index);
                StartTimer();
            });
        }

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
            inputFields[i].text = "";
        }

        foreach (var inputField in inputFields)
        {
            inputField.interactable = false;
        }

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (incompleteAnswersPopup != null) incompleteAnswersPopup.SetActive(false);

        if (btnClosePopup != null)
        {
            btnClosePopup.onClick.AddListener(() =>
            {
                if (incompleteAnswersPopup != null) incompleteAnswersPopup.SetActive(false);
            });
        }

        timeLeft = 300f;
        UpdateTimerText();

        userAnswers = new string[inputFields.Length];
        for (int i = 0; i < userAnswers.Length; i++) userAnswers[i] = "";
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
            ShowGameOver();
        }
    }

    private void SaveAllAnswers()
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i] != null)
                userAnswers[i] = inputFields[i].text.Trim();
        }
    }

    private void LoadAllAnswers()
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i] != null)
                inputFields[i].text = userAnswers[i];
        }
    }

    public void OnFinishClick()
    {
        SaveAllAnswers();

        foreach (var answer in userAnswers)
        {
            if (string.IsNullOrEmpty(answer))
            {
                if (incompleteAnswersPopup != null)
                    incompleteAnswersPopup.SetActive(true);
                return;
            }
        }

        int correctCount = 0, wrongCount = 0;
        for (int i = 0; i < userAnswers.Length; i++)
        {
            if (!string.IsNullOrEmpty(userAnswers[i]) && !string.IsNullOrEmpty(correctAnswers[i]))
            {
                if (userAnswers[i].Trim().ToLower() == correctAnswers[i].Trim().ToLower())
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

        if (correctCount == userAnswers.Length)
            SceneManager.LoadScene("Game-Evaluation-1");
        else if (correctCount >= Mathf.CeilToInt(userAnswers.Length * 0.67f))
            SceneManager.LoadScene("Game-Evaluation-2");
        else if (correctCount >= Mathf.CeilToInt(userAnswers.Length * 0.34f))
            SceneManager.LoadScene("Game-Evaluation-3");
        else
            SceneManager.LoadScene("Game-Evaluation-4");
    }

    public void OnClickWeather(int index)
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            inputFields[i].interactable = (i == index);
            if (i == index)
            {
                inputFields[i].Select();
                inputFields[i].ActivateInputField();
            }
        }
    }

    private int GetActivePanelIndex()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].activeSelf) return i;
        }
        return -1;
    }

    private void UpdateTimerText()
    {
        int idx = GetActivePanelIndex();
        for (int i = 0; i < timerTexts.Length; i++)
        {
            if (timerTexts[i] != null)
                timerTexts[i].text = (i == idx) ? Mathf.Round(timeLeft).ToString() : "";
        }
    }

    private void ShowGameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        timerRunning = false;
    }

    public void StartTimer()
    {
        if (!timerRunning) timerRunning = true;
    }

    public void ResetTimer()
    {
        timeLeft = 0f;
        PlayerPrefs.SetFloat("TimeLeft", timeLeft);
        UpdateTimerText();
        timerRunning = false;
    }

    public void ResetGame()
    {
        foreach (InputField inputField in inputFields)
        {
            inputField.text = "";
            inputField.interactable = false;
        }

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }

        for (int i = 0; i < userAnswers.Length; i++)
        {
            userAnswers[i] = "";
        }

        ResetTimer();
        timerRunning = false;
    }

    private void OnDestroy()
    {
        timerRunning = false;

        if (incompleteAnswersPopup != null)
            incompleteAnswersPopup.SetActive(false);

        if (btnClosePopup != null) btnClosePopup.onClick.RemoveAllListeners();

        foreach (var inputField in inputFields)
        {
            if (inputField != null)
                inputField.interactable = false;
        }

        foreach (var panel in panels)
        {
            if (panel != null)
                panel.SetActive(false);
        }
    }
}