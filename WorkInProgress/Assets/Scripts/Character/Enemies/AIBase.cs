using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBase : MonoBehaviour
{
    public GameObject projectileObject;
    public Patrol patrol;
    public GameObject spawnObject;

    protected float angle;
    protected Transform target;
    protected Transform injuredAlly;
    protected SpeechBubble speechBubble;
    protected CharacterMovementModel m_movementModel;
    protected Vector2 movementDirection;
    protected Coroutine projectileCoroutine;
    protected enumEnemyActions enemyAction;
    protected int enemyColliderIndex;
    protected PlayerStats playerStats;

    private CharacterMovementModel p_movementModel;
    private Animator m_Animator;
    private Attackable attackable;
    private int pacingDirection;
    private float pacingTime;
    private float pacingWaitTime;
    private int spawnCount;

    protected void Awake()
    {
        speechBubble = transform.parent.GetComponentInChildren<SpeechBubble>();
        p_movementModel = PlayerInstant.Instance.GetComponent<CharacterMovementModel>();
        m_movementModel = GetComponentInParent<CharacterMovementModel>();
        m_Animator = transform.parent.GetComponentInChildren<Animator>();
        attackable = transform.parent.GetComponentInChildren<Attackable>();
        playerStats = PlayerInstant.Instance.GetComponent<PlayerStats>();
    }

    protected virtual void Start()
    {
        if (transform.parent.name == "SplitterL1")
            enemyAction = enumEnemyActions.patrol;

        spawnCount = 0;
    }

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

    protected void Update()
    {
        UpdateActionEffects();
        UpdateAngle();
        SetDirectionTowardsTarget();
        DoMovement();
        CheckPlayerStats();
    }

    private void CheckPlayerStats()
    {
        if(playerStats.IsInvisibleUp() == true && enemyAction == enumEnemyActions.chase)
        {
            enemyAction = enumEnemyActions.patrol;
        }
    }

    private void DoMovement()
    {
        m_movementModel.SetDirection(movementDirection);
    }

    private void UpdateActionEffects()
    {
        m_Animator.SetBool("Defend", enemyAction == enumEnemyActions.defend);

        if (enemyAction != enumEnemyActions.pacing)
            pacingActive = false;
    }

    private void UpdateAngle()
    {
        if (enemyAction == enumEnemyActions.patrol)
        {
            target = patrol.GetTarget();
            angle = Mathf.Atan2(transform.position.y - target.position.y, transform.position.x - target.position.x) * 180 / Mathf.PI * -1;
        }

        if (enemyAction == enumEnemyActions.chase)
        {
            target = PlayerInstant.Instance.GetComponent<Transform>();
            angle = Mathf.Atan2(transform.position.y - target.position.y, transform.position.x - target.position.x) * 180 / Mathf.PI * -1;
        }

        if (enemyAction == enumEnemyActions.healAlly)
        {
            target = injuredAlly;
            angle = Mathf.Atan2(transform.position.y - target.position.y, transform.position.x - target.position.x) * 180 / Mathf.PI * -1;
        }
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
        
    }

    virtual protected void OnTriggerStay2D(Collider2D collider2D)
    {
        
    }

    virtual protected void OnTriggerExit2D(Collider2D collider2D)
    {
       
    }

    private bool pacingActive = false;

    //private IEnumerable DoSpawn()
    //{

    //}

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

        if (enemyAction == enumEnemyActions.spawn)
            movementDirection = new Vector2(-1, 0);

        if (enemyAction == enumEnemyActions.pacing)
        {
            Debug.Log("InPacing");

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

        if (enemyAction == enumEnemyActions.healAlly)
        {
            if (Vector2.Distance(transform.position, target.position) < 1)
            {
                movementDirection = Vector2.zero;

                if (ienumeratorDoHealCheck == false)
                {
                    StartCoroutine(DoHeal());
                }
            }
        }

        if (enemyAction == enumEnemyActions.NULL || enemyAction == enumEnemyActions.defend || attackable.GetHealth() == 0)
        {
            movementDirection = Vector2.zero;
        }
    }

    public void SetEnemyAction(enumEnemyActions m_Action)
    {
        enemyAction = m_Action;
    }

    private bool ienumeratorDoHealCheck = false;

    private IEnumerator DoHeal()
    {
        float yieldTime = 0.5f;

        ienumeratorDoHealCheck = true;
        m_Animator.SetBool("UniqueAction", true);

        yield return new WaitForSeconds(yieldTime);

        ienumeratorDoHealCheck = false;
        m_Animator.SetBool("UniqueAction", false);

        Attackable allyAttackable = injuredAlly.GetComponentInChildren<Attackable>();
        Attackable selfAttackable = transform.parent.GetComponentInChildren<Attackable>();

        allyAttackable.SetHealth(allyAttackable.GetMaxHealth());
        selfAttackable.SubstractHealth(selfAttackable.GetMaxHealth() / 3);

        enemyAction = enumEnemyActions.patrol;
    }
}