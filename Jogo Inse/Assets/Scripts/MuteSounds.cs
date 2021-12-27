using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSounds : MonoBehaviour


{
    private const bool V = true;
    [SerializeField] Image soundsOnIcon;
    [SerializeField] Image soundsOffIcon;


   

    private bool muted = false;

    // Start is called before the first frame update
    void Start()
    {


        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();

        }

        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
          }
        else
        {
            muted = false;
            AudioListener.pause = false;

        }

        Save();
        UpdateButtonIcon();
    }
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
    void Update()
    {
        
    }

    private void UpdateButtonIcon()
    {
        if(muted == false)
        {
            soundsOnIcon.enabled = true;
            soundsOffIcon.enabled = false;

        }
        else
        {
            soundsOffIcon.enabled = true;
            soundsOnIcon.enabled = false;
        }
    }

}
