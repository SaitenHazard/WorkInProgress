using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlime : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public float damage = 1;

    public void DoSlime()
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

        Projectile projectile = cloneObject.GetComponent<Projectile>();

        projectile.SetDirectionTowardsPlayerFacing();
        projectile.SetDamage(GetComponent<PlayerStats>().GetDamage());

        cloneObject.SetActive(true);
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public float GetDamage()
    {
        return damage;
    }

    private void Start()
    {
        StartCoroutine(SlimeFadeout());
    }

    private IEnumerator SlimeFadeout()
    {
        float opacity = 1f;

        while (opacity > 0.2f)
        {
            opacity -= 0.2f;
            spriteRenderer.color = new Color(1f, 1f, 1f, opacity);
            yield return new WaitForSeconds(0.2f);
        }

        Destroy(gameObject.transform.parent.gameObject);
    }
}
