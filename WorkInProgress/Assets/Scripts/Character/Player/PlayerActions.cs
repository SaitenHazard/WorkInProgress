using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
    Animator animator;
    CharacterMovementModel m_movementModel;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        m_movementModel = GetComponent<CharacterMovementModel>();
    }

    private void Update()
    {
        StartCoroutine(UpdateAttack());
    }

    public void OnActionPressed()
    {
        //m_movementModel.DoAction();
    }

    private IEnumerator UpdateAttack()
    {
        if (PlayerAttributes.instance.IsAttackState() == true)
        {
            PlayerAttributes.instance.SetAttackState(false);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.2f);
            PlayerAttributes.instance.setWalkStateFrozen(false);
        }
    }
}
