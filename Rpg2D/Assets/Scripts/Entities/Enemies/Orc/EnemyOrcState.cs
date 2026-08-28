public class EnemyOrcState : State
{
    protected EnemyOrc enemyOrc;

    public EnemyOrcState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
        enemyOrc = entity as EnemyOrc;
    }
}