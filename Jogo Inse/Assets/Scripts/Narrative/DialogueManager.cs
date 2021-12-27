using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Shows the name and dialogue text of a character. Includes animation and typewriter effect.
/// Must have the following componentes:
/// - Animator with Dialogue animator assigned to it
/// - AudioSource with TypeWritterSound as AudioClip
/// 
/// * nameText and dialogueText have to be assigned through the inspector
/// </summary>
public class DialogueManager : MonoBehaviour
{
    #region Inspector Variables
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    #endregion

    #region Hard-coded Variables
    AudioSource typeWriterSound;
    float delay = 0.1f;
    string currentText = "";
    Queue<string> sentences;
    #endregion

    void Start()
    {
        typeWriterSound = GetComponent<AudioSource>();
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        //Fill the queue of sentences
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
        StartCoroutine(ShowText(sentence));
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

    IEnumerator ShowText(string sentence)
    {
        for (int i = 0; i <= sentence.Length; i++)
        {
            dialogueText.text = "";
            if (i == 0 && animator.GetBool("IsOpen"))
            {
                typeWriterSound.Play();
            }
            dialogueText.text = sentence.Substring(0, i);
            yield return new WaitForSeconds(delay);
            if (i == sentence.Length || !animator.GetBool("IsOpen"))
            {
                typeWriterSound.Stop();
            }
        }
    }
}