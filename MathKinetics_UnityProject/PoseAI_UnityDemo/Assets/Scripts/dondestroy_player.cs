using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dondestroy_player : MonoBehaviour
{
    //Script to keep the player so that you can possibly reset the game with voiceinput "reset"
    void Start()
    {
     
        int players = GameObject.FindGameObjectsWithTag("Player").Length;
        if (players != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

}
