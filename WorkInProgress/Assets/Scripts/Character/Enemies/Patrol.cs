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

    public AIBase AIBase;

    private void Awake()
    {
        m_transform = AIBase.GetComponentInParent<Transform>();

        childTransforms = GetComponentsInChildren<Transform>();
        numberOfPoints = childTransforms.Length;
    }

    private void Start()
    {
        targetIndex = 0;
        SetTarget();
    }

    private void Update()
    {
        CheckTargetReached();
    }

    private void CheckTargetReached()
    {
        if (Vector2.Distance(m_transform.position, target.position) < 0.04)
        {
            SetNextPatrol();
        }
    }

    public void SetClosestPatrol()
    {
        float distance = Mathf.Infinity;

        for (int i = 0; i < numberOfPoints; i++)
        {
            if (Vector2.Distance(m_transform.position, childTransforms[i].position) < distance)
            {
                distance = Vector2.Distance(m_transform.position, childTransforms[i].position);
                targetIndex = i;
            }
        }

        SetTarget();
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
