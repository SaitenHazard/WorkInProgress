using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float yieldTime = 5;
    private int damage;

    private float speed;
    private bool projectile;
    private bool gameStateFrozen;

    private void Start()
    {
        speed = 1.5f;
        damage = 1;
    }

    private void Update()
    {
        CheckMenuActive();
    }

    public bool GetGameState()
    {
        return gameStateFrozen;
    }

    private void CheckMenuActive()
    {
        gameStateFrozen = MenuView.Instance.GetMenuActive();
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

    public bool IsDamageUp()
    {
        return damage == 2;
    }

    public void SetProjectile()
    {
        projectile = true;
        StartCoroutine(RevertProjectile());
    }

    private IEnumerator RevertProjectile()
    {
        yield return new WaitForSeconds(yieldTime);
        projectile = false;
    }

    private IEnumerator RevertDamageUp()
    {
        yield return new WaitForSeconds(yieldTime);
        damage--;
    }
}
