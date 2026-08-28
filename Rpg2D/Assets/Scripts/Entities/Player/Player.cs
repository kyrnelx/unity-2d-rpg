using Cinemachine;
using UnityEngine;

public class Player : Entity
{
    public float jumpForce;
    public float attackRecoveryCooldown;
    public float[] attackRangeArray;
    public float[] attackSlightForce;
    public Transform attackPoint;
    public float rollForce;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerDeathState DeathState { get; private set; }
    public PlayerIdleBlockState IdleBlockState { get; private set; }
    public PlayerRollState RollState { get; private set; }

    public bool IsGrounded => _groundedChecker.IsChecked;
    public Vector2 Velocity => _rb.velocity;
    public Vector2 GetInput => _input;

    public SkillManager skill => SkillManager.Instance;
    public PlayerStats playerStats => entityStats as PlayerStats;

    private Rigidbody2D _rb;
    private ColliderChecker _groundedChecker;
    private CinemachineImpulseSource _cinemachineImpulseSource;
    public CinemachineImpulseSource CinemachineImpulseSource => _cinemachineImpulseSource;

    private int _attackCounter = 0;
    private float _attackTimer = 0;
    private Vector2 _input = new Vector2();
    private Vector2 _move = new Vector2();
    private Vector2 _jump = new Vector2();
    private ulong _deathSfx;
    private ulong _selfDeathSfx;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _groundedChecker = GetComponentInChildren<ColliderChecker>();
        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();

        IdleState = new PlayerIdleState(stateMachine, "Idle", this);
        RunState = new PlayerRunState(stateMachine, "Run", this);
        JumpState = new PlayerJumpState(stateMachine, "Jump", this);
        FallState = new PlayerFallState(stateMachine, "Fall", this);
        AttackState = new PlayerAttackState(stateMachine, "Attack", this);
        DeathState = new PlayerDeathState(stateMachine, "Death", this);
        IdleBlockState = new PlayerIdleBlockState(stateMachine, "IdleBlock", this);
        RollState = new PlayerRollState(stateMachine, "Roll", this);
        stateMachine.Initialise(IdleState);
    }

    protected override void Start()
    {
        base.Start();
        PlayerManager.Instance.Initialize();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");

        UpdateAttackTimer();

        CheckAndReleaseMagicOrbSkill();
    }

    private void CheckAndReleaseMagicOrbSkill()
    {
        if (Input.GetKeyDown(KeyCode.G) && skill.SkillMagicOrb.CanRelease() && skill.SkillMagicOrb.isUnlocked)
        {
            skill.SkillMagicOrb.Release();
        }
    }

    #region Attack

    private void UpdateAttackTimer()
    {
        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0)
        {
            _attackCounter = 0;
        }
    }

    public void SetAttackAnim()
    {
        animator.SetInteger("AttackCounter", GetAttackCounter());
    }

    public int GetAttackCounter()
    {
        _attackTimer = attackRecoveryCooldown;
        _attackCounter++;
        if (_attackCounter > 3)
            _attackCounter = 1;
        return _attackCounter;
    }

    #endregion

    public Vector2 GetMove()
    {
        _move.x = _input.x * moveSpeed;
        _move.y = _rb.velocity.y;
        return _move;
    }

    public Vector2 GetJump()
    {
        _jump.x = _rb.velocity.x;
        _jump.y = jumpForce;
        return _jump;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }

    public void CheckFlip()
    {
        if (GetInput.x > 0 && isFacingRight == false)
        {
            Flip();
        }
        else if (GetInput.x < 0 && isFacingRight == true)
        {
            Flip();
        }
    }

    public override void Die()
    {
        PlayerManager.Instance.IsPlayerDead = true;
        base.Die();
        SoundManager.Instance.PlaySfx(AudioID.SfxPlayerDeath, ref _deathSfx);
        SoundManager.Instance.PlaySfx(AudioID.SfxPlayerSelfDeath, ref _selfDeathSfx);
        stateMachine.ChangeState(DeathState);
        GameManager.Instance.IsGrimReaperDead = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeArray[0]);
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeArray[1]);
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeArray[2]);
    }
}