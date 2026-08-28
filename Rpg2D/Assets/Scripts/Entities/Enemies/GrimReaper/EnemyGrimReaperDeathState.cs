using UnityEngine;

public class EnemyGrimReaperDeathState : EnemyGrimReaperState
{
    private ulong _sfx;

    public EnemyGrimReaperDeathState(StateMachine stateMachine, string animBoolName, Entity entity) : base(stateMachine,
        animBoolName, entity)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SoundManager.Instance.PlaySfx(AudioID.SfxGrimRepearDeath, ref _sfx);
    }

    public override void Update()
    {
        base.Update();
        if (grimReaper.IsTriggered())
        {
            GameObject.Destroy(grimReaper.gameObject);
        }
    }
}