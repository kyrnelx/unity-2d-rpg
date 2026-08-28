using UnityEngine;

public class PlayerRollState : PlayerState
{
    public PlayerRollState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(new Vector2(player.rollForce * player.facingDir, player.Velocity.y));
        if (player.skill.SkillClone.CanRelease())
            player.skill.SkillClone.Release(player);
    }

    public override void Update()
    {
        base.Update();
        if (player.IsTriggered())
        {
            player.SetVelocity(Vector2.zero);
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        if (player.skill.SkillClone.CanRelease())
            player.skill.SkillClone.Release(player);
        base.Exit();
    }
}