using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneFeedback : MonoBehaviour
{
    public AudioSource source;
    public AudioLoudnessDetection detector;


    public bool micBool = false;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        
        //change sprite accordingly
        if (loudness < threshold)
        {
            loudness = 0;   
        }
       

        


        if(micBool == false && loudness > threshold)
        {
            micBool = true;
            ImageChange();
        }
        else if(micBool == true && loudness < threshold)
        {
            micBool = false;
            ImageChange();
        }
        
        //Debug.Log("loudness " + loudness);


    }

    void ImageChange()
    {
        if(micBool == false)
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("microphoneFeedback_Off");
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("microphoneFeedback_On");
        }
    }
}
