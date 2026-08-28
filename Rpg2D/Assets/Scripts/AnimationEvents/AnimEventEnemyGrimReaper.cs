using UnityEngine;

public class AnimEventEnemyGrimReaper : EntityAnimationEvent
{
    public LayerMask attackLayer;
    [SerializeField] private GameObject magicPrefab;
    private EnemyGrimReaper m_EnemyGrimReaper;

    protected override void Awake()
    {
        base.Awake();
        m_EnemyGrimReaper = GetComponentInParent<EnemyGrimReaper>();
    }

    private void Attack()
    {
        Collider2D[] cds = Physics2D.OverlapCircleAll(m_EnemyGrimReaper.AttackPosition, m_EnemyGrimReaper.AttackRaduis,
            attackLayer);
        if (cds.Length > 0 && cds[0].CompareTag("player"))
        {
            AlmightyStats warriorStats = m_EnemyGrimReaper.entityStats as AlmightyStats;
            warriorStats.DoDamage(PlayerManager.Instance.player.playerStats);
        }
    }

    public void MagicAttack()
    {
        Player player = PlayerManager.Instance.player;
        Vector3 spawnPosition = player.transform.position + Vector3.up * 3.25f;
        Instantiate(magicPrefab, spawnPosition, Quaternion.identity);
        if (Random.value > 0.5f)
        {
            spawnPosition = player.transform.position + Vector3.up * 3.25f + Vector3.right * 2.25f;
            Instantiate(magicPrefab, spawnPosition, Quaternion.identity);
        }

        if (Random.value > 0.5f)
        {
            spawnPosition = player.transform.position + Vector3.up * 3.25f + Vector3.left * 2.25f;
            Instantiate(magicPrefab, spawnPosition, Quaternion.identity);
        }
    }
}