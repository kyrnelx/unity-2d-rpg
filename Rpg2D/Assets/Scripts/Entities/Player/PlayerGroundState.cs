using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGrounded)
        {
            stateMachine.ChangeState(player.JumpState);
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            stateMachine.ChangeState(player.AttackState);
        }

        if (Input.GetKeyDown(KeyCode.F) && player.IsGrounded && player.skill.SkillRoll.CanRelease())
        {
            stateMachine.ChangeState(player.RollState);
            isReturn = true;
        }

        if (Input.GetMouseButtonDown(1) && player.skill.SkillIdleBlock.CanRelease())
        {
            stateMachine.ChangeState(player.IdleBlockState);
        }
    }
}