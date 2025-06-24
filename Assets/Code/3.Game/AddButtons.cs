using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{

    [SerializeField]
    private Transform puzzleField;

    [SerializeField]
    private GameObject btn;

    void Awake()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject newBtn = Instantiate(btn, puzzleField);
            newBtn.name = "" + i;
            newBtn.transform.SetParent(puzzleField, false);
        }
    }
}
