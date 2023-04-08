using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Unity Tutorial How To Make Game Object Or Character Move Along Bezier Curve With Simple C# Script
*    Author: Alexander Zotov
*    Date:  10 Feb 2023
*    Availability: https://www.youtube.com/watch?v=11ofnLOE8pw&t=714s
*
***************************************************************************************/

public class Route : MonoBehaviour
{
[SerializeField]
    private Transform[] controlPoints;


    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for(float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) 
                * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) 
                * controlPoints[3].position;
            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y), new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
        Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y), new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));

    }
}
