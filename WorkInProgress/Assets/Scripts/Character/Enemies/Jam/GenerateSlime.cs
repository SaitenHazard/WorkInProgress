using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSlime : MonoBehaviour
{
    private Slime slime;

    private void Awake()
    {
        slime = GetComponentInParent<Slime>();
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        GameObject cloneObject = Instantiate(slime.gameObject);
        cloneObject.transform.position = transform.position;
        cloneObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Generate());
    }
}
