using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAttackable : Attackable
{
    public GameObject bombRing;

    override protected void DoDestroy()
    {
        base.DoDestroy();

        bombRing.GetComponent<SpriteRenderer>().enabled = true;
    }

    override protected IEnumerator CharacterFadeOut()
    {

        float opacity = 1f;

        yield return new WaitForSeconds(pushBackTime);


        while (opacity > 0.2f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.2f);
        }

        bombRing.GetComponent<CircleCollider2D>().enabled = true;

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject.transform.parent.gameObject);
    }
}
