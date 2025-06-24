using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainHandler : MonoBehaviour
{
    public void TryAgain()
    {
        string lastPlayedScene = PlayerPrefs.GetString("LastPlayedGameScene", "");
        if (!string.IsNullOrEmpty(lastPlayedScene))
            SceneManager.LoadScene(lastPlayedScene);
    }
}