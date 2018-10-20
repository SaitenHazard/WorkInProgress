using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Player;
    public Attackable attackable;

    public Sprite spriteHealthBar;
    public Sprite spriteInvincibleBar;

    private Image image;
    private RectTransform rectTransform;
    private float width;
    private float maxHP;
    private PlayerStats playerStats;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        width = rectTransform.rect.width;
        maxHP = attackable.GetHealth();
        image = GetComponent<Image>();

        playerStats = PlayerInstant.Instance.GetComponent<PlayerStats>();

        Debug.Log(transform.parent.name + " : " + image);
    }

    private void Update()
    {
        UpdateHealthBar();
        UpdateBarSprite();
    }

    private void UpdateBarSprite()
    {
        if(playerStats.IsInvincibleUp() == true)
        {
            image.sprite = spriteInvincibleBar;
        }
        else
        {
            image.sprite = spriteHealthBar;
        }
    }

    private void UpdateHealthBar()
    {
        float currentHP = attackable.GetHealth();

        float newWidth = (width / maxHP) * currentHP;

        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.rect.height);
    }
}
