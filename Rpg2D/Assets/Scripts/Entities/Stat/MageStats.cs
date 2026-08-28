/// <summary>
/// 法师属性
/// </summary>
public class MageStats : EntityStats
{
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
        if (damage > 0) target.TakeDamage(damage);

        int magicResistance = target.magicResistance.GetValue();
        if (target.isShocked) magicResistance = (int)(magicResistance * 0.8f);
        int magicDamage = CalculateMagicDamage() - magicResistance;
        if (magicDamage > 0) target.TakeDamage(magicDamage);
    }

    public bool CanChill()
    {
        int totalChance = chill.GetValue();
        if (GetRandomValue() < totalChance) return true;
        return false;
    }

    public bool CanIgnite()
    {
        int totalChance = ignite.GetValue();
        if (GetRandomValue() < totalChance) return true;
        return false;
    }

    public bool CanLighting()
    {
        int totalChance = lighting.GetValue();
        if (GetRandomValue() < totalChance) return true;
        return false;
    }

    public bool CanEvasion()
    {
        int totalChance = evasion.GetValue();
        if (GetRandomValue() < totalChance) return true;
        return false;
    }

    public int CalculateDamage()
    {
        int totalDamage = attackPower.GetValue();
        return totalDamage;
    }

    public int CalculateMagicDamage()
    {
        int totalDamage = magicPower.GetValue();
        return totalDamage;
    }
}