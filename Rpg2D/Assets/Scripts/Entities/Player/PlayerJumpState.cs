public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(player.GetJump());
    }

    public override void Update()
    {
        base.Update();
        if (player.Velocity.y < 0)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }
}