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
        StartCoroutine(Spawn());
    }

    private void PickEnemy()
    {
        numberOfDrops = (int)enumInventory.NULL;
        int randomNum = Random.Range(0, numberOfDrops);

        randomNum = 0;

        drop = (enumInventory)randomNum;

        Debug.Log(randomNum);
        Debug.Log(drop);
    }

    private IEnumerator Spawn()
    {
        Debug.Log(Resources.Load<GameObject>("Drops/" + drop));

        GameObject loadObject = Resources.Load<GameObject>("Drops/" + drop);

        GameObject cloneObject = Instantiate(loadObject);

        cloneObject.transform.position = this.transform.position;

        yield return new WaitForSeconds(160f);
    }
}
