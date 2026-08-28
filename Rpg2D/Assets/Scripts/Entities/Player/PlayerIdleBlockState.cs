using UnityEngine;

public class PlayerIdleBlockState : PlayerState
{
    public PlayerIdleBlockState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.playerStats.isInvincible = true;
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonUp(1))
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.playerStats.isInvincible = false;
    }
}