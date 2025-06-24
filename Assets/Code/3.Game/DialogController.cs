using UnityEngine;

public class DialogController : MonoBehaviour
{
    public GameObject dialogPanel; 
    public GameObject levelPanel;  
    public float displayDuration = 10f; 
    private bool isDialogActive = true; 

    void Start()
    {
        dialogPanel.SetActive(true);
        levelPanel.SetActive(false);

        Invoke(nameof(ShowLevels), displayDuration);
    }

    void Update()
    {
        if (isDialogActive && Input.GetMouseButtonDown(0))
        {
            ShowLevels();
        }
    }

    void ShowLevels()
    {
        dialogPanel.SetActive(false);
        levelPanel.SetActive(true);
        isDialogActive = false; 
    }
}
