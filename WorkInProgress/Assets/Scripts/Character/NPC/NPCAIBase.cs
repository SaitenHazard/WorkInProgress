using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCAIBase : MonoBehaviour
{
    public Patrol patrol;
    public enumNPCActions basicAction;

    protected float angle;
    protected Transform target;
    protected SpeechBubble speechBubble;
    protected CharacterMovementModel m_movementModel;
    protected Vector2 movementDirection;
    public enumNPCActions npcAction;

    private CharacterMovementModel p_movementModel;
    private int pacingDirection;
    private float pacingTime;
    private float pacingWaitTime;
    private int spawnCount;
    private float m_speed;

    //---------------------COMMON---------------------//

    protected void Awake()
    {
        speechBubble = transform.parent.GetComponentInChildren<SpeechBubble>();
        m_movementModel = GetComponentInParent<CharacterMovementModel>();
        m_speed = m_movementModel.GetSpeed();
        SpawnManager spawnManager = transform.parent.GetComponentInChildren<SpawnManager>();
    }

    protected virtual void Start()
    {
        spawnCount = 0;

        npcAction = basicAction;
    }

    protected void Update()
    {
        DoMovement();
    }

    //------------------------------------------------//
    //---------------------PUBLIC---------------------//

    public void SetTarget(Transform transform)
    {
        target = transform;
    }

    public GameObject GetPatrolObject()
    {
        if (patrol == null)
            return null;

        return patrol.gameObject;
    }

    public enumNPCActions GetEnemyAction()
    {
        return npcAction;
    }

    public void SetEnemyAction(enumNPCActions m_Action)
    {
        npcAction = m_Action;
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
        if (npcAction == enumNPCActions.patrol)
        {
            if (target == null)
                Debug.Log("Here: " + transform.parent.name);
            target = patrol.GetTarget();
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
        if (npcAction == enumNPCActions.pacing)
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

        if (npcAction == enumNPCActions.NULL)
            movementDirection = Vector2.zero;

        if (npcAction == enumNPCActions.idle)
        {
            movementDirection = new Vector2(0, -1);
        }

        if (npcAction == enumNPCActions.face)
        {
            if(m_movementModel.GetSpeed() != 0)
            {
                m_movementModel.SetSpeed(0f);
                movementDirection = PlayerInstant.Instance.GetComponent<CharacterMovementModel>().GetReverseFacingDirection();
            }
            else
            {
                movementDirection = Vector2.zero;
            }
        }
        else
        {
            m_movementModel.SetSpeed(m_speed);
        }
    }
}