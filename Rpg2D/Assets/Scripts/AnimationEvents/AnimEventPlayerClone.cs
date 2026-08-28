using UnityEngine;

public class AnimEventPlayerClone : EntityAnimationEvent
{
    public LayerMask attackLayer;
    public Transform attackPoint;
    private Player _player;

    protected override void Awake()
    {
        base.Awake();
        _player = PlayerManager.Instance.player;
    }

    public void Attack1()
    {
        Attack(0);
    }

    public void Attack2()
    {
        Attack(1);
    }

    public void Attack3()
    {
        Attack(2);
    }

    private void Attack(int rangeIndex)
    {
        SoundManager.Instance.PlaySwordSfx();

        EventSubscriber.PlayerAttack?.Invoke(attackPoint);

        Collider2D[] cds =
            Physics2D.OverlapCircleAll(attackPoint.position, _player.attackRangeArray[rangeIndex], attackLayer);
        foreach (var cd in cds)
        {
            Entity target = cd.GetComponent<Entity>();
            if (target == null) continue;

            EventSubscriber.PlayFleshVFX?.Invoke(target);
            SoundManager.Instance.PlaySwordFleshSfx();
            target.Knockback(_player.knockbackForce);
            _player.playerStats.DoDamage(target.entityStats);
        }
    }

    public override void Trigger()
    {
    }

    public override bool IsTriggered()
    {
        Debugger.Warning($"[AnimEvent_PlayeClone] IsTriggered not implemented | Class: {GetType().Name}");
        return false;
    }
}