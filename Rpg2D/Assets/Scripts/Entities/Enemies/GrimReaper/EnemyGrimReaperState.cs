public class EnemyGrimReaperState : State
{
    protected EnemyGrimReaper grimReaper;

    public EnemyGrimReaperState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
        grimReaper = entity as EnemyGrimReaper;
    }
}