using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsShowHide : MonoBehaviour
{
    public GameObject settings;
    public int pressed = 0;
    public void Show()
    {
        if (settings.activeSelf == true)
        {
            settings.SetActive(false);
        }
        else
        {
            settings.SetActive(true);
        }
        
    }

    public void Hide()
    {
        settings.SetActive(false);
    }

    public void ResetGame()
    {
        //if you get errors for closing the game, might not be necessary
        //dictationRecognizer.Stop();
        //dictationRecognizer.Dispose();
        //Destroy(distanceObj.gameObject);
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
