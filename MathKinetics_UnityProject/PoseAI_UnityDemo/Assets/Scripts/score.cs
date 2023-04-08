using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public GameObject aufgabe;
    public Animator scoreAnim;
    public int oldscore = 0;
    public float disappearTimer = 0;
    public bool startTime = false;

    //OLD SCRIPT TO COUNT HOW MANY EXERCIZES ANSWERED

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        int score = aufgabe.GetComponent<Aufgabe>().completed;
        GetComponent<TextMeshProUGUI>().text = score.ToString();
        if(startTime == true)
        {
            disappearTimer += Time.deltaTime;
            if (disappearTimer > 1.5f)
            {
                scoreAnim.SetBool("AnimTrigger", false);
                disappearTimer = 0;
                startTime = false;
            }
        }
        
        if (oldscore < score)
        {
            startTime = true;
            scoreAnim.SetBool("AnimTrigger", true);
            oldscore = score;
            
        }
        
    }
}
