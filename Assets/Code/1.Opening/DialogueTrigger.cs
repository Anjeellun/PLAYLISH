using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue1 dialogue;
    public void TriggerDialogue ()
    {
       FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
 