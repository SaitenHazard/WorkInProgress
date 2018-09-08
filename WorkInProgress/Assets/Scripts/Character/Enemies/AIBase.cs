using UnityEngine;
using System.Collections;

public class AIBase : MonoBehaviour
{
    private CharacterMovementModel m_movementModel;
    private SpeechBubble speechBubble;
    private Vector2 movementDirection;
    private Patrol patrol;
    private Transform target;

    public enumEnemyActions enemyActions = enumEnemyActions.patrol;

    protected float angle;

    virtual protected void Awake()
    {
        speechBubble =
            transform.parent.GetComponentInChildren<SpeechBubble>();

        m_movementModel = GetComponentInParent<CharacterMovementModel>();

        patrol = GetComponentInChildren<Patrol>();
    }

    virtual protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            speechBubble.PopSpeechBubble(enumSpeechBubbles.Question);
            enemyActions = enumEnemyActions.chase;
            target = PlayerInstant.Instance.GetComponent<Transform>();
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            SetNullDirection();
            enemyActions = enumEnemyActions.chase;
            patrol.SetClosestPatrol();
        }
    }

    protected void Update()
    {
        UpdateAngle();
        SetDirectionTowardsTarget();

        m_movementModel.SetDirection(movementDirection);
    }

    private void UpdateAngle()
    {
        if(enemyActions == enumEnemyActions.patrol)
        {
            target = patrol.GetTarget();
        }
        else if(enemyActions == enumEnemyActions.patrol)
        {
            target = PlayerInstant.Instance.GetComponent<Transform>();
        }

        angle = Mathf.Atan2(transform.position.y - target.position.y, 
            transform.position.x - target.position.x) * 180 / Mathf.PI * -1;   
    }

    private void SetNullDirection()
    {
        movementDirection = Vector2.zero;
    }

    private void SetDirectionTowardsTarget()
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