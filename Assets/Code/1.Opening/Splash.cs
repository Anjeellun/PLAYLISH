using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    void Start()
    {
        Invoke("NextScene", 4f); 
    }

    void NextScene()
    {
        SceneManager.LoadScene("Playlish-Opening"); 
    }
}
