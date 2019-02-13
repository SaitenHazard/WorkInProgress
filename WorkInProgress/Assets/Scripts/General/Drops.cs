using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    private int numberOfDrops;
    private enumInventory drop;

	void Start ()
    {
        numberOfDrops = (int)enumInventory.NULL;
        int randomNum = Random.Range(0, numberOfDrops);
        drop = (enumInventory)randomNum;
    }

    private void OnDestroy()
    {
        GameObject loadObject = Resources.Load("Drops/" + drop.ToString()) as GameObject;

        GameObject cloneObject = Instantiate(loadObject);

        cloneObject.transform.position = this.transform.position;
    }
}
