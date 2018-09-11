using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public Vector2 mustFaceDirection;

    private SpeechBubble speechBubble;
    private enumSpeechBubbles enumSpeechBubble;

    private void Awake()
    {
        speechBubble = gameObject.transform.parent.
            GetComponentInChildren<SpeechBubble>();

        enumSpeechBubble = enumSpeechBubbles.Interactable;
    }

    virtual protected void OnInteract()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Player")
        {
            Vector2 facingDirection = 
                PlayerInstant.Instance.GetComponent<CharacterMovementModel>().
                GetFacingDirection();

            if(facingDirection == mustFaceDirection)
            {
                speechBubble.ShowSpeechBubble(enumSpeechBubble);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        speechBubble.HideSpeechBubble();
    }
}