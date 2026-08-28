using UnityEngine;

public class AnimEventEnemyOrc : EntityAnimationEvent
{
    public LayerMask attackLayer;
    private EnemyOrc _enemyOrc;

    protected override void Awake()
    {
        base.Awake();
        _enemyOrc = GetComponentInParent<EnemyOrc>();
    }

    private void Attack()
    {
        Collider2D[] cds = Physics2D.OverlapCircleAll(_enemyOrc.AttackPosition, _enemyOrc.AttackRaduis, attackLayer);
        if (cds.Length > 0 && cds[0].CompareTag("player"))
        {
            WarriorStats warriorStats = _enemyOrc.entityStats as WarriorStats;
            warriorStats.DoDamage(PlayerManager.Instance.player.playerStats);
        }
    }
}