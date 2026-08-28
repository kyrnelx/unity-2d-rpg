/// <summary>
/// 全能者属性：法师 + 战士
/// </summary>
public class AlmightyStats : EntityStats
{
    public Stat agility; // 1 point increase evasion by 1% and critical.chance by 1%
    public Stat intelligence; // 1 point increase magic damage by 1 and magic resistance by 3
    public Stat strength; // 1 point increase damage by 1 and critical.power by 1%
    public Stat vitality; // 1 point increase health by 5 points
    public Stat criticalPower;
    public Stat criticalChance;
    public Stat evasion;
    public Stat lighting;
    public Stat chill;
    public Stat ignite;

    public override void DoDamage(EntityStats target)
    {
        if (target.currentHealth <= 0) return;
        if (CanEvasion(target)) return;

        if (CanChill()) target.SetMagicStatus(ElementStatusType.Chill);
        if (CanLighting()) target.SetMagicStatus(ElementStatusType.Lighting);
        if (CanIgnite()) target.SetMagicStatus(ElementStatusType.Ignite);

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
        int totalChance = criticalChance.GetValue();
        if (GetRandomValue() < totalChance) return true;
        return false;
    }

    public bool CanEvasion()
    {
        int totalChance = evasion.GetValue() + agility.GetValue();
        if (GetRandomValue() < totalChance) return true;
        return false;
    }

    public bool CanChill()
    {
        int totalChance = chill.GetValue() + intelligence.GetValue();
        if (GetRandomValue() < totalChance) return true;
        return false;
    }

    public bool CanIgnite()
    {
        int totalChance = ignite.GetValue() + intelligence.GetValue();
        if (GetRandomValue() < totalChance) return true;
        return false;
    }

    public bool CanLighting()
    {
        int totalChance = lighting.GetValue() + intelligence.GetValue();
        if (GetRandomValue() < totalChance) return true;
        return false;
    }

    private int CalculateCriticalDamage(int damage)
    {
        int extraDamage = damage * (criticalPower.GetValue() / 100);
        damage += extraDamage;
        return damage;
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
}