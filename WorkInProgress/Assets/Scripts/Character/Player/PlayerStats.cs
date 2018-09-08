using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float yieldTime = 5;
    public int damage;

    private float speed;
    private bool projectile;

    private void Start()
    {
        speed = 1.5f;
        damage = 1;
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
