using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBase : MonoBehaviour
{
    public string[] speech;
    public string[] speaker;
    public Vector2[] faceDirection;
    public enumSpeechBubbles[] speechBubbleEnum;

    private int index;

    public void Initialize()
    {
        index = -1;
    }

    public void DoSpeech()
    {
        index++;

        if (index == 0)
        {
            SetInteractionStates(true);
        }

        if (index == speech.Length)
        {
            SetInteractionStates(false);
            return;
        }

        DialogueTextUI.Instance.SetString(speech[index]);
    }

    private void SetInteractionStates(bool activate)
    {
        CharacterMovementModel m_movementModel = GetComponentInParent<CharacterMovementModel>();
        CharacterMovementModel p_movementModel = PlayerInstant.Instance.GetComponent<CharacterMovementModel>();
        NPCAIBase npcAIBase = gameObject.transform.parent.GetComponentInChildren<NPCAIBase>();

        DialogueTextUI.Instance.ActivateDialogueBox(activate);

        if (activate == true)
        {
            if (npcAIBase != null)
            {
                npcAIBase.SetEnemyAction(enumNPCActions.face);
            }
        }
        else
        {
            if (npcAIBase != null)
            {
                npcAIBase.SetEnemyAction(enumNPCActions.patrol);
            }
        }

        p_movementModel.SetMovementFrozen(activate);
    }
}