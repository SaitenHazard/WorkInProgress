using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBase : MonoBehaviour
{
    public GameObject projectileObject;
    public Patrol patrol;

    protected float angle;
    protected Transform target;
    protected SpeechBubble speechBubble;
    protected CharacterMovementModel m_movementModel;
    protected Vector2 movementDirection;
    protected Coroutine projectileCoroutine;
    protected enumEnemyActions enemyAction;
    protected List<Collider2D> enemyColliders = new List<Collider2D>();
    protected int enemyColliderIndex;

    private CharacterMovementModel p_movementModel;
    private Animator m_Animator;

    protected void Awake()
    {
        speechBubble = transform.parent.GetComponentInChildren<SpeechBubble>();
        p_movementModel = PlayerInstant.Instance.GetComponent<CharacterMovementModel>();
        m_movementModel = GetComponentInParent<CharacterMovementModel>();
        m_Animator = transform.parent.GetComponentInChildren<Animator>();
    }

    protected void Start()
    {
        enemyAction = enumEnemyActions.patrol;
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
        UpdateUniqueAni();
        UpdateAngle();
        SetDirectionTowardsTarget();
        DoMovement();
    }

    private void DoMovement()
    {
        m_movementModel.SetDirection(movementDirection);
    }

    private void UpdateUniqueAni()
    {
        m_Animator.SetBool("UniqueAction", enemyAction == enumEnemyActions.defend);
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
            target = enemyColliders[enemyColliderIndex].GetComponent<Transform>();
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
        if (collider2D.gameObject.tag == "Player")
        {
        }
    }

    virtual protected void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
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

        if (enemyAction == enumEnemyActions.healAlly)
        {
            Debug.Log("Distance from enemy: " + Vector2.Distance(transform.position, target.position));

            if (Vector2.Distance(transform.position, target.position) < 1)
            {
                movementDirection = Vector2.zero;
                StartCoroutine(DoHeal());
            }
        }

        if (enemyAction == enumEnemyActions.NULL || enemyAction == enumEnemyActions.defend)
        {
            movementDirection = Vector2.zero;
        }
    }

    private IEnumerator DoHeal()
    {
        float yieldTime = 0.5f;

        m_Animator.SetBool("UniqueAction", true);

        //Debug.Log("Before Yeild Return");

        yield return new WaitForSeconds(yieldTime);

        //Debug.Log("After Yeild Return");

        Attackable allyAttackable = enemyColliders[enemyColliderIndex].transform.parent.GetComponentInChildren<Attackable>();
        Attackable selfAttackable = transform.parent.GetComponentInChildren<Attackable>();

        m_Animator.SetBool("UniqueAction", false);
        allyAttackable.AddHealth(allyAttackable.GetMaxHealth());
        selfAttackable.SubstractHealth(selfAttackable.GetMaxHealth()/3);
        Debug.Log("1/3 max health" + selfAttackable.GetMaxHealth() / 3);
    }
}