using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject Player;

    private RectTransform rectTransform;
    private float width;
    private Attackable attackable;
    private int maxHP;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        width = rectTransform.rect.width;
        attackable = Player.GetComponentInChildren<Attackable>();
        maxHP = attackable.GetHealht();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        int currentHP = attackable.GetHealht();

        float newWidth = (width / maxHP) * currentHP;

        rectTransform.sizeDelta = 
            new Vector2(newWidth, rectTransform.rect.height);
    }
}
