using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float explodeAfter;

    private void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explodeAfter);

        CircleCollider2D bombRingCollider = GetComponentsInChildren<CircleCollider2D>()[1];

        bombRingCollider.enabled = true;

        Debug.Log(bombRingCollider);

        yield return new WaitForSeconds(1f);

        //GetComponent<DoDestroyAnim>().DoDestroy();
    }
}
