using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationListener : CharacterAnimationListener
{
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
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
        PlayerAttributes.instance.setWalkStateFrozen(false);
    }
}
