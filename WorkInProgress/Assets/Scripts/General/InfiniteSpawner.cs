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
        int enemyIndex = Random.Range(0, enemies.Length);

        if (gameObject.name == "InfinitePowerSpawner")
            Debug.Log(enemyIndex);

        GameObject instantiateObejct 
            = Instantiate(enemies[enemyIndex], this.transform);

        instantiateObejct.transform.position = this.transform.position;

        yield return new WaitForSeconds(spawnTime);

        StartCoroutine(Spawn());
    }
}
