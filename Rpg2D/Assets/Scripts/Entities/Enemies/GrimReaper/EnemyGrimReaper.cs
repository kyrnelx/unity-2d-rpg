using UnityEngine;

public class EnemyGrimReaper : Enemy
{
    [SerializeField] private BossArea bossArea;
    public float magicDuration;

    private bool _lastIsMagicState;

    public bool LastIsMagicState
    {
        get => _lastIsMagicState;
        set => _lastIsMagicState = value;
    }

    public EnemyGrimReaperIdleState IdleState { get; private set; }
    public EnemyGrimReaperWalkState WalkState { get; private set; }
    public EnemyGrimReaperAttackState AttackState { get; private set; }
    public EnemyGrimReaperAppearState AppearState { get; private set; }
    public EnemyGrimReaperDisappearState DisappearState { get; private set; }
    public EnemyGrimReaperMagicState MagicState { get; private set; }
    public EnemyGrimReaperDeathState DeathState { get; private set; }
    public EnemyGrimReaperBattleState BattleState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        IdleState = new EnemyGrimReaperIdleState(stateMachine, "Idle", this);
        WalkState = new EnemyGrimReaperWalkState(stateMachine, "Walk", this);
        AttackState = new EnemyGrimReaperAttackState(stateMachine, "Attack", this);
        AppearState = new EnemyGrimReaperAppearState(stateMachine, "Appear", this);
        DisappearState = new EnemyGrimReaperDisappearState(stateMachine, "Disappear", this);
        MagicState = new EnemyGrimReaperMagicState(stateMachine, "Magic", this);
        DeathState = new EnemyGrimReaperDeathState(stateMachine, "Disappear", this);
        BattleState = new EnemyGrimReaperBattleState(stateMachine, "Walk", this);
        stateMachine.Initialise(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public Vector2 GetReleaseMagicStatePosition()
    {
        return Random.value > 0.5f ? bossArea.teleportPosition1.position : bossArea.teleportPosition2.position;
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(DeathState);
        GameManager.Instance.IsGrimReaperDead = true;
        GameManager.Instance.ChallengeBoss(false);
    }
}