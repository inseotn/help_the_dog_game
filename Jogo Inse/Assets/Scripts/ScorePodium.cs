using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScorePodium : MonoBehaviour
{
    public Text Score;
    public Text highScore;


   


    void Start()
    {
        //highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void Update()
    {
        
    }

    public void  RollDice()
    {
        int number = Random.Range(1, 7);
        Score.text = number.ToString();


        PlayerPrefs.SetInt("HighScore", number);

    }
}
