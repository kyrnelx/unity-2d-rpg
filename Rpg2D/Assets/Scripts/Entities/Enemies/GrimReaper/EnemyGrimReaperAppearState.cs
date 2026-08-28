public class EnemyGrimReaperAppearState : EnemyGrimReaperState
{
    public EnemyGrimReaperAppearState(StateMachine stateMachine, string animBoolName, Entity entity) : base(
        stateMachine, animBoolName, entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (grimReaper.LastIsMagicState)
        {
            grimReaper.transform.position = PlayerManager.Instance.player.transform.position;
        }
        else
        {
            grimReaper.transform.position = grimReaper.GetReleaseMagicStatePosition();
        }
    }

    public override void Update()
    {
        base.Update();
        if (grimReaper.IsTriggered())
        {
            if (grimReaper.LastIsMagicState)
            {
                grimReaper.LastIsMagicState = false;
                stateMachine.ChangeState(grimReaper.BattleState);
            }
            else
            {
                grimReaper.LastIsMagicState = true;
                stateMachine.ChangeState(grimReaper.MagicState);
            }
        }
    }
}