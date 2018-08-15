using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementModel : CharacterMovementModel
{
    private bool attackFlag;

    private void Awake()
    {
        base.Awake();

        attackFlag = false;
    }

    public override void AttactAnimationListener()
    {
        SetAttackFlag(false);
    }

    override public void DoAction()
    {
        InteractableBase interactableInProximity = FindInteractableInProximity();

        if (interactableInProximity == null)
        {
            SetAttackFlag(true);
            return;
        }

        interactableInProximity.OnInteract();
    }

    override public void SetAttackFlag(bool set)
    {
        attackFlag = set;
        stopMovement = set;
    }

    override public bool GetAttackFlag()
    {
        return attackFlag;
    }

    private InteractableBase FindInteractableInProximity()
    {
        Collider2D[] closeColliders = GetCloseColliders();

        InteractableBase closestInteractable = null;
        float angleToClosestInteractble = Mathf.Infinity;

        for (int i = 0; i < closeColliders.Length; ++i)
        {
            InteractableBase colliderInteractable = closeColliders[i].GetComponent<InteractableBase>();

            if (colliderInteractable == null)
            {
                continue;
            }

            Vector3 directionToInteractble = closeColliders[i].transform.position - transform.position;

            float angleToInteractable = Vector3.Angle(GetFacingDirection(), directionToInteractble);

            if (angleToInteractable < 40)
            {
                if (angleToInteractable < angleToClosestInteractble)
                {
                    closestInteractable = colliderInteractable;
                    angleToClosestInteractble = angleToInteractable;
                }
            }
        }

        return closestInteractable;
    }

    private Collider2D[] GetCloseColliders()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        return Physics2D.OverlapAreaAll(
                     (Vector2)transform.position + boxCollider.offset + boxCollider.size * 0.6f,
                     (Vector2)transform.position + boxCollider.offset - boxCollider.size * 0.6f
                );
    }
}
