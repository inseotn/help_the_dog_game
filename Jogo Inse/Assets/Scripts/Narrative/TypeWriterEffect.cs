using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Allows to get the text component

public class TypeWriterEffect : MonoBehaviour
{
    AudioSource typeWriterSound;
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        typeWriterSound = GetComponent<AudioSource>();
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            //Start to play typewriter sound
            if (i == 0)
            {
                typeWriterSound.Play();
            }

            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);

            if (i == fullText.Length - 1)
            {
                typeWriterSound.Stop();
            }
        }
    }
}