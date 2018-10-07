using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int damage;
    private int projectileNumbers;

    private bool projectileActive;
    private bool gameStateFrozen;
    private bool shockActive;

    private float speed;
    private float yieldTime;

    private InteractableBase m_interactableBase;

    private void Awake()
    {
        m_interactableBase = null;
    }

    private void Start()
    {
        speed = 1.5f;
        damage = 1;
        yieldTime = 5;
    }

    public void SetInteractableBase(InteractableBase interactableBase)
    {
        m_interactableBase = interactableBase;
    }

    public InteractableBase GetInteractableBase()
    {
        return m_interactableBase;
    }

    private void Update()
    {
        CheckActiveUI();
    }

    public bool GetGameState()
    {
        return gameStateFrozen;
    }

    private void CheckActiveUI()
    {
        if (MenuView.Instance.GetMenuActive() == true || SpeechTextUI.Instance.GetTextBoxActive())
            gameStateFrozen = true;
        else
            gameStateFrozen = false;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void DamageUp()
    {
        damage++;
        StartCoroutine(RevertDamageUp());
    }

    public void SpeedUp()
    {
        speed = 2.5f;
        StartCoroutine(RevertSpeedUp());
    }

    public bool IsDamageUp()
    {
        return damage == 2;
    }

    public bool IsSpeedUp()
    {
        return speed == 2;
    }

    public bool IsProjetileActive()
    {
        return projectileNumbers > 0;
    }

    public void SetProjectile()
    {
        projectileNumbers = 5;
    }

    public void DeductProjectiletNumber()
    {
        projectileNumbers--;
    }

    public void ShockUp()
    {
        shockActive = true;
        StartCoroutine(RevertShock());
    }

    public bool IsParalyzeUp()
    {
        return shockActive;
    }

    private IEnumerator RevertShock()
    {
        yield return new WaitForSeconds(yieldTime);
        shockActive = false;
    }

    private IEnumerator RevertSpeedUp()
    {
        yield return new WaitForSeconds(yieldTime);
        speed = 1.5f;
    }

    private IEnumerator RevertDamageUp()
    {
        yield return new WaitForSeconds(yieldTime);
        damage--;
    }
}
