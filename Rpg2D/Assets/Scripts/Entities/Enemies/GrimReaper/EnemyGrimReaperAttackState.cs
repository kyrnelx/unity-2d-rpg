public class EnemyGrimReaperAttackState : EnemyGrimReaperState
{
    public EnemyGrimReaperAttackState(StateMachine stateMachine, string animBoolName, Entity entity) : base(
        stateMachine, animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (grimReaper.IsTriggered())
        {
            stateMachine.ChangeState(grimReaper.BattleState);
        }
    }
}