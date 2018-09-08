using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Transform GetPlayerTransform()
    {
        return PlayerInstant.Instance.transform;
    }
}
