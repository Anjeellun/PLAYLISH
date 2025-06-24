using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTeddyTrans : MonoBehaviour
{
    public GameObject currentPanel; 
    public GameObject nextPanel;
    public GameObject btnTeddy;
    public Animator btnTeddyAnimator;

    void Start()
    {
        btnTeddy.SetActive(false);
    }

    public void SwitchPanel()
    {

        currentPanel.SetActive(false);

        nextPanel.SetActive(true);

        ShowButton();
        
    }

    public void ShowButton()
    {
        btnTeddy.SetActive(true);
        btnTeddyAnimator.SetTrigger("AppearTrigger");
    }
}