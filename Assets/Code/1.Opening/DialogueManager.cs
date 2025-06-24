using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text nameText; 
    public Text dialogueText; 


    private Queue<string> sentences; 
    private int currentDialogCount = 0;
    private int maxDialogCount = 6; 
    private bool isLastSentence = false; 

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue1 dialogue)
    {
        nameText.text = dialogue.name.ToUpper();

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(); 
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines(); 
        StartCoroutine(TypeSentence(sentence)); 

        currentDialogCount++; 

        if (currentDialogCount >= maxDialogCount)
        {
            isLastSentence = true;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);  
        }

        if (isLastSentence)
        {
            yield return new WaitForSeconds(0.30f);
            SceneManager.LoadScene("Playlish-Menu");
        }
    }
    
    void EndDialogue()
    {
        Debug.Log("End of conversation.");
    }
}
