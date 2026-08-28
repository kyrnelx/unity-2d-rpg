public class EnemyOrcIdleState : EnemyOrcGroundedState
{
    public EnemyOrcIdleState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        if (timer > enemyOrc.idleDuration)
        {
            stateMachine.ChangeState(enemyOrc.patrolState);
        }

        base.Update();
    }
}