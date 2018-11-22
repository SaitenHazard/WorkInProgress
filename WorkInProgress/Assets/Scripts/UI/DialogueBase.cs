using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBase : MonoBehaviour
{
    public string[] speech;
    public bool[] optionsIndexes;
    public string[] options1;
    public string[] options2;

    protected int index;

    public void Initialize()
    {
        index = -1;
    }

    virtual public void DoSpeech()
    {
        index++;

        if (index == 0)
        {
            SetInteractionStates(true);
        }

        if(index < speech.Length)
        {
            if (optionsIndexes[index] == true)
            {
                DialogueTextUI.Instance.ActivateDialogueOptionsBox(true, options1[index], options2[index]);
            }
            else
            {
                DialogueTextUI.Instance.ActivateDialogueOptionsBox(false);
            }
        }
        else
        {
            DialogueTextUI.Instance.ActivateDialogueOptionsBox(false);
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