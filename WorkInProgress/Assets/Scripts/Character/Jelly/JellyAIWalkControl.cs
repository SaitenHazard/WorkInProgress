using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyAIWalkControl : CharacterBaseControl
{
    Vector2 newDirection;

    private void Start()
    {
        StartCoroutine(WalkCycle());
    }

    private void Update()
    {
        UpdateDirection();
    }

    private IEnumerator WalkCycle()
    {
        newDirection = new Vector2(1, 0);
        yield return new WaitForSeconds(2);

        newDirection = new Vector2(0, -1);
        yield return new WaitForSeconds(2);

        newDirection = new Vector2(-1, 0);
        yield return new WaitForSeconds(2);

        newDirection = new Vector2(0, 1);
        yield return new WaitForSeconds(2);

        StartCoroutine(WalkCycle());
    }

    private void UpdateDirection()
    {
        SetDirection(newDirection);
    }
}
