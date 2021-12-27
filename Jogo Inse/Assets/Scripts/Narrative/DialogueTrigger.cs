using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Triggers the dialogue to happen by calling the StartDialogue(class dialogue) function from class DialogueManager. Assign this script to the trigger algorithm.
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}