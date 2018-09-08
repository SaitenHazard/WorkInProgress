using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSlime : MonoBehaviour
{
    private Slime slime;

    private void Awake()
    {
        slime = GetComponentInChildren<Slime>();
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        GameObject cloneObject = Instantiate(slime.gameObject);
        cloneObject.transform.position = transform.position;
        cloneObject.GetComponent<SpriteRenderer>().enabled = true;
        cloneObject.GetComponent<Slime>().enabled = true;
        cloneObject.GetComponent<Collider2D>().enabled = true;

        yield return new WaitForSeconds(0.15f);
        StartCoroutine(Generate());
    }
}
