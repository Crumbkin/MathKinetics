using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{

    public float zPos;
    public AudioSource sound_effect;
    
    //public float timedDestroy;
    //public float endtime;

    private void Awake()
    {
        if (tag == "bomb")
        {
            GameObject SoundEffects = GameObject.Find("SoundEffects");
            GameObject child = SoundEffects.transform.GetChild(0).gameObject;
            sound_effect = child.GetComponent<AudioSource>();
        }
        else if (tag == "sphere")
        {
            GameObject SoundEffects = GameObject.Find("SoundEffects");
            GameObject child = SoundEffects.transform.GetChild(1).gameObject;
            sound_effect = child.GetComponent<AudioSource>();
        }
        else if (tag == "wall")
        {
            GameObject SoundEffects = GameObject.Find("SoundEffects");
            GameObject child = SoundEffects.transform.GetChild(2).gameObject;
            sound_effect = child.GetComponent<AudioSource>();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCollision")
        {
            sound_effect.Play();
            Destroy(gameObject);
        }
    }

    void Update()
    {
        zPos = gameObject.transform.position.z;
        if (zPos < -10)
        {
            Destroy(gameObject);
        }



    }
}
