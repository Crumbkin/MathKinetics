using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using PoseAI;


public class UI_InputWindow : MonoBehaviour
{

    public GameObject nameinputrequesttitle;
    public GameObject naming;
    public GameObject score;

    public PoseAICharacterController poseai_controller_script;
    public GameObject Player;

    public Highscore_Table highscoreTable;

    public GameObject keyboardWindow;

    private string input;

    string namingText;
    public bool confirmname = false;

    private GameObject distanceObj;
    float distance;
    public int distance_score;

    bool confirmed = false;

    private DictationRecognizer dictationRecognizer;

    //checking if Dictation is listening
    public GameObject micListening;
    public TextMeshProUGUI micListening_text;
    public string listening;


    public void Start()
    {
        Hide(keyboardWindow);

        Player = GameObject.Find("PlayerArmature Variant");
        poseai_controller_script = Player.GetComponent<PoseAICharacterController>();
        distance = poseai_controller_script.GetCurrentDistance();
        distance_score = Mathf.FloorToInt(distance);

        //distanceObj = GameObject.Find("distance_calc");
        //distance_score = Mathf.Abs((int)distanceObj.transform.position.z);
        score.GetComponent<TextMeshProUGUI>().text = "Score:" + distance_score.ToString();



        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        StartRecording();
        dictationRecognizer.Start();

        micListening = GameObject.Find("Canvas/Dictation");
        micListening_text = micListening.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        //checking whether KeywordRecognizer is listening
        micListening_text.text = "DictationRecognizer: " + dictationRecognizer.Status.ToString();
    }

    //Is required to avoid the problem of having the phraserecognized not shutdown from keywordrecognizer
    public void StartRecording()
    {
        // Shutdown the PhraseRecognitionSystem. This controls the KeywordRecognizers.
        if (PhraseRecognitionSystem.Status == SpeechSystemStatus.Running)
        {
            PhraseRecognitionSystem.Shutdown();
        }

        StartCoroutine(StartRecordingWhenPossible());
    }

    private IEnumerator StartRecordingWhenPossible()
    {
        while (PhraseRecognitionSystem.Status == SpeechSystemStatus.Running)
        {
            yield return null;
        }

        dictationRecognizer.Start();
    }



    private void DictationRecognizer_DictationHypothesis(string text)
    { 

        if (text != "okay" && text != "reset" && confirmed == false)
        {
                Debug.Log("thrd step");
                namingText = text;
                Debug.Log("dictation=" + text);
            if(text.Length <= 20)
            {
                naming.GetComponent<TextMeshProUGUI>().text = namingText;
                confirmname = true;
            }
            else
            {
                naming.GetComponent<TextMeshProUGUI>().text = "Name is too long!";
                confirmname = false;
            }
        }

        if (text == "okay" && confirmname == true)
        {
            highscoreTable.AddHighscoreEntry(distance_score, namingText);
            Hide(this.gameObject);
            confirmed = true;
            //if you get errors for closing the game, might not be necessary
            //dictationRecognizer.Stop();
            //dictationRecognizer.Dispose();
        }
        if(text == "reset")
        {
            //if you get errors for closing the game, might not be necessary
            dictationRecognizer.Stop();
            dictationRecognizer.Dispose();
            //Destroy(distanceObj.gameObject);
            SceneManager.LoadScene(0);
        }

    }
    public void ResetGame()
    {
        //if you get errors for closing the game, might not be necessary
        dictationRecognizer.Stop();
        dictationRecognizer.Dispose();
        //Destroy(distanceObj.gameObject);
        SceneManager.LoadScene(0);
    }



    public void Show(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void Hide(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void CancelNameinput()
    {
        //Trigger for Button
        highscoreTable.AddHighscoreEntry(distance_score, "Unknown");
        Hide(this.gameObject);
        confirmed = true;
    }

    public void ReadStringInput(string name)
    {
        input = name;
        Debug.Log("this is your keyboard input" + input);
        highscoreTable.AddHighscoreEntry(distance_score, input);
        Hide(this.gameObject);
        confirmed = true;
    }
}
