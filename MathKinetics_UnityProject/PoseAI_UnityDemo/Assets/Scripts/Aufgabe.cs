using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.UI;
using TMPro;
using Humanizer;
using System.Threading;
using System.Globalization;
using UnityEngine.SceneManagement;

public class Aufgabe : MonoBehaviour
{
    //Script to control all the mental exercizes, timer, etc


    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    internal static object score;
    public string str_result_german = "";
    public string str_result_italian = "";
    public string str_result_english = "";
    public string text = "";
    public int completed = 0;
    public int streakCounter = 0;
    public Animator streakFire_image;


    public int bonusTime = 10;
    int result;

    public string currentMath;
    public string oldMath;
    public float elapsedMathTime;
    public Animator feedbackNext;

    Queue<string[]> aufgabeQueue = new Queue<string[]>();


    public GameObject tutorial_image;
    public Image tut;
    public bool animateTut = true;
    public GameObject tutText;
    public GameObject startTut_background;


    int old_arithmetic;

    
    public float disappearTimer = 0;
    public bool fadingIn = false;

    
    public bool text_fading_add = false;
    public float disappearTimer_Time = 0;

    public TextMeshProUGUI correctObj;

    public Animator mathPop;
    public Animator index1Pop;
    public Animator index2Pop;
    public Animator index3Pop;
    public Animator index4Pop;

    

    public GameObject trail;
    public GameObject trail_prefab;
    private GameObject trail_instance;
    public Transform startPosition;


    public TextMeshProUGUI[] mathobjArr;
    public TextMeshProUGUI youranswer;

    public TextMeshProUGUI timeObj;

    public string[] m_Keywords;

    public float timeValue = 90;
    public float startTime = 10;

    //Feedbacktimer
    public TextMeshProUGUI feedback_time_Obj;
    public Animator myAnimationController;

    //Microphone name
    string MicrophoneName;
    public GameObject micro;
    public TextMeshProUGUI microname_text;
    public GameObject micListening;
    public TextMeshProUGUI micListening_text;
    public string listening;

    public bool showAnswer = false;
    public float countAnswer = 0f;

    public AudioSource sound_effect;

    public void Start()
    {
        //get audiosource correct
        GameObject SoundEffects = GameObject.Find("SoundEffects");
        GameObject child = SoundEffects.transform.GetChild(4).gameObject;
        sound_effect = child.GetComponent<AudioSource>();


        tut = tutorial_image.GetComponent<Image>();

        
        micro = GameObject.Find("Canvas/MicrophoneName");
        micListening = GameObject.Find("Canvas/KeywordRecognizerListening");

        microname_text = micro.GetComponent<TextMeshProUGUI>();

        micListening_text = micListening.GetComponent<TextMeshProUGUI>();


        // Create a list and this list to the Keywords for the Voicerecognition
        List<string> list = new List<string>();

        list.Add("next");
        /*
        for (int i = 0; i < 201; i++)
        {
            // Turns numbers into words (german)
            list.Add(i.ToWords());
            // Turns numbers into words in italian and english
            //list.Add(i.ToWords(new CultureInfo("it")));
            list.Add(i.ToWords(new CultureInfo("en")));
            //Debug.Log(i.ToWords(new CultureInfo("en")));
            
        }
        */
        /*
        //create russian keywords
        for (int i = 0; i < 101; i++)
        {
            list.Add(i.ToWords(new CultureInfo("ru")));
            Debug.Log(i.ToWords(new CultureInfo("ru")));

        }
        */
        /*
        //create italian keywords
        for (int i = 0; i < 101; i++)
        {
            list.Add(i.ToWords(new CultureInfo("it")));
            Debug.Log(i.ToWords(new CultureInfo("it")));

        }
        */

        //create english keywords
        for (int i = 0; i < 101; i++)
        {
            list.Add(i.ToWords(new CultureInfo("en")));
            
        }
        //create german keywords
        list.Add("null");
        list.Add("eins");
        //toWords does not translate one in german correctly:
        //Debug.Log("one in german = " + 1.ToWords());
        for (int i = 2; i < 101; i++)
        {
            list.Add(i.ToWords());
        }
        

        m_Keywords = list.ToArray();


        /*
        for(int i = 0; i< 402; i++)
        {
            Debug.Log("mkey: " + m_Keywords[i]);
        }
        */

        // If you want to make a code with actions:
        //actions.Add("eins", Number);
        //keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());



        keywordRecognizer = new KeywordRecognizer(m_Keywords, ConfidenceLevel.Low);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        StartRecording();


        //checking if the keywords are properly parsed to the keyword recognizer:
        /*
        List<string> testlist = new List<string>();
        testlist = keywordRecognizer.Keywords.ToList();
        string[] keywordArray = testlist.ToArray();
        for (int i = 0; i < 101; i++)
        {
            Debug.Log("key: " + keywordArray[i]);
        }
        */
        
        timeObj.text = "90";

    }


