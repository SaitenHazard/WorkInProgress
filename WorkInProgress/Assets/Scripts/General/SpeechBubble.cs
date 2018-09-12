using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private enumSpeechBubbles m_bubble;
    
    public Sprite exclamatoinBubble;
    public Sprite questionBubble;
    public Sprite interactableBubble;
    public Sprite empty;

    private float defaultPopTime = 0.25f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SelectBubble()
    {
        if (m_bubble == enumSpeechBubbles.Question)
            spriteRenderer.sprite = questionBubble;

        else if (m_bubble == enumSpeechBubbles.Exclamation)
            spriteRenderer.sprite = exclamatoinBubble;

        else if (m_bubble == enumSpeechBubbles.Interactable)
            spriteRenderer.sprite = interactableBubble;

        else if (m_bubble == enumSpeechBubbles.Interactable)
            spriteRenderer.sprite = empty;
    }

    public void PopSpeechBubble(enumSpeechBubbles bubble)
    {
        m_bubble = bubble;
        SelectBubble();
        Pop();
    }

    public void PopSpeechBubble(enumSpeechBubbles bubble, float time)
    {
        m_bubble = bubble;
        SelectBubble();
        StartCoroutine(Pop(time));
    }

    public void ShowSpeechBubble(enumSpeechBubbles bubble)
    {
        m_bubble = bubble;
        SelectBubble();
        spriteRenderer.enabled = true;
    }

    public void HideSpeechBubble()
    {
        spriteRenderer.enabled = false;
    }

    private void Pop()
    {
        StartCoroutine(Pop(defaultPopTime));
    }

    private IEnumerator Pop(float time)
    {
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(time);
        spriteRenderer.enabled = false;
    }
}
