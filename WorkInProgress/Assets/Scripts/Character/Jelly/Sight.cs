using UnityEngine;
using System.Collections;

public class Sight : MonoBehaviour
{
    private SpeechBubble speechBubble;

    private void Awake()
    {
        speechBubble = 
            transform.parent.GetComponentInChildren<SpeechBubble>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.name == "Player")
        {
            SpeechBubble();
        }
    }

    private void SpeechBubble()
    {
        speechBubble.PopSpeechBubble(enumSpeechBubbles.Question);
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name == "Player")
        {
            float angleBetween =
                Mathf.Atan2(transform.position.y - collider2D.transform.position.y, transform.position.x - collider2D.transform.position.x) * 180 / Mathf.PI;
            Debug.Log(angleBetween);
        }
    }


}
