public class EnemyOrcGroundedState : EnemyOrcState
{
    public EnemyOrcGroundedState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (enemyOrc.IsPlayerNear)
        {
            enemyOrc.CheckFlipByPlayer();
            if (enemyOrc.CanAttack())
            {
                stateMachine.ChangeState(enemyOrc.attackState);
            }
            else
            {
                stateMachine.ChangeState(enemyOrc.BattleState);
            }
        }
    }
}