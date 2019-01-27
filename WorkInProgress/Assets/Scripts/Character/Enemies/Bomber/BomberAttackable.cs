using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAttackable : Attackable
{
    public GameObject Bomb;

    override protected void DoDestroy()
    {
        Bomb.SetActive(true);
        Bomb.transform.SetParent(null);
        base.DoDestroy();
    }
}
