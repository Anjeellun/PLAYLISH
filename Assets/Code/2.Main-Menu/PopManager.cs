using UnityEngine;

public class PopManager : MonoBehaviour
{
    public GameObject popPlay;
    public GameObject popStudy;

    public void ShowPopPlay()
    {
        popPlay.SetActive(true);  
        popStudy.SetActive(false); 
    }

    public void ShowPopStudy()
    {
        popStudy.SetActive(true); 
        popPlay.SetActive(false); 
    }

    public void CloseAllPopUps()
    {
        popPlay.SetActive(false);
        popStudy.SetActive(false);
    }
}