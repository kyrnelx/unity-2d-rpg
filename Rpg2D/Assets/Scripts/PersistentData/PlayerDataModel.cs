using LitJson;
using UnityEngine.Events;

public class PlayerDataModel : JSONDataModel
{
    public override void ParseJSONData(JsonData data, UnityAction callback = null)
    {
        base.ParseJSONData(data);
        if (data == null)
        {
            Debugger.Warning($"[PlayerData] jsonData is null in {nameof(ParseJSONData)} | Class: {GetType().Name}");
            callback?.Invoke();
            return;
        }

        var playerStats = PlayerManager.Instance.player.playerStats;

        if (data.Keys.Contains("currentHealth") && data["currentHealth"] != null)
        {
            int currentHealth;
            int.TryParse(data["currentHealth"].ToString(), out currentHealth);
            playerStats.currentHealth = currentHealth;
        }

        ParseStat(data, "maxHealth", playerStats.maxHealth);

        ParseStat(data, "attackPower", playerStats.attackPower);

        ParseStat(data, "agility", playerStats.agility);

        ParseStat(data, "intelligence", playerStats.intelligence);

        ParseStat(data, "strength", playerStats.strength);

        ParseStat(data, "vitality", playerStats.vitality);

        ParseStat(data, "criticalPower", playerStats.criticalPower);

        ParseStat(data, "criticalChance", playerStats.criticalChance);

        ParseStat(data, "evasion", playerStats.evasion);

        ParseStat(data, "lighting", playerStats.lighting);

        ParseStat(data, "chill", playerStats.chill);

        ParseStat(data, "ignite", playerStats.ignite);

        ParseStat(data, "armor", playerStats.armor);

        ParseStat(data, "magicResistance", playerStats.magicResistance);

        callback?.Invoke();
    }

    public override JsonData GetJSONData()
    {
        base.GetJSONData();

        var playerStats = PlayerManager.Instance.player.playerStats;

        JsonData jsonData = new JsonData();

        // Save currentHealth
        jsonData["currentHealth"] = playerStats.currentHealth;

        // Save maxHealth
        JsonData maxHealthData = new JsonData();
        maxHealthData["baseValue"] = playerStats.maxHealth.baseValue;
        JsonData maxHealthModifiers = new JsonData();
        maxHealthModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.maxHealth.modifiers.Count; i++)
        {
            maxHealthModifiers.Add(playerStats.maxHealth.modifiers[i]);
        }

        maxHealthData["modifiers"] = maxHealthModifiers;
        jsonData["maxHealth"] = maxHealthData;

