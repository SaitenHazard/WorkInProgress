using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBase : MonoBehaviour
{
    public GameObject projectileObject;
    public Patrol patrol;
    public GameObject spawnObject;
    public enumEnemyActions basicActionWithPlayer;
    public enumEnemyActions basicAction;

    protected float angle;
    protected Transform target;
    protected SpeechBubble speechBubble;
    protected CharacterMovementModel m_movementModel;
    protected Vector2 movementDirection;
    protected Coroutine projectileCoroutine;
    protected int enemyColliderIndex;
    protected PlayerStats playerStats;
    protected PlayerInstant playerInstance;
    protected enumEnemyActions enemyAction;

    private CharacterMovementModel p_movementModel;
    private Animator m_Animator;
    private Attackable attackable;
    private int pacingDirection;
    private float pacingTime;
    private float pacingWaitTime;
    private int spawnCount;

    //---------------------COMMON---------------------//

    protected void Awake()
    {
        speechBubble = transform.parent.GetComponentInChildren<SpeechBubble>();
        m_movementModel = GetComponentInParent<CharacterMovementModel>();
        m_Animator = transform.parent.GetComponentInChildren<Animator>();
        attackable = transform.parent.GetComponentInChildren<Attackable>();

        SpawnManager spawnManager = transform.parent.GetComponentInChildren<SpawnManager>();

        if(spawnManager != null)
            spawnObject = spawnManager.gameObject;
    }

    protected virtual void Start()
    {
        playerInstance = PlayerInstant.Instance;

        p_movementModel = playerInstance.GetComponent<CharacterMovementModel>();
        playerStats = playerInstance.GetComponent<PlayerStats>();

        spawnCount = 0;

        enemyAction = basicAction;
    }

    protected void Update()
    {
        UpdateActionEffects();
        UpdateAngle();
        DoMovement();
        CheckPlayerStats();
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

    public void DeductSpawn()
    {
        spawnCount--;
    }

    //------------------------------------------------//
    //---------------------IENUMRATORS----------------//

    private bool isDoSpawnActive = false;
    private bool projectileOn = false;
    private bool pacingActive = false;
    private bool ienumeratorDoHealCheck = false;

    private IEnumerator DoHeal()
    {
        float yieldTime = 0.5f;

        ienumeratorDoHealCheck = true;
        m_Animator.SetBool("Heal", true);

        yield return new WaitForSeconds(yieldTime);

        ienumeratorDoHealCheck = false;
        m_Animator.SetBool("Heal", false);

        Attackable allyAttackable = target.GetComponentInChildren<Attackable>();
        Attackable selfAttackable = transform.parent.GetComponentInChildren<Attackable>();

        allyAttackable.SetHealth(allyAttackable.GetMaxHealth());
        selfAttackable.SubstractHealth(selfAttackable.GetMaxHealth() / 3);

        enemyAction = basicAction;
    }

    private IEnumerator DoSpawn()
    {
        float yieldTime = 5f;

        isDoSpawnActive = true;

        if (spawnCount < 5)
        {
            GameObject tempSpawnObject = Instantiate(spawnObject);
            SpawnManager spawnManager = tempSpawnObject.GetComponent<SpawnManager>();

            tempSpawnObject.transform.position = spawnObject.transform.position;

            spawnManager.Initialize(this);
            spawnCount++;

            yield return new WaitForSeconds(yieldTime);
        }

        isDoSpawnActive = false;
    }

    protected IEnumerator DoProjectile()
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

        StartCoroutine(DoProjectile());
    }

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
    //---------------------CHECKS---------------------//

    private void CheckPlayerStats()
    {
        if(playerStats.IsInvisibleUp() == true && enemyAction == enumEnemyActions.chase)
        {
            enemyAction = enumEnemyActions.patrol;
        }
    }

    private void UpdateActionEffects()
    {
        m_Animator.SetBool("Defend", enemyAction == enumEnemyActions.defend);

        if (enemyAction == enumEnemyActions.healAlly && target == null)
            enemyAction = basicAction;

        if (enemyAction == enumEnemyActions.chaseDecoy && target == null)
            enemyAction = basicAction;

        if (enemyAction != enumEnemyActions.spawn)
            isDoSpawnActive = false;

        if (enemyAction == enumEnemyActions.spawn && isDoSpawnActive == false)
            StartCoroutine(DoSpawn());

        if (enemyAction != enumEnemyActions.pacing)
            pacingActive = false;

        if (enemyAction == enumEnemyActions.chase || enemyAction == enumEnemyActions.chaseDecoy)
        {
            if (projectileOn == true || projectileObject == null)
                return;

            projectileOn = true;
            StartCoroutine(DoProjectile());
        }
        else
        {
            projectileOn = false;
        }
    }

    //------------------------------------------------//
    //---------------------MOVEMENTS------------------//

    protected void DoMovement()
    {
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

        if (enemyAction == enumEnemyActions.chaseDecoy)
        {
            if (Vector2.Distance(transform.position, target.position) < 0.7f)
            {
               movementDirection = Vector2.zero;
            }
        }

        if (enemyAction == enumEnemyActions.NULL || enemyAction == enumEnemyActions.defend || attackable.GetHealth() == 0)
            movementDirection = Vector2.zero;

        if (enemyAction == enumEnemyActions.idle)
        {
            movementDirection = new Vector2(0, -1);
        }
    }

    //------------------------------------------------//
    //---------------------VIRTUALS------------------//

    virtual protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        
    }

    virtual protected void OnTriggerStay2D(Collider2D collider2D)
    {
        
    }

    virtual protected void OnTriggerExit2D(Collider2D collider2D)
    {
       
    }

    //------------------------------------------------//
}