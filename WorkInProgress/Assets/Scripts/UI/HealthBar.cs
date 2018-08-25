using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private RectTransform rectTransform;
    private float maxWidth;
    private float currentWidth;

    public int maxHP;
    public int currentHP;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        maxWidth = rectTransform.rect.width;
        currentWidth = maxWidth;
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float newWidth = (maxWidth / maxHP) * currentHP;

        rectTransform.sizeDelta = 
            new Vector2(newWidth, rectTransform.rect.height);
    }
}
