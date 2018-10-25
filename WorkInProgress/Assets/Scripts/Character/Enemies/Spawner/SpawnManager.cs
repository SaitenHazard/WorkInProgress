﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject HPGameObject;

    private SpriteRenderer spriteRenderer;

    private AIBase parentSpawnerAI;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialize(AIBase spawnerAI)
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeIn());
        parentSpawnerAI = spawnerAI;
    }

    public AIBase GetSpanwerAI()
    {
        return parentSpawnerAI;
    }

    private IEnumerator FadeIn ()
    {
        float opacity = 0f;

        while (opacity < 1f)
        {
            opacity += 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.2f);
        }

        ActivateAllScriptsAndHealthBar();
    }

    private void ActivateAllScriptsAndHealthBar()
    {
        GetComponentInChildren<Attackable>().enabled = true;
        GetComponentInChildren<AIBase>().enabled = true;
        HPGameObject.SetActive(true);
    }
}
