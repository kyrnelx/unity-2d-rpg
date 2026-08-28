using UnityEngine;

public class EnemyGrimReaperBattleState : EnemyGrimReaperState
{
    public EnemyGrimReaperBattleState(StateMachine stateMachine, string animBoolName, Entity entity) : base(
        stateMachine, animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();

        grimReaper.CheckFlipByPlayer();
        grimReaper.Move();
        if (grimReaper.CanAttack())
        {
            if (Random.value > 0.75)
            {
                stateMachine.ChangeState(grimReaper.DisappearState);
            }
            else
            {
                stateMachine.ChangeState(grimReaper.AttackState);
            }
        }
    }
}