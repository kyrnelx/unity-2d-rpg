using System.Collections.Generic;

/// <summary>
/// 属性基本单位
/// </summary>
[System.Serializable]
public class Stat
{
    public int baseValue;
    public List<int> modifiers;

    public int GetValue()
    {
        int finalValue = baseValue;
        foreach (var modifier in modifiers) finalValue += modifier;
        return finalValue;
    }

    public int SetDefaultValue(int value) => baseValue = value;

    public void AddModifier(int modifier) => modifiers.Add(modifier);

    public void RemoveModifier(int modifier) => modifiers.Remove(modifier);
}