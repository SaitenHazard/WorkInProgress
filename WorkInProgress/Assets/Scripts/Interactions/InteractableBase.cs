using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    private SpeechBubble speechBubble;
    private enumSpeechBubbles enumSpeechBubble;
    private PlayerStats playerStats;

    public void Awake()
    {
        speechBubble = gameObject.transform.parent.GetComponentInChildren<SpeechBubble>();
        enumSpeechBubble = enumSpeechBubbles.Interactable;
    }

    public void Start()
    {
        playerStats = PlayerInstant.Instance.GetComponent<PlayerStats>();
    }

    virtual public void OnInteract()
    {
       
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.tag == "PlayerInteraction")
        {
            speechBubble.ShowSpeechBubble(enumSpeechBubble);
            playerStats.SetInteractableBase(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "PlayerInteraction")
        {
            playerStats.SetInteractableBase(null);
            speechBubble.HideSpeechBubble();
        }
    }
}