using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public GameObject DontDestory;

    public string warpScene;
    public string warpDestination;
    public Vector2 faceDirection;

    private void Awake()
    {
        //DontDestroyOnLoad(DontDestory);

        //WarpManager.Instance.
        //    Warp(warpScene, warpDestination, faceDirection);
    }
}