    public void textoverride()
        //creates the random math arithmetic and displays it into the text gui
    {
        //if the queue has already 5 accumulated tasks then nothing happens
        if(aufgabeQueue.Count >= 5)
        {
            return;
        }



        System.Random rnd = new System.Random();

        int choose_arithmetic = UnityEngine.Random.Range(1, 5);
        while (choose_arithmetic == old_arithmetic)
        {
            choose_arithmetic = UnityEngine.Random.Range(1, 5);
        }
        old_arithmetic = choose_arithmetic;
        int a;
        int b;
        
        string str_a;
        string str_b;

        

        //multiply
        if (choose_arithmetic == 1)
        {
            a = UnityEngine.Random.Range(1, 11);
            b = UnityEngine.Random.Range(1, 11);
            result = a * b;
            str_a = a.ToString();
            str_b = b.ToString();
            text = str_a + " x " + str_b;
        }
        //add
        else if (choose_arithmetic == 2)
        {
            a = UnityEngine.Random.Range(1, 30);
            b = UnityEngine.Random.Range(1, 30);
            result = a + b;
            str_a = a.ToString();
            str_b = b.ToString();
            text = str_a + " + " + str_b;
        }

        //divide
        else if (choose_arithmetic == 3)
        {


            a = UnityEngine.Random.Range(1, 60); 
            b = UnityEngine.Random.Range(2, a-2);

            int c = a % b;
            a = a - c;
            
            while (a == b)
            {
                a = UnityEngine.Random.Range(1, 60);
                b = UnityEngine.Random.Range(2, a - 2);

                int d = a % b;
                a = a - d;
            }

            result = a / b;
            str_a = a.ToString();
            str_b = b.ToString();
            text = str_a + " : " + str_b;
        }

        //subtract
        else if (choose_arithmetic == 4)
        {
            a = UnityEngine.Random.Range(1, 30);
            b = UnityEngine.Random.Range(1, a);
            result = a - b;
            str_a = a.ToString();
            str_b = b.ToString();
            text = str_a + " - " + str_b;
        }

       string[] aufgabe = new string[] { text, result.ToString() };
       aufgabeQueue.Enqueue(aufgabe);
       PrintAufgabe();
       Debug.Log(text);
       oldMath = aufgabeQueue.Peek()[0];

       if (aufgabeQueue.Count == 1)
       {
           mathPop.SetTrigger("playPop");
       }
       else if(aufgabeQueue.Count == 2)
       {
            index1Pop.SetTrigger("playIndex1");
       }
       else if (aufgabeQueue.Count == 3)
       {
           index2Pop.SetTrigger("playIndex2");
       }
       else if (aufgabeQueue.Count == 4)
       {
           index3Pop.SetTrigger("playIndex3");
       }
       else if (aufgabeQueue.Count == 5)
       {
           index4Pop.SetTrigger("playIndex4");
       }
    }

