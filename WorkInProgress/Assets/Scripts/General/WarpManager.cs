﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpManager : MonoBehaviour {

    public static WarpManager Instance;

	private void Awake ()
    {
        Instance = this;
    }
	
	public void Warp(string warpScene, string warpDestination, Vector2 faceDirection)
    {
        CharacterMovementModel movementModel = PlayerInstant.Instance.GetComponent<CharacterMovementModel>();
        movementModel.SetDirection(faceDirection);
        movementModel.SetMovementFrozen(false);

        SceneManager.LoadScene(warpScene, LoadSceneMode.Single);

        Transform destinationTransform = transform.Find(warpDestination);

        PlayerInstant.Instance.transform.position = 
            new Vector2(destinationTransform.position.x, destinationTransform.position.y);

        StartCoroutine(Fade.Instance.FadeIn());
    }
}
