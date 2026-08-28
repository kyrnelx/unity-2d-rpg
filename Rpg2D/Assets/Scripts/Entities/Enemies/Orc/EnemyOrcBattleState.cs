public class EnemyOrcBattleState : EnemyOrcState
{
    public EnemyOrcBattleState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        enemyOrc.Move();
        if (enemyOrc.CanAttack())
        {
            stateMachine.ChangeState(enemyOrc.attackState);
        }
        else if (enemyOrc.IsPlayerNear == false)
        {
            stateMachine.ChangeState(enemyOrc.IdleState);
        }
    }
}