        // Save attackPower
        JsonData attackPowerData = new JsonData();
        attackPowerData["baseValue"] = playerStats.attackPower.baseValue;
        JsonData attackPowerModifiers = new JsonData();
        attackPowerModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.attackPower.modifiers.Count; i++)
        {
            attackPowerModifiers.Add(playerStats.attackPower.modifiers[i]);
        }

        attackPowerData["modifiers"] = attackPowerModifiers;
        jsonData["attackPower"] = attackPowerData;

        // Save agility
        JsonData agilityData = new JsonData();
        agilityData["baseValue"] = playerStats.agility.baseValue;
        JsonData agilityModifiers = new JsonData();
        agilityModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.agility.modifiers.Count; i++)
        {
            agilityModifiers.Add(playerStats.agility.modifiers[i]);
        }

        agilityData["modifiers"] = agilityModifiers;
        jsonData["agility"] = agilityData;

        // Save intelligence
        JsonData intelligenceData = new JsonData();
        intelligenceData["baseValue"] = playerStats.intelligence.baseValue;
        JsonData intelligenceModifiers = new JsonData();
        intelligenceModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.intelligence.modifiers.Count; i++)
        {
            intelligenceModifiers.Add(playerStats.intelligence.modifiers[i]);
        }

        intelligenceData["modifiers"] = intelligenceModifiers;
        jsonData["intelligence"] = intelligenceData;

        // Save strength
        JsonData strengthData = new JsonData();
        strengthData["baseValue"] = playerStats.strength.baseValue;
        JsonData strengthModifiers = new JsonData();
        strengthModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.strength.modifiers.Count; i++)
        {
            strengthModifiers.Add(playerStats.strength.modifiers[i]);
        }

        strengthData["modifiers"] = strengthModifiers;
        jsonData["strength"] = strengthData;

        // Save vitality
        JsonData vitalityData = new JsonData();
        vitalityData["baseValue"] = playerStats.vitality.baseValue;
        JsonData vitalityModifiers = new JsonData();
        vitalityModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.vitality.modifiers.Count; i++)
        {
            vitalityModifiers.Add(playerStats.vitality.modifiers[i]);
        }

        vitalityData["modifiers"] = vitalityModifiers;
        jsonData["vitality"] = vitalityData;

        // Save criticalPower
        JsonData criticalPowerData = new JsonData();
        criticalPowerData["baseValue"] = playerStats.criticalPower.baseValue;
        JsonData criticalPowerModifiers = new JsonData();
        criticalPowerModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.criticalPower.modifiers.Count; i++)
        {
            criticalPowerModifiers.Add(playerStats.criticalPower.modifiers[i]);
        }

        criticalPowerData["modifiers"] = criticalPowerModifiers;
        jsonData["criticalPower"] = criticalPowerData;

        // Save criticalChance
        JsonData criticalChanceData = new JsonData();
        criticalChanceData["baseValue"] = playerStats.criticalChance.baseValue;
        JsonData criticalChanceModifiers = new JsonData();
        criticalChanceModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.criticalChance.modifiers.Count; i++)
        {
            criticalChanceModifiers.Add(playerStats.criticalChance.modifiers[i]);
        }

        criticalChanceData["modifiers"] = criticalChanceModifiers;
        jsonData["criticalChance"] = criticalChanceData;

        // Save evasion
        JsonData evasionData = new JsonData();
        evasionData["baseValue"] = playerStats.evasion.baseValue;
        JsonData evasionModifiers = new JsonData();
        evasionModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.evasion.modifiers.Count; i++)
        {
            evasionModifiers.Add(playerStats.evasion.modifiers[i]);
        }

        evasionData["modifiers"] = evasionModifiers;
        jsonData["evasion"] = evasionData;

        // Save lighting
        JsonData lightingData = new JsonData();
        lightingData["baseValue"] = playerStats.lighting.baseValue;
        JsonData lightingModifiers = new JsonData();
        lightingModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.lighting.modifiers.Count; i++)
        {
            lightingModifiers.Add(playerStats.lighting.modifiers[i]);
        }

        lightingData["modifiers"] = lightingModifiers;
        jsonData["lighting"] = lightingData;

        // Save chill
        JsonData chillData = new JsonData();
        chillData["baseValue"] = playerStats.chill.baseValue;
        JsonData chillModifiers = new JsonData();
        chillModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.chill.modifiers.Count; i++)
        {
            chillModifiers.Add(playerStats.chill.modifiers[i]);
        }

        chillData["modifiers"] = chillModifiers;
        jsonData["chill"] = chillData;

        // Save ignite
        JsonData igniteData = new JsonData();
        igniteData["baseValue"] = playerStats.ignite.baseValue;
        JsonData igniteModifiers = new JsonData();
        igniteModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.ignite.modifiers.Count; i++)
        {
            igniteModifiers.Add(playerStats.ignite.modifiers[i]);
        }

        igniteData["modifiers"] = igniteModifiers;
        jsonData["ignite"] = igniteData;

        // Save armor
        JsonData armorData = new JsonData();
        armorData["baseValue"] = playerStats.armor.baseValue;
        JsonData armorModifiers = new JsonData();
        armorModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.armor.modifiers.Count; i++)
        {
            armorModifiers.Add(playerStats.armor.modifiers[i]);
        }

        armorData["modifiers"] = armorModifiers;
        jsonData["armor"] = armorData;

        // Save magicResistance
        JsonData magicResistanceData = new JsonData();
        magicResistanceData["baseValue"] = playerStats.magicResistance.baseValue;
        JsonData magicResistanceModifiers = new JsonData();
        magicResistanceModifiers.SetJsonType(LitJson.JsonType.Array);
        for (int i = 0; i < playerStats.magicResistance.modifiers.Count; i++)
        {
            magicResistanceModifiers.Add(playerStats.magicResistance.modifiers[i]);
        }

        magicResistanceData["modifiers"] = magicResistanceModifiers;
        jsonData["magicResistance"] = magicResistanceData;

        return jsonData;
    }

    private void ParseStat(JsonData jsonData, string statName, Stat targetStat)
    {
        if (!jsonData.Keys.Contains(statName) || jsonData[statName] == null) return;

        if (jsonData[statName].Keys.Contains("baseValue") && jsonData[statName]["baseValue"] != null)
        {
            targetStat.SetDefaultValue((int)(jsonData[statName]["baseValue"]));
        }

        if (jsonData[statName].Keys.Contains("modifiers") && jsonData[statName]["modifiers"] != null &&
            jsonData[statName]["modifiers"].Count > 0)
        {
            int modifierLength = jsonData[statName]["modifiers"].Count;
            for (int i = 0; i < modifierLength; i++)
            {
                targetStat.AddModifier((int)(jsonData[statName]["modifiers"][i]));
            }
        }
    }
}