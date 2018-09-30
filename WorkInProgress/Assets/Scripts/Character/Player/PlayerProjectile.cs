using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private CharacterMovementModel m_movementModel;
    private PlayerStats playerStats;

    public GameObject projectileObject;
    public GameObject projectile2Object;

    private void Awake()
    {
        m_movementModel = GetComponent<CharacterMovementModel>();
        playerStats = GetComponent<PlayerStats>();
    }

    public void DoPorjectile()
    {
        playerStats.DeductProjectiletNumber();

        Vector2 facingDirection = m_movementModel.GetFacingDirection();

        GameObject cloneObject = null;

        if (playerStats.IsDamageUp())
            cloneObject = Instantiate(projectile2Object);
        else
            cloneObject = Instantiate(projectileObject);

        cloneObject.transform.position = gameObject.transform.parent.position;

        if (facingDirection == new Vector2(0, 1))
            cloneObject.transform.position = new Vector2(transform.position.x, transform.position.y + 0.25f);
        else if (facingDirection == new Vector2(0, -1))
            cloneObject.transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
        else if (facingDirection == new Vector2(1, 0))
            cloneObject.transform.position = new Vector2(transform.position.x + 0.25f, transform.position.y);
        else
            cloneObject.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y);

        cloneObject.SetActive(true);
        cloneObject.GetComponent<Projectile>().SetDirectionTowardsPlayerFacing();
    }
}
