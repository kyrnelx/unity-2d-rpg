using System;

/// <summary>
/// 玩家属性
/// </summary>
public class PlayerStats : AlmightyStats, IDisposable
{
    protected override void Awake()
    {
        base.Awake();
        EventSubscriber.Equip += Equip;
        EventSubscriber.UnEquip += UnEquip;
    }

    /// <summary>
    /// 监听背包装备行为，修改玩家属性
    /// </summary>
    /// <param name="itemData"></param>
    private void Equip(InventoryItemBaseData itemData)
    {
        if (itemData is InventoryItemStatData == false) return;
        var statData = itemData as InventoryItemStatData;
        if (statData.maxHealth > 0) maxHealth.AddModifier(statData.maxHealth);
        if (statData.attackPower > 0) attackPower.AddModifier(statData.attackPower);
        if (statData.armor > 0) armor.AddModifier(statData.armor);
        if (statData.magicResistance > 0) magicResistance.AddModifier(statData.magicResistance);
        if (statData.agility > 0) agility.AddModifier(statData.agility);
        if (statData.intelligence > 0) intelligence.AddModifier(statData.intelligence);
        if (statData.strength > 0) strength.AddModifier(statData.strength);
        if (statData.vitality > 0) vitality.AddModifier(statData.vitality);
        if (statData.criticalPower > 0) criticalPower.AddModifier(statData.criticalPower);
        if (statData.criticalChance > 0) criticalChance.AddModifier(statData.criticalChance);
        if (statData.evasion > 0) evasion.AddModifier(statData.evasion);
        if (statData.lighting > 0) lighting.AddModifier(statData.lighting);
        if (statData.chill > 0) chill.AddModifier(statData.chill);
        if (statData.ignite > 0) ignite.AddModifier(statData.ignite);
    }

    /// <summary>
    /// 监听背包卸下行为，修改玩家属性
    /// </summary>
    /// <param name="itemData"></param>
    private void UnEquip(InventoryItemBaseData itemData)
    {
        if (itemData is InventoryItemStatData == false) return;
        var statData = itemData as InventoryItemStatData;
        if (statData.maxHealth > 0) maxHealth.RemoveModifier(statData.maxHealth);
        if (statData.attackPower > 0) attackPower.RemoveModifier(statData.attackPower);
        if (statData.armor > 0) armor.RemoveModifier(statData.armor);
        if (statData.magicResistance > 0) magicResistance.RemoveModifier(statData.magicResistance);
        if (statData.agility > 0) agility.RemoveModifier(statData.agility);
        if (statData.intelligence > 0) intelligence.RemoveModifier(statData.intelligence);
        if (statData.strength > 0) strength.RemoveModifier(statData.strength);
        if (statData.vitality > 0) vitality.RemoveModifier(statData.vitality);
        if (statData.criticalPower > 0) criticalPower.RemoveModifier(statData.criticalPower);
        if (statData.criticalChance > 0) criticalChance.RemoveModifier(statData.criticalChance);
        if (statData.evasion > 0) evasion.RemoveModifier(statData.evasion);
        if (statData.lighting > 0) lighting.RemoveModifier(statData.lighting);
        if (statData.chill > 0) chill.RemoveModifier(statData.chill);
        if (statData.ignite > 0) ignite.RemoveModifier(statData.ignite);
    }

    public override void TakeDamage(int damage)
    {
        if (isDead) return;
        if (isInvincible) return;
        base.TakeDamage(damage);
        EventSubscriber.OnPlayerHealthChange?.Invoke((float)currentHealth / maxHealth.GetValue());
    }

    public void Dispose()
    {
        EventSubscriber.Equip -= Equip;
        EventSubscriber.UnEquip -= UnEquip;
    }
}