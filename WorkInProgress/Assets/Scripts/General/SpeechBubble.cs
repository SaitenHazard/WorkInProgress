using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private enumSpeechBubbles m_bubble;

    public Sprite questionBubble;
    public Sprite exclamatoinBubble;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SelectBubble()
    {
        if (m_bubble == enumSpeechBubbles.Question)
            spriteRenderer.sprite = questionBubble;

        else if (m_bubble == enumSpeechBubbles.Exclamation)
            spriteRenderer.sprite = questionBubble;
    }

    public void PopSpeechBubble(enumSpeechBubbles bubble)
    {
        Debug.Log("on");
        m_bubble = bubble;
        SelectBubble();
        StartCoroutine(Pop());
    }

    public void PopSpeechBubble(enumSpeechBubbles bubble, float time)
    {
        m_bubble = bubble;
        SelectBubble();
        StartCoroutine(Pop(time));
    }

    private IEnumerator Pop()
    {
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.35f);
        spriteRenderer.enabled = false;
    }

    private IEnumerator Pop(float time)
    {
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(time);
        spriteRenderer.enabled = false;
    }
}
