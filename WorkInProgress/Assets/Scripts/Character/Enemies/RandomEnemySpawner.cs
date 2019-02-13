using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    private int numberOfEnemies;
    private enumEnemies enemy;

    private void Start()
    {
        PickEnemy();
        StartCoroutine(Spawn());
    }

    private void PickEnemy()
    {
        numberOfEnemies = (int)enumEnemies.NULL;
        int randomNum = Random.Range(0, numberOfEnemies);
        enemy = (enumEnemies)randomNum;
    }

    private IEnumerator Spawn()
    {
        GameObject loadObject = Resources.Load("Enemies/" + enemy) as GameObject;

        GameObject cloneObject = Instantiate(loadObject);

        cloneObject.transform.position = this.transform.position;

        yield return new WaitForSeconds(160f);
    }
}
