using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Database/Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public string id;
    public int price;
    public SkillID skillID;
    public SkillID[] unlockCondition;

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if (id == "0" || string.IsNullOrEmpty(id))
        {
            id = Guid.NewGuid().ToString();
        }
    }
#endif
}