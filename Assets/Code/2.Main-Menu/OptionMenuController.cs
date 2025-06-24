using UnityEngine;

public class OptionMenuController : MonoBehaviour
{
    public GameObject optionMenuPopup; 

    public void ShowOptionMenu()
    {
        if (optionMenuPopup != null)
            optionMenuPopup.SetActive(true);
    }

    public void HideOptionMenu()
    {
        if (optionMenuPopup != null)
            optionMenuPopup.SetActive(false);
    }

    public void OnContactUsInstagram()
    {
        Application.OpenURL("https://instagram.com/anjeellun");
    }

    public void OnExitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
