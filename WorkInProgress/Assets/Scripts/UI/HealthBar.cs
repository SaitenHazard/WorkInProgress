using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private RectTransform rectTransform;
    private float width;

    public int maxHP;
    public int currentHP;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        width = rectTransform.rect.width;
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float newWidth = (width / maxHP) * currentHP;

        rectTransform.sizeDelta = 
            new Vector2(newWidth, rectTransform.rect.height);
    }
}
