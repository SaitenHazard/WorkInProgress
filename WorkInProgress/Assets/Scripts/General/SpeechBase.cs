using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBase : MonoBehaviour
{
    public static int indexSize;

    public string[] speech = new string[indexSize];
    public Vector2[] faceDirection = new Vector2[indexSize];
    public GameObject[] Object = new GameObject[indexSize];
}