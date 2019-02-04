using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private int projectileNumbers;
    private bool projectileActive;
    private bool gameStateFrozen;
    private bool stunActive;
    private bool rangeActive;
    private bool invincibleActive;
    private bool invisibleActive;
    private float yieldTime;
    private SpriteRenderer playerSpriteRenderer;
    private InteractableBase m_interactableBase;

    public GameObject punchVisuals;
    public GameObject playerSlime1;

    private void Awake()
    {
        m_interactableBase = null;
    }

    private void Start()
    {
        playerSpriteRenderer = PlayerInstant.Instance.GetComponentInChildren<SpriteRenderer>();

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
        CheckPowerUps();
    }

    public bool IsInvisibleUp()
    {
        return invisibleActive;
    }

    public bool IsInvincibleUp()
    {
        return invincibleActive;
    }

    public void InvisibleUp()
    {
        invisibleActive = true;
        StartCoroutine(RevertInvisible());
    }

    private IEnumerator RevertInvisible()
    {
        yield return new WaitForSeconds(yieldTime);
        invisibleActive = false;
    }

    public void InvincibleUp()
    {
        invincibleActive = true;
        StartCoroutine(RevertInvincible());
    }

    private IEnumerator RevertInvincible()
    {
        yield return new WaitForSeconds(yieldTime);
        invincibleActive = false;
    }

    private void CheckPowerUps()
    {
        if (IsRangeUp() == true)
        {
            punchVisuals.transform.localScale = new Vector3(2, 2, 1);
        }
        else
        {
            punchVisuals.transform.localScale = new Vector3(1, 1, 1);
        }

        if(invisibleActive == true)
        {
            playerSpriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            playerSpriteRenderer.color = new Color(1, 1, 1, 1f);
        }
    }

    public bool GetGameState()
    {
        return gameStateFrozen;
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

    public void StunUp()
    {
        stunActive = true;
        StartCoroutine(RevertShock());
    }

    public void RangeUp()
    {
        rangeActive = true;
        StartCoroutine(RevertRange());
    }

    public bool IsRangeUp()
    {
        return rangeActive;
    }

    private IEnumerator RevertRange()
    {
        yield return new WaitForSeconds(yieldTime);
        rangeActive = false;
    }

    public bool IsStunUp()
    {
        return stunActive;
    }

    private IEnumerator RevertShock()
    {
        yield return new WaitForSeconds(yieldTime);
        stunActive = false;
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
