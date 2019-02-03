using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttackable : Attackable
{
    public GameObject SpawnObject;

    override protected void DoDestroy()
    {
        SpawnObject.SetActive(true);
        SpawnObject.transform.SetParent(null);
        base.DoDestroy();
    }
}
