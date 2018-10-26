using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterOneAttackable : Attackable
{
    private GameObject spawnObject;

    override protected void Awake()
    {
        base.Awake();
        spawnObject = GetComponentInChildren<SpawnManager>().gameObject;
    }

    override protected void DoDestroy()
    {
        base.DoDestroy();
    }

    private void DoSpawns()
    {
        GameObject spawn1 = Instantiate(spawnObject);
        GameObject spawn2 = Instantiate(spawnObject);

        Vector2 facingDirection = m_movementModel.GetFacingDirection();

        if (facingDirection.x == 1 || facingDirection.x == -1)
        {
            spawn1.transform.position = new Vector3(transform.position.x + 2.5f, transform.position.y, 0);
            spawn2.transform.position = new Vector3(transform.position.x - 2.5f, transform.position.y, 0);
        }
        else
        {
            spawn1.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, 0);
            spawn2.transform.position = new Vector3(transform.position.x, transform.position.y - 2.5f, 0);
        }

        spawn1.GetComponent<SpawnManager>().Initialize();
        spawn2.GetComponent<SpawnManager>().Initialize();
    }
}
