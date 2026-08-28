/// <summary>
/// 战士属性
/// </summary>
public class WarriorStats : EntityStats
{
    public Stat agility; // 1 point increase evasion by 1% and critical.chance by 1%
    public Stat intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public Stat strength; // 1 point increase damage by 1 and critical.power by 1%
    public Stat vitality; // 1 point increase health by 5 points
    public Stat criticalPower;
    public Stat criticalChance;

    public override void DoDamage(EntityStats target)
    {
        if (target.currentHealth <= 0) return;
        if (CanEvasion(target)) return;

        int armor = target.armor.GetValue();
        if (target.isChilled) armor = (int)(armor * 0.8f);
        int damage = CalculateDamage() - armor;
        if (CanCritical()) damage = CalculateCriticalDamage(damage);
        if (damage > 0) target.TakeDamage(damage);

        int magicResistance = target.magicResistance.GetValue();
        if (target.isShocked) magicResistance = (int)(magicResistance * 0.8f);
        int magicDamage = CalculateMagicDamage() - magicResistance;
        if (CanCritical()) magicDamage = CalculateCriticalDamage(magicDamage);
        if (magicDamage > 0) target.TakeDamage(magicDamage);
    }

    public bool CanCritical()
    {
        int totalChance = criticalChance.GetValue() + agility.GetValue();
        if (GetRandomValue() < totalChance) return true;
        return false;
    }

    public int CalculateDamage()
    {
        int totalDamage = attackPower.GetValue() + strength.GetValue();
        return totalDamage;
    }

    public int CalculateMagicDamage()
    {
        int totalDamage = magicPower.GetValue() + intelligence.GetValue();
        return totalDamage;
    }

    private int CalculateCriticalDamage(int damage)
    {
        int extraDamage = damage * (criticalPower.GetValue() / 100);
        damage += extraDamage;
        return damage;
    }
}