using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    private int numberOfDrops;
    private enumEnemies drop;

    private void Start()
    {
        PickEnemy();
        StartCoroutine(Spawn());
    }

    private void PickEnemy()
    {
        numberOfDrops = (int)enumEnemies.NULL;
        int randomNum = Random.Range(0, numberOfDrops);
        drop = (enumEnemies)randomNum;
    }

    private IEnumerator Spawn()
    {
        GameObject loadObject = Resources.Load("Drops/" + drop) as GameObject;

        GameObject cloneObject = Instantiate(loadObject);

        cloneObject.transform.position = this.transform.position;

        yield return new WaitForSeconds(160f);
    }
}
