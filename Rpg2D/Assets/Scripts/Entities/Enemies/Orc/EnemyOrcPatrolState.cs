public class EnemyOrcPatrolState : EnemyOrcGroundedState
{
    public EnemyOrcPatrolState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        enemyOrc.Move();

        if (enemyOrc.IsGrounded == false || enemyOrc.IsWalled)
        {
            enemyOrc.Flip();
            stateMachine.ChangeState(enemyOrc.IdleState);
        }

        base.Update();
    }
}