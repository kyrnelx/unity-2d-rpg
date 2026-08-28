public class EnemyGrimReaperIdleState : EnemyGrimReaperState
{
    public EnemyGrimReaperIdleState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (timer > grimReaper.idleDuration)
        {
            stateMachine.ChangeState(grimReaper.WalkState);
        }
    }
}