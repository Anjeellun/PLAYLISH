using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeatherMemoryGame : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;
    public Sprite[] puzzleImages;
    private List<Sprite> puzzleImagesList = new List<Sprite>();
    private List<Button> buttons = new List<Button>();
    [SerializeField]
    private GameObject timerPrefab;
    private Text timerText;
    private float timer = 240f;

    [SerializeField]
    private GameObject gameOverPanel;

    private Button firstSelectedButton = null;
    private Button secondSelectedButton = null;
    private bool isCheckingMatch = false;

    private int currentRound = 0;
    private List<List<Sprite>> roundPairs = new List<List<Sprite>>();

    private int lives = 7;
    private int freeTriesPerRound = 3;
    private int currentFreeTries = 0;

    [SerializeField]
    private Image[] lifeImages;
    [SerializeField]
    private Sprite lifeFullSprite;
    [SerializeField]
    private Sprite lifeEmptySprite;

    void Start()
    {
        PlayerPrefs.SetString("LastPlayedGameScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        GetButtons();
        SetupMiddleButtonAsTimer();
        PrepareRounds();
        LoadRound(currentRound);
        UpdateLifeUI();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        buttons.Clear();
        for (int i = 0; i < objects.Length; i++)
        {
            Button button = objects[i].GetComponent<Button>();
            if (button != null)
            {
                buttons.Add(button);
                button.image.sprite = bgImage;
            }
        }
    }

    void SetupMiddleButtonAsTimer()
    {
        if (buttons.Count >= 5)
        {
            Button middleButton = buttons[4];
            middleButton.onClick.RemoveAllListeners();
            middleButton.interactable = false;
            GameObject timerInstance = Instantiate(timerPrefab, middleButton.transform.parent);
            timerInstance.transform.SetSiblingIndex(middleButton.transform.GetSiblingIndex());
            timerInstance.transform.localPosition = middleButton.transform.localPosition;
            Destroy(middleButton.gameObject);
            timerText = timerInstance.GetComponentInChildren<Text>();
            buttons.RemoveAt(4);
        }
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timerText != null)
                timerText.text = Mathf.CeilToInt(timer).ToString();
        }
        else if (timer <= 0)
        {
            timer = 0;
            if (timerText != null)
                timerText.text = "0";
            if (gameOverPanel != null && !gameOverPanel.activeSelf)
                gameOverPanel.SetActive(true);
            foreach (var btn in buttons)
                btn.interactable = false;
        }
    }

    private IEnumerator CheckMatch()
    {
        isCheckingMatch = true;
        yield return new WaitForSeconds(1f);
        string firstName = puzzleImagesList[buttons.IndexOf(firstSelectedButton)].name;
        string secondName = puzzleImagesList[buttons.IndexOf(secondSelectedButton)].name;
        if (IsMatchingPair(firstName, secondName))
        {
            StartCoroutine(FadeOutButton(firstSelectedButton));
            StartCoroutine(FadeOutButton(secondSelectedButton));
        }
        else
        {
            if (currentFreeTries > 1)
            {
                currentFreeTries--;
            }
            else if (currentFreeTries == 1)
            {
                currentFreeTries--;
            }
            else
            {
                lives--;
                UpdateLifeUI();
                if (lives <= 0)
                {
                    PlayerPrefs.SetInt("WeatherGameLives", 0);
                    float timeUsed = 240f - timer;
                    PlayerPrefs.SetFloat("WeatherGameTimeUsed", timeUsed);
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Evaluation-L4-4");
                    yield break;
                }
            }
            firstSelectedButton.image.sprite = bgImage;
            secondSelectedButton.image.sprite = bgImage;
        }
        firstSelectedButton = null;
        secondSelectedButton = null;
        isCheckingMatch = false;
    }

    private bool IsMatchingPair(string firstName, string secondName)
    {
        return firstName.Substring(0, firstName.Length - 2) == secondName.Substring(0, secondName.Length - 2);
    }

    private IEnumerator FadeOutButton(Button button)
    {
        Image image = button.image;
        Color originalColor = image.color;
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f - t);
            yield return null;
        }
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        button.interactable = false;
        CheckIfRoundComplete();
    }

    private void CheckIfRoundComplete()
    {
        bool allMatched = true;
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i].interactable)
            {
                allMatched = false;
                break;
            }
        }
        if (allMatched)
        {
            if (currentRound < roundPairs.Count - 1)
            {
                currentRound++;
                LoadRound(currentRound);
            }
            else
            {
                PlayerPrefs.SetInt("WeatherGameLives", lives);
                float timeUsed = 240f - timer;
                PlayerPrefs.SetFloat("WeatherGameTimeUsed", timeUsed);

                if (lives == 7)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Evaluation-L4-1");
                else if (lives == 6 || lives == 5)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Evaluation-L4-2");
                else if (lives == 4 || lives == 3)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Evaluation-L4-3");
                else
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Evaluation-L4-4");
            }
        }
    }

    void PrepareRounds()
    {
        Dictionary<string, List<Sprite>> pairDict = new Dictionary<string, List<Sprite>>();
        foreach (var sprite in puzzleImages)
        {
            string baseName = sprite.name.Substring(0, sprite.name.Length - 2);
            if (!pairDict.ContainsKey(baseName))
                pairDict[baseName] = new List<Sprite>();
            pairDict[baseName].Add(sprite);
        }
        List<List<Sprite>> allPairs = new List<List<Sprite>>();
        foreach (var pair in pairDict.Values)
        {
            if (pair.Count == 2)
                allPairs.Add(new List<Sprite>(pair));
        }
        for (int i = 0; i < allPairs.Count; i++)
        {
            var temp = allPairs[i];
            int randIdx = Random.Range(0, allPairs.Count);
            allPairs[i] = allPairs[randIdx];
            allPairs[randIdx] = temp;
        }
        roundPairs.Clear();
        for (int i = 0; i < allPairs.Count; i += 4)
        {
            List<Sprite> batch = new List<Sprite>();
            for (int j = i; j < i + 4 && j < allPairs.Count; j++)
            {
                batch.AddRange(allPairs[j]);
            }
            if (batch.Count == 8)
                roundPairs.Add(batch);
        }
    }

    void LoadRound(int round)
    {
        puzzleImagesList.Clear();
        if (round < roundPairs.Count)
        {
            puzzleImagesList.AddRange(roundPairs[round]);
        }
        else
        {
            return;
        }
        for (int i = 0; i < puzzleImagesList.Count; i++)
        {
            Sprite temp = puzzleImagesList[i];
            int randomIndex = Random.Range(0, puzzleImagesList.Count);
            puzzleImagesList[i] = puzzleImagesList[randomIndex];
            puzzleImagesList[randomIndex] = temp;
        }
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i < puzzleImagesList.Count)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].image.sprite = bgImage;
                buttons[i].interactable = true;
                var img = buttons[i].image;
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
                buttons[i].onClick.RemoveAllListeners();
                int buttonIndex = i;
                buttons[i].onClick.AddListener(() => OnButtonClicked(buttonIndex));
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
        currentFreeTries = freeTriesPerRound;
        UpdateLifeUI();
    }

    private void UpdateLifeUI()
    {
        for (int i = 0; i < lifeImages.Length; i++)
        {
            if (i < lives)
                lifeImages[i].sprite = lifeFullSprite;
            else
                lifeImages[i].sprite = lifeEmptySprite;
        }
    }

    void OnButtonClicked(int index)
    {
        if (isCheckingMatch) return;
        Button selectedButton = buttons[index];
        if (selectedButton == null || selectedButton.image.sprite != bgImage) return;
        selectedButton.image.sprite = puzzleImagesList[index];
        if (firstSelectedButton == null)
        {
            firstSelectedButton = selectedButton;
        }
        else if (secondSelectedButton == null)
        {
            secondSelectedButton = selectedButton;
            StartCoroutine(CheckMatch());
        }
    }
}