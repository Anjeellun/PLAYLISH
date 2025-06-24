using UnityEngine;
using UnityEngine.SceneManagement;

public class LearningOption : MonoBehaviour
{
    public GameObject popUpAlert; 

    public void GoToNextScene()
    {
        SceneManager.LoadScene("Learning-Option"); 
    }

    public void ClosePopUp()
    {
        popUpAlert.SetActive(false);
    }
}
