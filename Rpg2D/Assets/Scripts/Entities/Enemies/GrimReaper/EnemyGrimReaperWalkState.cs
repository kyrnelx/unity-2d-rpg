public class EnemyGrimReaperWalkState : EnemyGrimReaperState
{
    public EnemyGrimReaperWalkState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        grimReaper.Move();

        if (grimReaper.IsGrounded == false || grimReaper.IsWalled)
        {
            grimReaper.Flip();
            stateMachine.ChangeState(grimReaper.IdleState);
        }

        base.Update();
    }
}