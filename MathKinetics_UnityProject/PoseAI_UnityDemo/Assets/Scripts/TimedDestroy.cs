using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{

    public float timer;
    public float whenDestroy = 15f;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= whenDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
