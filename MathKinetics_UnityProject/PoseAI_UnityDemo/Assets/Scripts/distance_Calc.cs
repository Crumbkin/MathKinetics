using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class distance_Calc : MonoBehaviour
{
    public GameObject Position;
    public TextMeshProUGUI distance_text;
    int distance;
    string distance_str;
    int countSceneloads = 0;

    private void Start()
    {
         //DontDestroyOnLoad(Position.gameObject);
        Position = GameObject.Find("distance_calc");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Math.Abs((int)Position.transform.position.z);
        distance_str = distance.ToString();
        distance_text.text = distance_str;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