    public void QuestionTrail()
    {
        trail_instance = Instantiate(trail_prefab, startPosition.position, Quaternion.identity) as GameObject;

        if (aufgabeQueue.Count == 0)
        {
            trail_instance.GetComponent<Follow>().routeToGo = 0;
        }
        else if (aufgabeQueue.Count == 1)
        {
            trail_instance.GetComponent<Follow>().routeToGo = 1;
        }
        else if (aufgabeQueue.Count == 2)
        {
            trail_instance.GetComponent<Follow>().routeToGo = 2;
        }
        else if (aufgabeQueue.Count == 3)
        {
            trail_instance.GetComponent<Follow>().routeToGo = 3;
        }
        else if (aufgabeQueue.Count == 4)
        {
            trail_instance.GetComponent<Follow>().routeToGo = 4;
        }
        else if (aufgabeQueue.Count == 5)
        {
            trail_instance.GetComponent<Follow>().routeToGo = 5;
        }

        trail_instance.GetComponent<Follow>().coroutineAllowed = true;
    }

    private void Update()
    {
        //counting to delete youranswer text after 1.5f seconds
        if(showAnswer == true)
        {
            countAnswer += Time.deltaTime;
        }

        if(countAnswer >= 1.5f)
        {
            countAnswer = 0;
            //youranswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Italic;
            youranswer.GetComponent<TextMeshProUGUI>().text = "";
            showAnswer = false;
        }


        //checking whether KeywordRecognizer is listening
        if (keywordRecognizer.IsRunning)
        {
            listening = "ON";
        }
        else
        {
            listening = "OFF";
        }
        micListening_text.text = "KeywordRecognizer: " + listening;
        MicrophoneName = Microphone.devices[0];
        microname_text.text = MicrophoneName;


        if (aufgabeQueue.Count != 0)
        {
            currentMath = aufgabeQueue.Peek()[0];
            if (currentMath == oldMath)
            {
                elapsedMathTime += Time.deltaTime;
            }

            if (elapsedMathTime >= 10f)
            {
                feedbackNext.SetTrigger("playNextTip");
                elapsedMathTime = 0;
            }
        }
            




        // fades the feedback for correct or wrong
        if (fadingIn)
        {
            FadeIn();
        }
        else if(correctObj.color.a != 0)
        {
            correctObj.CrossFadeAlpha(0, 0.5f, false);
            
        }






        startTime -= Time.deltaTime;
        if (startTime <= 0)
        {
            //timer countdown
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
                int timer_int = (int)timeValue;
                timeObj.text = timer_int.ToString();
            }
            else
            {
                Debug.Log("Game Over");

                StartGameover();
                //highscoreTable_script = Eventsystem_Distance.GetComponent<distance_Calc>();
            }

        }

        //tutorial
        if(animateTut == true)
        {

            if (startTime >= 9 && startTime <= 9)
            {
                tutorial_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("jump_02");
                tutText.GetComponent<TextMeshProUGUI>().text = "How to JUMP";
            }

            else if (startTime >= 8 && startTime < 9)
            {
                tutorial_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("jump_01");
            }
            else if (startTime >= 7 && startTime < 8)
            {
                tutorial_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("jump_02");
            }
            else if (startTime >= 6 && startTime < 7)
            {
                tutorial_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("jump_01");
            }
            else if (startTime >= 5 && startTime < 6)
            {
                tutorial_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("jump_02");
            }
            else if (startTime >= 4 && startTime < 5)
            {
                tutText.GetComponent<TextMeshProUGUI>().text = "How to SPRINT";
                tutText.GetComponent<RectTransform>().localPosition = new Vector3(420, 215.62f, 0);
                tutorial_image.GetComponent<RectTransform>().localPosition = new Vector3(420, -111.25f, 0);
                tutorial_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprint_01");
            }
            else if (startTime >= 3 && startTime < 4)
            {
                tutorial_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprint_02");
            }
            else if (startTime >= 2 && startTime < 3)
            {
                tutorial_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprint_01");
            }
            else if (startTime >= 1 && startTime < 2)
            {
                tutorial_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprint_02");
            }
            else if (startTime >= 0.5 && startTime < 1)
            {
                tutorial_image.GetComponent<Image>().sprite = Resources.Load<Sprite>("sprint_01");
            }
            else if (startTime <= 0)
            {
                tut.color = new Color(tut.color.r, tut.color.g, tut.color.b, 0f);
                animateTut = false;
                startTut_background.SetActive(false);
                tutText.SetActive(false);
            }
        }
        

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

