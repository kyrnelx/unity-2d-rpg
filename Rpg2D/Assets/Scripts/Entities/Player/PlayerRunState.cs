public class PlayerRunState : PlayerGroundState
{
    private ulong _runSfx;

    public PlayerRunState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SoundManager.Instance.PlaySfx(AudioID.SfxGrassStep, ref _runSfx, true);
    }

    public override void Update()
    {
        base.Update();
        if (IsReturn()) return;
        player.SetVelocity(player.GetMove());
        player.CheckFlip();
        if (player.GetInput.x == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        SoundManager.Instance.StopSfx(_runSfx);
    }
}