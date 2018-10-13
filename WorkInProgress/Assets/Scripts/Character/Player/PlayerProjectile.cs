using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private CharacterMovementModel m_movementModel;
    private PlayerStats playerStats;
    private Vector2 projectileFacingDirection;
    private GameObject cloneObject = null;

    public GameObject projectileObject;
    public GameObject projectile2Object;

    private void Awake()
    {
        m_movementModel = GetComponent<CharacterMovementModel>();
        playerStats = GetComponent<PlayerStats>();
    }

    public void DoRangeProjectile()
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
        {
            cloneObject.transform.position = new Vector2(transform.position.x, transform.position.y + 0.25f);
            cloneObject.transform.position = new Vector2(transform.position.x + 0.25f, transform.position.y + 0.25f);
            cloneObject.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y + 0.25f);
        }

        else if (facingDirection == new Vector2(0, -1))
            cloneObject.transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
        else if (facingDirection == new Vector2(1, 0))
            cloneObject.transform.position = new Vector2(transform.position.x + 0.25f, transform.position.y);
        else
            cloneObject.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y);

        Projectile projectile = cloneObject.GetComponent<Projectile>();

        projectile.SetDirectionTowardsPlayerFacing();
        cloneObject.SetActive(true);
    }

    public void DoPorjectile(bool rangeUp)
    {
        playerStats.DeductProjectiletNumber();

        projectileFacingDirection = m_movementModel.GetFacingDirection();

        cloneObject = null;

        if (playerStats.IsDamageUp())
            cloneObject = Instantiate(projectile2Object);
        else
            cloneObject = Instantiate(projectileObject);

        cloneObject.transform.position = gameObject.transform.parent.position;

        CreateProjectile(rangeUp);
    }

    private void CreateProjectile(bool rangeUp)
    {
        if (projectileFacingDirection == new Vector2(0, 1))
            cloneObject.transform.position = new Vector2(transform.position.x, transform.position.y + 0.25f);
        else if (projectileFacingDirection == new Vector2(0, -1))
            cloneObject.transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);
        else if (projectileFacingDirection == new Vector2(1, 0))
            cloneObject.transform.position = new Vector2(transform.position.x + 0.25f, transform.position.y);
        else
            cloneObject.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y);

        Projectile projectile = cloneObject.GetComponent<Projectile>();

        projectile.SetDirectionTowardsPlayerFacing();
        cloneObject.SetActive(true);

        if (rangeUp == true)
            CreateRangeProjectile();
    }

    private void CreateRangeProjectile()
    {
        GameObject cloneObject1 = Instantiate(cloneObject);
        GameObject cloneObject2 = Instantiate(cloneObject);

        cloneObject1.transform.position = gameObject.transform.parent.position;
        cloneObject2.transform.position = gameObject.transform.parent.position;

        Projectile projectile1 = cloneObject1.GetComponent<Projectile>();
        Projectile projectile2 = cloneObject2.GetComponent<Projectile>();

        if (projectileFacingDirection == new Vector2(0, 1))
        {
            cloneObject1.transform.position = new Vector2(transform.position.x + 0.25f, transform.position.y + 0.25f);
            cloneObject2.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y + 0.25f);
            projectile1.SetDirectionTopRight();
            projectile2.SetDirectionTopLeft();
        }

        else if (projectileFacingDirection == new Vector2(0, -1))
        {
            cloneObject1.transform.position = new Vector2(transform.position.x + 0.25f, transform.position.y - 0.25f);
            cloneObject2.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y - 0.25f);
            projectile1.SetDirectionBottomRight();
            projectile2.SetDirectionBottomLeft();
        }

        else if (projectileFacingDirection == new Vector2(1, 0))
        {
            cloneObject1.transform.position = new Vector2(transform.position.x + 0.25f, transform.position.y + 0.25f);
            cloneObject2.transform.position = new Vector2(transform.position.x + 0.25f, transform.position.y - 0.25f);
            projectile1.SetDirectionTopRight();
            projectile2.SetDirectionBottomRight();
        }

        else
        {
            cloneObject1.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y + 0.25f);
            cloneObject2.transform.position = new Vector2(transform.position.x - 0.25f, transform.position.y - 0.25f);
            projectile1.SetDirectionTopLeft();
            projectile2.SetDirectionBottomLeft();
        }
    }
}
