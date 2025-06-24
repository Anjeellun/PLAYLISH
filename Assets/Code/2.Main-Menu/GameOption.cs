using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOption : MonoBehaviour
{
    public GameObject popUpAlert; 

    public void GoToNextScene()
    {
        SceneManager.LoadScene("Game-Option"); 
    }

    public void ClosePopUp()
    {
        popUpAlert.SetActive(false);
    }
}
