using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteSpawner : MonoBehaviour
{
    public GameObject [] enemies;
    public float spawnTime;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        int enemyIndex = Random.Range(0, enemies.Length - 1);
        Debug.Log(enemyIndex);

        GameObject instantiateObejct 
            = Instantiate(enemies[enemyIndex], this.transform);

        instantiateObejct.transform.position = this.transform.position;

        Debug.Log(instantiateObejct.transform);
        Debug.Log(instantiateObejct.transform.parent.name);

        yield return new WaitForSeconds(spawnTime);

        StartCoroutine(Spawn());
    }
}
