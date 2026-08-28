using UnityEngine;

public class Enemy : Entity
{
    public EnemyID enemyID;
    public int coin;
    public GameObject[] dropItems;
    public float idleDuration;

    public Vector3 AttackPosition => attackPosition.position;
    public float AttackRaduis => attackRadius;
    public bool IsGrounded => groundChecker.IsChecked;
    public bool IsWalled => wallChecker.IsChecked;
    public bool IsPlayerNear => isPlayerInRangeChecker.IsChecked;

    [Header("Checker")] [SerializeField] private float attackRadius;
    [SerializeField] private Vector2 isPlayerInRangeCheckSize;
    [SerializeField] private float wallCheckRadius;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private ColliderChecker isPlayerInRangeChecker;
    [SerializeField] private ColliderChecker wallChecker;
    [SerializeField] private ColliderChecker groundChecker;

    protected WorldHealthBar healthBar;

    protected override void Awake()
    {
        base.Awake();
        healthBar = transform.Find("World_Health_Bar").GetComponent<WorldHealthBar>();
        entityStats.takeDamageCallback += healthBar.UpdateHealthBar;

        UpdateByEnemyData();
    }

    private void UpdateByEnemyData()
    {
        EnemyData enemyData = DataManager.Instance.LoadEnemyData(enemyID);
        coin = enemyData.coin;
        entityStats.maxHealth = enemyData.maxHealth;
        entityStats.currentHealth = entityStats.maxHealth.GetValue();
        entityStats.attackPower = enemyData.attackPower;
        entityStats.magicPower = enemyData.magicPower;
        entityStats.armor = enemyData.armor;
        entityStats.magicResistance = enemyData.magicResistance;
        if (entityStats is AlmightyStats almightyStats)
        {
            almightyStats.agility = enemyData.agility;
            almightyStats.intelligence = enemyData.intelligence;
            almightyStats.strength = enemyData.strength;
            almightyStats.vitality = enemyData.vitality;
            almightyStats.criticalPower = enemyData.criticalPower;
            almightyStats.criticalChance = enemyData.criticalChance;
            almightyStats.evasion = enemyData.evasion;
            almightyStats.lighting = enemyData.lighting;
            almightyStats.chill = enemyData.chill;
            almightyStats.ignite = enemyData.ignite;
        }
        else if (entityStats is WarriorStats warriorStats)
        {
            warriorStats.agility = enemyData.agility;
            warriorStats.intelligence = enemyData.intelligence;
            warriorStats.strength = enemyData.strength;
            warriorStats.vitality = enemyData.vitality;
            warriorStats.criticalPower = enemyData.criticalPower;
            warriorStats.criticalChance = enemyData.criticalChance;
        }
        else if (entityStats is MageStats mageStats)
        {
            mageStats.evasion = enemyData.evasion;
            mageStats.lighting = enemyData.lighting;
            mageStats.chill = enemyData.chill;
            mageStats.ignite = enemyData.ignite;
        }
    }

    public bool CanAttack()
    {
        bool canAttack = Vector2.Distance(transform.position, PlayerManager.Instance.player.transform.position) <
                         attackRadius;
        return canAttack;
    }

    public void CheckFlipByPlayer()
    {
        if (PlayerManager.Instance.player.transform.position.x > transform.position.x && isFacingRight == false ||
            PlayerManager.Instance.player.transform.position.x < transform.position.x && isFacingRight == true)
        {
            Flip();
        }
    }

    public void Move()
    {
        rb.velocity = new Vector2(facingDir * moveSpeed, rb.velocity.y);
    }

    public override void Die()
    {
        base.Die();
        foreach (var dropItem in dropItems)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }

        DataManager.Instance.GameDataModel.AddCoin(coin);
    }

    public override void Flip()
    {
        base.Flip();
        healthBar.transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
        Gizmos.DrawWireCube(isPlayerInRangeChecker.Position, isPlayerInRangeCheckSize);
        Gizmos.DrawWireSphere(wallChecker.Position, wallCheckRadius);
        Gizmos.DrawWireSphere(groundChecker.Position, groundCheckRadius);
    }

    protected override void OnDestroy()
    {
        entityStats.takeDamageCallback -= healthBar.UpdateHealthBar;
        base.OnDestroy();
    }
}

public enum EnemyID
{
    LightOrc,
    DarkOrc,
    GrimReaper
}