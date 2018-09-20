using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public Vector2 mustFaceDirection;

    private SpeechBubble speechBubble;
    private enumSpeechBubbles enumSpeechBubble;
    private PlayerStats playerStats;

    public void Awake()
    {
        playerStats = PlayerInstant.Instance.GetComponent<PlayerStats>();

        speechBubble = gameObject.transform.parent.
            GetComponentInChildren<SpeechBubble>();

        enumSpeechBubble = enumSpeechBubbles.Interactable;
    }

    virtual public void OnInteract()
    {
       
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "Player")
        {
            Vector2 facingDirection = 
                PlayerInstant.Instance.GetComponent<CharacterMovementModel>().
                GetFacingDirection();

            if(facingDirection == mustFaceDirection)
            {
                speechBubble.ShowSpeechBubble(enumSpeechBubble);
                playerStats.SetInteractableBase(this);
            }
            else
            {
                playerStats.SetInteractableBase(null);
                speechBubble.HideSpeechBubble();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            playerStats.SetInteractableBase(null);
            speechBubble.HideSpeechBubble();
        }
    }
}