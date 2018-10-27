using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterOneAttackable : Attackable
{
    public GameObject spawnObject;

    override protected void Awake()
    {
        base.Awake();
    }

    protected override IEnumerator CharacterFadeOut()
    {
        float opacity = 1f;

        yield return new WaitForSeconds(pushBackTime);

        DoSpawns();

        while (opacity > 0.2f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.2f);
        }

        Destroy(gameObject.transform.parent.gameObject);
    }

    private void DoSpawns()
    {
        GameObject spawn1 = Instantiate(spawnObject);
        GameObject spawn2 = Instantiate(spawnObject);

        Vector2 facingDirection = m_movementModel.GetFacingDirection();

        if (facingDirection.x == 1 || facingDirection.x == -1)
        {
            spawn1.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            spawn2.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, 0);
        }
        else
        {
            spawn1.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, 0);
            spawn2.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, 0);
        }

        spawn1.GetComponent<SpawnManager>().Initialize();
        spawn2.GetComponent<SpawnManager>().Initialize();
    }
}
