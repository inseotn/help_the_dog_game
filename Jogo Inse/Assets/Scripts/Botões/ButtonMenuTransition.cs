using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenuTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    [SerializeField] GameObject PauseGame;
    public string nameScene;
    public AudioClip fxButton;

    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            LaodNextLevel();

        }
        */
    }
    public void LoadNextLevel(string levelIndex)

    {
        StartCoroutine(LoadLevel(levelIndex));
        AudioManager.instance.PlaySound(fxButton);
    }

    IEnumerator LoadLevel(string levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);


    }

    public void sair()
    {
        Application.Quit();

    }


    public void Pause()
    {
        PauseGame.SetActive(true);
        Time.timeScale = 0f;

    }

    public void Resume()
    {
        PauseGame.SetActive(false);
        Time.timeScale = 1f;
        AudioManager.instance.PlaySound(fxButton);
    }
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nameScene);
        AudioManager.instance.PlaySound(fxButton);
    }


}

