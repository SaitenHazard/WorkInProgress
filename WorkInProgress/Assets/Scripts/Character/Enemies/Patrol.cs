using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private int numberOfPoints;

    private void Awake()
    {
        numberOfPoints = transform.childCount;
    }

    private void OnEnable()
    {
        findClosestPatrol();
    }

    private void findClosestPatrol()
    {
        GameObject closesPetrol;
        Vector2 minDistance;

        for (int i = 0; i < numberOfPoints; i++)
        {
            
        }
    }

    private void get
}
