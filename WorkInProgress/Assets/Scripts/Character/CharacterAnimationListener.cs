using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationListener : MonoBehaviour {
    protected CharacterMovementModel characterMovementModel;

    private void Start()
    {
        characterMovementModel = gameObject.GetComponentInParent<CharacterMovementModel>();
    }
}
