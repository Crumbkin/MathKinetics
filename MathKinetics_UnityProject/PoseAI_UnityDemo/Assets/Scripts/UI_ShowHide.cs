using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ShowHide : MonoBehaviour
{
    public GameObject Ui_inputwindow;
    public GameObject highscore;
    public GameObject keyboardWindow;
    public GameObject Dictation;
    public bool keyboardbuttonpressed = false;

    public GameObject keyboard;

    float elapsedTime;
    public Animator feedbackReset;

    public void Awake()
    {
        StartCoroutine(ActivationRoutine(Ui_inputwindow, 4));
        StartCoroutine(ActivationRoutine(Dictation, 4));
        StartCoroutine(ActivationRoutine(highscore, 4));
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= 10)
        {
            feedbackReset.SetTrigger("playNextTip");
        }

    }

    IEnumerator ActivationRoutine(GameObject obj, int time)
    {
        yield return new WaitForSeconds(time);
        Show(obj);

    }
    public void Show(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Hide(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void Exit()
    {
        //function to exit the game with the exit button
        Application.Quit();
    }



    public void Keyboard()
    {
        if (keyboardbuttonpressed == false)
        {
            Show(keyboardWindow);
            keyboardbuttonpressed = true;
            EventSystem.current.SetSelectedGameObject(keyboard);
            keyboard.GetComponent<TMP_InputField>().ActivateInputField();
        }
        else
        {
            Hide(keyboardWindow);
            keyboardbuttonpressed = false;
        }
    }
}
