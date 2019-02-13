using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    private int numberOfEnemies;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        //GameObject loadObject = Resources.Load("Drops/" + drop.ToString()) as GameObject;

        GameObject cloneObject = Instantiate(loadObject);

        cloneObject.transform.position = this.transform.position;

        yield return new WaitForSeconds(160f);
    }
}
