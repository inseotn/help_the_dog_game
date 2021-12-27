using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text textDisplay;        //texto sendo exibido
    public string[] sentences;      //conjunto de textos a serem exibidos
    private int index;              //índice das sentenças
    public float typingSpeed;
    public GameObject continueButton;
    public Text[] textName;
    public int[] nameIndex;
    public Image currentBaloon;
    public Sprite[] baloonOptions;
    public GameObject nextSceneButton;
    
    public Sprite[] dogFaceEmotion; //Lista de rostinho de cachorro para cada frase do diálogo
    public Image dogCurrentFace;    //Rostinho do cachorro sendo exibido

    public Sprite[] catFaceEmotion; //Lista de rostinho de gato para cada frase do diálogo
    public Image catCurrentFace;           //Rostinho do gato sendo exibido


    public AudioClip fxButton;
    public AudioClip fxWrite;
   

    //sentences = [sentence(1), sentence(2), sentence(3), ..., sentence(n)];
    //index = 1, 2, 3, ..., n;
    //sentence(1) = "Exemplo de sentença!";

    void Start()
    {
        StartCoroutine(Type());
        nextSceneButton.SetActive(false);
    }


    void Update()
    {
        if (textDisplay.text == sentences[index] && index < sentences.Length)
        {
            continueButton.SetActive(true);
           

        }

    }

    /// <summary>
    /// Corrotina para exibir um caracter por vez.
    /// </summary>
    IEnumerator Type() {

        foreach (char letter in sentences[index].ToCharArray())
        {
            dogCurrentFace.sprite = dogFaceEmotion[index];
            catCurrentFace.sprite = catFaceEmotion[index];
            currentBaloon.sprite = baloonOptions[index];
           

            if (nameIndex[index] == 0)
            {
                textName[0].gameObject.SetActive(true);
                
                textName[1].gameObject.SetActive(false);
                
            }
            else if (nameIndex[index] == 1)
            {
                textName[0].gameObject.SetActive(false);
                textName[1].gameObject.SetActive(true);
                
            }

            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
            AudioManager.instance.PlaySound(fxWrite);
        }

        
    }

    /// <summary>
    /// Vai para a próxima sentença.
    /// </summary>
    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            AudioManager.instance.PlaySound(fxButton);
        }
        else{
            //textDisplay.text = "";
            continueButton.SetActive(false);
            nextSceneButton.SetActive(true);
        }
    }
}
