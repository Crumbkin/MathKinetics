using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Trigger_endlessrunner : MonoBehaviour
{
    public GameObject aufgabe;
    public TextMeshProUGUI feedback_time_Obj;

    public GameObject feedback_minus;
    public Animator myAnimationController;

    private GameObject Player;
    private Transform Position;




    public GameObject ExplosionVfx;
    public GameObject DustCloudVfx;


    public GameObject streakFire;


    public bool streakReached = false;

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            Debug.Log("Collisio has loaded level 0");
            aufgabe = GameObject.Find("Canvas/Math_Ui/math");
            feedback_minus = GameObject.Find("Canvas/Timer_UI/timerFeedback_minus");
            myAnimationController = feedback_minus.GetComponent<Animator>();
            
        }
        
    }

    private void Start()
    {
        Player = GameObject.Find("PlayerArmature Variant");
        Position = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        try
        {
            //Debug.Log("streak; " + aufgabe.GetComponent<Aufgabe>().streakCounter);
            if (aufgabe.GetComponent<Aufgabe>().streakCounter >= 5 && streakReached == false)
            {
                streakFire.SetActive(true);
                streakReached = true;
            }
            if (aufgabe.GetComponent<Aufgabe>().streakCounter <= 4 && streakReached == true)
            {
                streakFire.SetActive(false);
                streakReached = false;
            }
        }
        catch (MissingReferenceException ex)
        {
            Debug.Log("skipping, because there is no aufgabe object");
        }


    }

    void OnTriggerEnter(Collider other)
    {
        //Script to trigger collision with obstacles ingame and correct timer accordingly
        int bombMinustime = 15;
        int wallMinustime= 7;

        if (other.tag == "sphere")
        {
            //this is the question obstacle that will trigger an exercize from another script
            aufgabe.GetComponent<Aufgabe>().QuestionTrail();
            aufgabe.GetComponent<Aufgabe>().textoverride();
        }
        if (other.tag == "bomb")
        {
            aufgabe.GetComponent<Aufgabe>().timeValue -= bombMinustime;
            feedback_time_Obj.text = "-" + bombMinustime.ToString();
            myAnimationController.SetTrigger("minus");
            Instantiate(ExplosionVfx, Position.position, Quaternion.identity);
        }
        if (other.tag == "wall")
        {
            aufgabe.GetComponent<Aufgabe>().timeValue -= wallMinustime;
            feedback_time_Obj.text = "-" + wallMinustime.ToString();
            myAnimationController.SetTrigger("minus");
            Instantiate(DustCloudVfx, Position.position, Quaternion.identity);
        }
    }

}
