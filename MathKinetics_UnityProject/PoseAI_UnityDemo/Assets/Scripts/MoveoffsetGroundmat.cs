using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveoffsetGroundmat : MonoBehaviour
{
    // Scroll the main texture based on time

    float scrollSpeed = 0f;
    float StartTimeValue = 10;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        //START TIMER COUNTDOWN
        if (StartTimeValue > 0)
        {
            StartTimeValue -= Time.deltaTime;
        }
        else
        {
            scrollSpeed = -0.15f;
        }
        float offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(0, offset);
    }

}
