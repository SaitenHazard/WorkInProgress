using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhambaSight : SightBase
{
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

    private IEnumerator ProjectileInstantiate()
    {
        GameObject projectileObject = GetComponent<Projectile>().gameObject;
        GameObject cloneObject = Instantiate(projectileObject);

        Transform transform = cloneObject.transform;

        CharacterMovementModel movementModel = GetComponentInParent<CharacterMovementModel>();
        Vector2 facingDirection = movementModel.GetFacingDirection();

        if (facingDirection == new Vector2(0, 1))
            transform.position = new Vector2(0, 0.25f);
        else if (facingDirection == new Vector2(0, -1))
            transform.position = new Vector2(0, -0.25f);
        else if (facingDirection == new Vector2(1, 0))
            transform.position = new Vector2(0.25f, 0);
        else
            transform.position = new Vector2(-0.25f, 0);

        cloneObject.SetActive(true);

        yield return new WaitForSeconds(7);

        ProjectileInstantiate();
    }
}
