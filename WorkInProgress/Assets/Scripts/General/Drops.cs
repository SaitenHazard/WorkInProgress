using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    private int numberOfDrops;
    private enumInventory drop;

    private void Start()
    {
        PickEnemy();
    }

    private void PickEnemy()
    {
        numberOfDrops = (int)enumInventory.NULL;
        int randomNum = Random.Range(0, numberOfDrops);

        drop = (enumInventory)randomNum;
    }

    private void OnDestroy()
    {
        GameObject loadObject = Resources.Load<GameObject>("Drops/" + drop);
        GameObject cloneObject = Instantiate(loadObject);
        cloneObject.transform.position = gameObject.transform.position;
    } 
}
