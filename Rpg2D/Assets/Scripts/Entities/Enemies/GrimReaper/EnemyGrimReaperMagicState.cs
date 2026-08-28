public class EnemyGrimReaperMagicState : EnemyGrimReaperState
{
    public EnemyGrimReaperMagicState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Update()
    {
        base.Update();
        if (timer > grimReaper.magicDuration)
        {
            stateMachine.ChangeState(grimReaper.DisappearState); 
        }
    }
}