        keywordRecognizer.Start();
    }

    public void StartGameover()
    {
        // Shutdown the PhraseRecognitionSystem. This controls the KeywordRecognizers.
        if (PhraseRecognitionSystem.Status == SpeechSystemStatus.Running)
        {
            PhraseRecognitionSystem.Shutdown();
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }

        StartCoroutine(StartWhenPossible());
    }

    private IEnumerator StartWhenPossible()
    {
        while (PhraseRecognitionSystem.Status == SpeechSystemStatus.Running)
        {
            yield return null;
        }

        SceneManager.LoadScene(1);
    }

    void FadeIn()
    {
        //function for fading
        correctObj.CrossFadeAlpha(1, 0.5f, false);
        disappearTimer += Time.deltaTime;
        if(correctObj.color.a == 1 && disappearTimer > 1.5f)
        {
            fadingIn = false;
            disappearTimer = 0;
        }

    }



    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        //youranswer.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        youranswer.GetComponent<TextMeshProUGUI>().text = speech.text;
        showAnswer = true;
        countAnswer = 0;

        Debug.Log(speech.text);

        //If the stack is empty jump out of function right away 
        if (aufgabeQueue.Count == 0)
        {
            return;
        }

        string resultStr = aufgabeQueue.Peek()[1];
        int resultInt = int.Parse(resultStr);
        Debug.Log("expected result:" + resultStr);
        //exception as germanresult does not translate one correctly
        string germanResult = resultInt.ToWords();
        if(germanResult == "ein")
        {
            germanResult = "eins";
        }
        //if (speech.text == resultInt.ToWords(new CultureInfo("it")) || speech.text == resultInt.ToWords(new CultureInfo("en")) || speech.text == germanResult)
        if (speech.text == resultInt.ToWords(new CultureInfo("en")) || speech.text == germanResult)
        //if (speech.text == resultInt.ToWords(new CultureInfo("en")))
        //if (speech.text == resultInt.ToWords(new CultureInfo("it")))
        {
            fadingIn = true;
            correctObj.color = Color.green;
            correctObj.text = "correct!";
            completed += 1;
            timeValue += bonusTime;
            feedback_time_Obj.text = bonusTime.ToString();
            myAnimationController.SetTrigger("plus");
            //remove up
            aufgabeQueue.Dequeue();
            PrintAufgabe();
            if (aufgabeQueue.Count != 0)
            {
                oldMath = aufgabeQueue.Peek()[0];
            }
            mathPop.SetTrigger("playPop");
            sound_effect.Play();
            streakCounter += 1;
            if(streakCounter == 5)
            {
                streakFire_image.SetTrigger("streakFire");
            }
        }
        else if(speech.text != "next")
        {
            fadingIn = true;
            correctObj.color = Color.red;
            correctObj.text = "wrong!";
            streakCounter = 0;
        }
        if(speech.text == "next")
        {
            aufgabeQueue.Dequeue();
            PrintAufgabe();
            oldMath = aufgabeQueue.Peek()[0];
        }

    }
    


    private void PrintAufgabe()
    {
        for (int i = 0; i < mathobjArr.Length; i++)
        {
            if (i < aufgabeQueue.Count)
            {
                mathobjArr[i].GetComponent<TextMeshProUGUI>().text = aufgabeQueue.ElementAt(i)[0];
            }
            else
            {
                mathobjArr[i].GetComponent<TextMeshProUGUI>().text = "";
            }

        }
    }

    
}
