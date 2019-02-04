using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    private Attackable attackable;

    private float width;
    private float maxHP;

    private void Awake()
    {
        attackable = transform.parent.parent.GetComponentInChildren<Attackable>();

        width = transform.localScale.x;
    }

    private void Start()
    {
        maxHP = attackable.GetHealth();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float currentHP = attackable.GetHealth();

        float newWidth = (width / maxHP) * currentHP;

        transform.localScale = new Vector2(newWidth, transform.localScale.y);
    }
}
