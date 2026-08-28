using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public float moveSpeed;
    public bool isFacingRight = true;
    public float facingDir = 1;
    public float knockbackForce;
    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public Animator animator;
    [HideInInspector] public EntityStats entityStats;
    [HideInInspector] public EntityAnimationEvent animEvent;
    [HideInInspector] public EntityFX entityFX;
    protected Collider collider;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();
        entityFX = GetComponent<EntityFX>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<Collider>();
        entityStats = GetComponent<EntityStats>();
        animEvent = transform.Find("Animator").GetComponent<EntityAnimationEvent>();
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void LateUpdate()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void OnDestroy()
    {
    }

    protected virtual void OnApplicationPause(bool pauseStatus)
    {
    }

    protected virtual void OnApplicationFocus(bool hasFocus)
    {
    }

    protected virtual void OnApplicationQuit()
    {
    }

    public void Knockback(float knockbackForce)
    {
        rb.velocity = new Vector2(knockbackForce * -facingDir, rb.velocity.y);
    }

    public virtual void Flip()
    {
        isFacingRight = !isFacingRight;
        facingDir = -facingDir;
        transform.Rotate(0, 180, 0);
    }

    public virtual void Die()
    {
    }

    public bool IsTriggered()
    {
        return animEvent.IsTriggered();
    }
}