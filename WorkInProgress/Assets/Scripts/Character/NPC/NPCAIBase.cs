using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCAIBase : MonoBehaviour
{
    public Patrol patrol;
    public enumEnemyActions basicAction;

    protected float angle;
    protected Transform target;
    protected SpeechBubble speechBubble;
    protected CharacterMovementModel m_movementModel;
    protected Vector2 movementDirection;
    protected enumEnemyActions enemyAction;

    private CharacterMovementModel p_movementModel;
    private int pacingDirection;
    private float pacingTime;
    private float pacingWaitTime;
    private int spawnCount;

    //---------------------COMMON---------------------//

    protected void Awake()
    {
        speechBubble = transform.parent.GetComponentInChildren<SpeechBubble>();
        m_movementModel = GetComponentInParent<CharacterMovementModel>();

        SpawnManager spawnManager = transform.parent.GetComponentInChildren<SpawnManager>();
    }

    protected virtual void Start()
    {
        spawnCount = 0;

        enemyAction = basicAction;

        GetComponent<CircleCollider2D>().radius = 3f;
    }

    protected void Update()
    {
        DoMovement();
    }

    //------------------------------------------------//
    //---------------------PUBLIC---------------------//

    public GameObject GetPatrolObject()
    {
        if (patrol == null)
            return null;

        return patrol.gameObject;
    }

    public enumEnemyActions GetEnemyAction()
    {
        return enemyAction;
    }

    public void SetEnemyAction(enumEnemyActions m_Action)
    {
        enemyAction = m_Action;
    }


    //------------------------------------------------//
    //---------------------IENUMRATORS----------------//

    private bool pacingActive = false;

    private IEnumerator SetPacing()
    {
        pacingDirection = Random.Range(1, 4);
        pacingTime = Random.Range(0.2f, 1f);
        pacingWaitTime = 2;
        pacingActive = true;

        yield return new WaitForSeconds(pacingTime);

        pacingDirection = 0;

        yield return new WaitForSeconds(pacingWaitTime);

        pacingActive = false;
    }

    //------------------------------------------------//
    //---------------------MOVEMENTS------------------//

    protected void DoMovement()
    {
        UpdateAngle();
        SetDirectionTowardsTarget();
        SetDirectionSpecial();

        m_movementModel.SetDirection(movementDirection);
    }

    private void UpdateAngle()
    {
        if (enemyAction == enumEnemyActions.patrol)
        {
            target = patrol.GetTarget();
            angle = Mathf.Atan2(transform.position.y - target.position.y, transform.position.x - target.position.x) * 180 / Mathf.PI * -1;
        }

        if (enemyAction == enumEnemyActions.chase || enemyAction == enumEnemyActions.healAlly || 
            enemyAction == enumEnemyActions.chaseDecoy)
        {
            angle = Mathf.Atan2(transform.position.y - target.position.y, transform.position.x - target.position.x) * 180 / Mathf.PI * -1;
        }
    }

    protected void SetDirectionTowardsTarget()
    {
        if (angle >= 22.5 && angle <= 67.5)
        {
            movementDirection = new Vector2(-1, 1);
        }

        if (angle >= 112.5 && angle <= 157.5)
        {
            movementDirection = new Vector2(1, 1);
        }

        if (angle <= -22.5 && angle >= -67.5)
        {
            movementDirection = new Vector2(-1, -1);
        }

        if (angle <= -112.5 && angle >= -157.5)
        {
            movementDirection = new Vector2(1, -1);
        }

        if (angle >= 67.5 && angle <= 112.5)
        {
            movementDirection = new Vector2(0, 1);
        }

        if (angle <= -67.5 && angle >= -112.5)
        {
            movementDirection = new Vector2(0, -1);
        }

        if (angle <= 22.5 && angle >= 0 || angle >= -22.5 && angle <= 0)
        {
            movementDirection = new Vector2(-1, 0);
        }

        if (angle >= 157.5 && angle <= 180 || angle <= -157.5 && angle >= -180)
        {
            movementDirection = new Vector2(1, 0);
        }
    }

    protected void SetDirectionSpecial()
    {
        if (enemyAction == enumEnemyActions.spawn)
            movementDirection = new Vector2(-1, 0);

        if (enemyAction == enumEnemyActions.pacing)
        {
            if (pacingActive == false)
                StartCoroutine(SetPacing());

            if (pacingDirection == 1)
                movementDirection = new Vector2(1, 0);
            else if (pacingDirection == 2)
                movementDirection = new Vector2(0, -1);
            else if (pacingDirection == 3)
                movementDirection = new Vector2(-1, 0);
            else if (pacingDirection == 4)
                movementDirection = new Vector2(0, 1);
            else
                movementDirection = new Vector2(0, 0);
        }

        if (enemyAction == enumEnemyActions.NULL || enemyAction == enumEnemyActions.defend)
            movementDirection = Vector2.zero;

        if (enemyAction == enumEnemyActions.idle)
        {
            movementDirection = new Vector2(0, -1);
        }
    }
}