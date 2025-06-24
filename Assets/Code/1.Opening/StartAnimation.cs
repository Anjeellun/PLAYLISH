using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    public Animator playlishAnimator;

    void Start()
    {
        playlishAnimator.SetTrigger("ShowTittle");  
    }
}