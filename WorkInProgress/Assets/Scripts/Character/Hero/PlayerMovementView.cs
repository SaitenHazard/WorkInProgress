using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementView : CharacterMovementView
{
    override protected void Update()
    {
        base.Update();
        StartCoroutine(UpdateAttack());
    }

    private void FixedUpdate()
    {
        //UpdateAttack();
    }

    private IEnumerator UpdateAttack()
    {
        if (PlayerAttributes.instance.IsAttackState() == true)
        {
            PlayerAttributes.instance.SetAttackState(false);
            animator.SetTrigger("Attack");
            //Debug.Log(PlayerAttributes.instance.IsWalkFrozen());
            //yield return null;
            yield return new WaitForSeconds(0.2f);
            PlayerAttributes.instance.setWalkStateFrozen(false);
            //yield return new WaitForSeconds(1.0f);
        }
    }
}
