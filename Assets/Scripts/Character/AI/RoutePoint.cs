using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutePoint : MonoBehaviour
{
    [SerializeField]
    private RoutePoint[] nextPoints;

    public RoutePoint GetNextPoint()
    {
        if (nextPoints.Length == 0)
        {
            throw new System.Exception("There is no next points");
        }
        
        return nextPoints[Random.Range(0, nextPoints.Length)];
    }
}
