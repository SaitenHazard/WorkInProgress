using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhambaSight : SightBase
{
    public GameObject projectileObject;

    private void Update()
    {
        base.Update();
    }

    virtual protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            base.OnTriggerEnter2D(collider2D);
            StartCoroutine(ProjectileInstantiate());
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            base.OnTriggerExit2D(collider2D);
            StopAllCoroutines();
        }
    }

    private CharacterMovementModel p_movementModel;
    private CharacterMovementView c_MovementView;
    private Vector2 facingDirection;

    override protected void Awake()
    {
        p_movementModel = PlayerInstant.Instance.GetComponent<CharacterMovementModel>();
        c_MovementView = GetComponentInParent<CharacterMovementView>();
        base.Awake();
    }

    private IEnumerator ProjectileInstantiate()
    {
        yield return new WaitForSeconds(2);

        c_MovementView.DoAttack();
        facingDirection = p_movementModel.GetFacingDirection();

        GameObject cloneObject = Instantiate(projectileObject);
        cloneObject.transform.SetParent(gameObject.transform.parent);

        Transform transform = cloneObject.transform;

        if (facingDirection == new Vector2(0, 1))
            transform.localPosition = new Vector2(0, 0.25f);
        else if (facingDirection == new Vector2(0, -1))
            transform.localPosition = new Vector2(0, -0.25f);
        else if (facingDirection == new Vector2(1, 0))
            transform.localPosition = new Vector2(0.25f, 0);
        else
            transform.localPosition = new Vector2(-0.25f, 0);

        cloneObject.SetActive(true);
        yield return new WaitForSeconds(3);

        StartCoroutine(ProjectileInstantiate());
    }
}
