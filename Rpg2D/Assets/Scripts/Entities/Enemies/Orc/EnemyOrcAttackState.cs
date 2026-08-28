public class EnemyOrcAttackState : EnemyOrcState
{
    private ulong _sfx;

    public EnemyOrcAttackState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SoundManager.Instance.PlaySfx(AudioID.SfxOrcRoar, ref _sfx);
    }

    public override void Update()
    {
        base.Update();
        if (enemyOrc.IsTriggered())
        {
            stateMachine.ChangeState(enemyOrc.IdleState);
        }
    }
}