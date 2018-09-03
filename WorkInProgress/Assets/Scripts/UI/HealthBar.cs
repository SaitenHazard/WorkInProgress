﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject Player;
    public Attackable attackable;

    private RectTransform rectTransform;
    private float width;
    private int maxHP;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        width = rectTransform.rect.width;
        maxHP = attackable.GetHealth();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        int currentHP = attackable.GetHealth();

        float newWidth = (width / maxHP) * currentHP;

        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.rect.height);
    }
}
