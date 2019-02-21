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

        randomNum = 5;

        enemy = (enumEnemies)randomNum;

        //Debug.Log(randomNum);
        //Debug.Log(enemy);
    }

    private IEnumerator Spawn()
    {
        //Debug.Log(Resources.Load<GameObject>("Enemies/" + enemy));

        GameObject loadObject = Resources.Load<GameObject>("Enemies/" + enemy);

        GameObject cloneObject = Instantiate(loadObject);

        cloneObject.transform.position = this.transform.position;

        yield return new WaitForSeconds(160f);
    }
}
