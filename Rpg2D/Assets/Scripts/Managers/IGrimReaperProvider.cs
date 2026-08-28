public interface IGrimReaperProvider
{
    public EnemyGrimReaper GrimReaper { get; }
    public bool IsGrimReaperDead { get; set; }
}