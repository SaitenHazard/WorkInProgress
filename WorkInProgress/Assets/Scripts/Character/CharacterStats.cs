using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private void Start()
    {
    }

    public void DoParalyzeView(float paralyzeTime)
    {
        StartCoroutine(ReverseParalyze(paralyzeTime));
    }

    private IEnumerator ReverseParalyze(float paralyzeTime)
    {
        yield return new WaitForSeconds(paralyzeTime);
    }
}
