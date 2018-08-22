using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationListener : CharacterAnimationListener
{
    public SpriteRenderer spriteRenderer;
    public CharacterMovementModel m_movementModel;

    private void Start()
    {
        m_movementModel = GetComponentInParent<CharacterMovementModel>();
    }

    private void flipSortingOrder(int priority)
    {
        if (priority == 1)
            spriteRenderer.sortingOrder = 200;
        else
            spriteRenderer.sortingOrder = 0;
    }

    private void attackFinished()
    {
        m_movementModel.SetMovementFrozen(false);
    }
}
