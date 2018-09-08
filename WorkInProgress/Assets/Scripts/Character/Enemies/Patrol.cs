using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private int numberOfPoints;
    private Transform[] childTransforms;
    private Transform m_transform;
    private Transform target;
    private int targetIndex;

    private void Awake()
    {
        childTransforms = GetComponentsInChildren<Transform>();
        m_transform = GetComponentInParent<Transform>();
    }

    public void SetClosestPatrol()
    {
        float distance = Mathf.Infinity;

        for (int i = 0; i < numberOfPoints; i++)
        {
            if(transform != null)
            {
                if (Vector2.Distance(transform.position, childTransforms[i].position) < distance)
                {
                    targetIndex = i;
                }
            }
        }

        SetTarget();
    }

    public void Update()
    {
        CheckTargetReached();
    }

    public void CheckTargetReached()
    {
        if (target.transform.position == m_transform.position)
            SetNextPatrol();
    }

    public Transform GetTarget()
    {
        return target;
    }

    private void SetTarget()
    {
        target = childTransforms[targetIndex];
    }

    public void SetNextPatrol()
    {
        targetIndex++;

        if (targetIndex == numberOfPoints)
            targetIndex = 0;

        SetTarget();
    }
}
