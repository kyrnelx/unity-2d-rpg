public class PlayerFallState : PlayerState
{
    public PlayerFallState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (player.IsGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}