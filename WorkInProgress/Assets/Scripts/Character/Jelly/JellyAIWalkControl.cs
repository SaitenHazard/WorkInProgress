using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyAIWalkControl : CharacterBaseControl
{
    public float steps;

    private void Start()
    {
        Debug.Log("in");
        StartCoroutine(WalKCycle());
    }

    private void Update()
    {
        //Debug.Log("in");
        //StartCoroutine("WalKCycle");
    }

    IEnumerator WalKCycle()
    {
        for (float time = Time.time; (Time.time - time) <= steps; )
        {
           UpdateDirection(Directions.Right);
           yield return null;
        }

        for (float time = Time.time; (Time.time - time) <= steps;)
        {
            UpdateDirection(Directions.Down);
            yield return null;
        }

        for (float time = Time.time; (Time.time - time) <= steps;)
        {
            UpdateDirection(Directions.Left);
            yield return null;
        }

        for (float time = Time.time; (Time.time - time) <= steps;)
        {
            UpdateDirection(Directions.Up);
            yield return null;
        }

        StartCoroutine(WalKCycle());
    }

    private void UpdateDirection(Directions dir)
    {
        Vector2 newDirection = Vector2.zero;

        if (dir == Directions.Up)
        {
            newDirection.y = 1;
        }

        if (dir == Directions.Down)
        {
            newDirection.y = -1;
        }

        if (dir == Directions.Left)
        {
            newDirection.x = -1;
        }

        if (dir == Directions.Right)
        {
            newDirection.x = 1;
        }

        SetDirection(newDirection);
    }

    void UpdateAction()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            //OnActionPressed();
        }
    }
}
