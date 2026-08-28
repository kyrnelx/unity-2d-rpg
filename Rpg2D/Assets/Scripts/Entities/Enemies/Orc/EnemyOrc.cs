using UnityEngine;

public class EnemyOrc : Enemy
{
    public EnemyOrcIdleState IdleState { get; private set; }
    public EnemyOrcBattleState BattleState { get; private set; }
    public EnemyOrcAttackState attackState { get; private set; }
    public EnemyOrcDeadState deathState { get; private set; }
    public EnemyOrcPatrolState patrolState { get; private set; }
    private Vector2 _moveVec;

    protected override void Awake()
    {
        base.Awake();
        IdleState = new EnemyOrcIdleState(stateMachine, "Idle", this);
        BattleState = new EnemyOrcBattleState(stateMachine, "Run", this);
        attackState = new EnemyOrcAttackState(stateMachine, "Attack", this);
        deathState = new EnemyOrcDeadState(stateMachine, "Dead", this);
        patrolState = new EnemyOrcPatrolState(stateMachine, "Run", this);
        stateMachine.Initialise(patrolState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deathState);
    }
}