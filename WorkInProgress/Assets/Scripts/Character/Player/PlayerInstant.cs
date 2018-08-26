using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstant : MonoBehaviour
{
    public static PlayerInstant Instance;
    private GameObject Object;

    private void Awake()
    {
        Instance = this;
        Object = gameObject;
    }

    public GameObject GetPlayerObject()
    {
        return Object;
    }
}
