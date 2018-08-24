using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstant : MonoBehaviour
{
    public static PlayerInstant Instance;

    private void Awake()
    {
        Instance = this;
    }
}
