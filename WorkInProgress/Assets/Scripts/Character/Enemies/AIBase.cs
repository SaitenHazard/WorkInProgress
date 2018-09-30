using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour
{
    public enumEnemyActions enemyActions = enumEnemyActions.patrol;

    protected float angle;
    protected Transform target;
    protected SpeechBubble speechBubble;
    protected CharacterMovementModel m_movementModel;
    protected Vector2 movementDirection;
    protected Coroutine projectileCoroutine;

    private CharacterMovementModel p_movementModel;
    private CharacterMovementView movementView;

    public GameObject projectileObject;
    public Patrol patrol;

    virtual protected void Awake()
    {
        speechBubble =
            transform.parent.GetComponentInChildren<SpeechBubble>();

        p_movementModel = PlayerInstant.Instance.GetComponent<CharacterMovementModel>();
        m_movementModel = GetComponentInParent<CharacterMovementModel>();
        movementView = GetComponentInParent<CharacterMovementView>();
    }

    public GameObject GetPatrolObject()
    {
        if (patrol == null)
            return null;

        return patrol.gameObject;
    }

    protected void Update()
    {
        UpdateAngle();
        SetDirectionTowardsTarget();

        m_movementModel.SetDirection(movementDirection);
    }

    private void UpdateAngle()
    {
        if(enemyActions == enumEnemyActions.NULL)
        {
            SetNullDirection();
            return;
        }
        else if (enemyActions == enumEnemyActions.patrol)
        {
            target = patrol.GetTarget();
        }
        else if (enemyActions == enumEnemyActions.chase)
        {
            target = PlayerInstant.Instance.GetComponent<Transform>();
        }

        angle = Mathf.Atan2(transform.position.y - target.position.y,
            transform.position.x - target.position.x) * 180 / Mathf.PI * -1;
    }

    protected IEnumerator ProjectileInstantiate()
    {
        yield return new WaitForSeconds(2);

        Vector2 facingDirection = m_movementModel.GetFacingDirection();

        GameObject cloneObject = Instantiate(projectileObject);

        cloneObject.transform.position = gameObject.transform.parent.position;

        if (facingDirection == new Vector2(0, 1))
            cloneObject.transform.position = 
                new Vector2(transform.position.x, transform.position.y + 0.25f);
        else if (facingDirection == new Vector2(0, -1))
            cloneObject.transform.position =
                new Vector2(transform.position.x, transform.position.y - 0.25f);
        else if (facingDirection == new Vector2(1, 0))
            cloneObject.transform.position =
                new Vector2(transform.position.x + 0.25f, transform.position.y);
        else
            cloneObject.transform.position = 
                new Vector2(transform.position.x - 0.25f, transform.position.y);

        cloneObject.SetActive(true);
        cloneObject.GetComponent<Projectile>().SetDirectionTowardsPlayer();
        yield return new WaitForSeconds(3);

        StartCoroutine(ProjectileInstantiate());
    }

    virtual protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            speechBubble.PopSpeechBubble(enumSpeechBubbles.Exclamation);
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
        }
    }

    protected void SetNullDirection()
    {
        movementDirection = Vector2.zero;
    }

    protected void SetDirectionTowardsTarget()
    {
        if (angle >= 22.5 && angle <= 67.5)
            movementDirection = new Vector2(-1, 1);

        if (angle >= 112.5 && angle <= 157.5)
            movementDirection = new Vector2(1, 1);

        if (angle <= -22.5 && angle >= -67.5)
            movementDirection = new Vector2(-1, -1);

        if (angle <= -112.5 && angle >= -157.5)
            movementDirection = new Vector2(1, -1);

        if (angle >= 67.5 && angle <= 112.5)
            movementDirection = new Vector2(0, 1);

        if (angle <= -67.5 && angle >= -112.5)
            movementDirection = new Vector2(0, -1);

        if (angle <= 22.5 && angle >= 0  || angle >= -22.5 && angle <=0)
            movementDirection = new Vector2(-1, 0);

        if (angle >= 157.5 && angle <= 180 || angle <= -157.5 && angle >= -180)
            movementDirection = new Vector2(1, 0);
    }
}