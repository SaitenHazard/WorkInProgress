using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float explodeAfter;
    public int damage;

    private void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explodeAfter);

        CircleCollider2D bombRingCollider = GetComponentsInChildren<CircleCollider2D>()[1];

        bombRingCollider.enabled = true;

        yield return new WaitForSeconds(0.01f);
    }

    public int GetDamage()
    {
        return damage;
    }
}
