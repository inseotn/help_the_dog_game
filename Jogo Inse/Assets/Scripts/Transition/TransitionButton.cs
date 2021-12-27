using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionButton : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
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
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        AudioManager.instance.PlaySound(fxButton);
    }


    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(levelIndex);

    }

}
