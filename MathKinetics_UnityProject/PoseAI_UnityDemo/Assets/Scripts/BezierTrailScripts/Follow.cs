using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Unity Tutorial How To Make Game Object Or Character Move Along Bezier Curve With Simple C# Script
*    Author: Alexander Zotov
*    Date:  10 Feb 2023
*    Availability: https://www.youtube.com/watch?v=11ofnLOE8pw&t=714s
*   
*   With slight modifications made by myself
***************************************************************************************/

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;
    public Transform startPosition;
    public Transform startAnchor;

    public int routeToGo;

    private float tParam;

    private Vector2 objectPosition;

    private float speedModifier;

    public bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {

        tParam = 0f;
        speedModifier = 1f;

        startPosition = GameObject.Find("Controlpoint_Start").GetComponent<Transform>();
        startAnchor = GameObject.Find("Controlpoint_Start_anchor").GetComponent<Transform>();
        routes[0] = GameObject.Find("Route_math").GetComponent<Transform>();
        routes[1] = GameObject.Find("Route_index1").GetComponent<Transform>();
        routes[2] = GameObject.Find("Route_index2").GetComponent<Transform>();
        routes[3] = GameObject.Find("Route_index3").GetComponent<Transform>();
        routes[4] = GameObject.Find("Route_index4").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;
        
        Vector2 p0 = startPosition.position;
        Vector2 p1 = startAnchor.position;
        Vector2 p2 = routes[routeNum].GetChild(0).position;
        Vector2 p3 = routes[routeNum].GetChild(1).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        
        coroutineAllowed = false;
    }
}
