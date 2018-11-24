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
    protected int startingIndex;
    protected int finishingIndex;

    virtual public void Initialize()
    {

    }

    virtual public void DoSpeech()
    {
        Debug.Log("index = " + index);
        Debug.Log("start = " + startingIndex);
        Debug.Log("finish = " + finishingIndex);

        index++;

        Debug.Log("index = " + index);
        Debug.Log("start = " + startingIndex);
        Debug.Log("finish = " + finishingIndex);

        if (index == startingIndex)
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

        if (index == finishingIndex)
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