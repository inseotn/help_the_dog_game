using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionClik : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public AudioClip fxClick;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
            AudioManager.instance.PlaySound(fxClick);

        }
        void LoadNextLevel()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

        }


        IEnumerator LoadLevel(int levelIndex)
        {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(1);

            SceneManager.LoadScene(levelIndex);

        }

    }
}
