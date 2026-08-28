using System;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager m_instance;
    public static SkillManager Instance => m_instance;
    public Skill_Roll SkillRoll { get; set; }
    public Skill_Clone SkillClone { get; set; }
    public Skill_IdleBlock SkillIdleBlock { get; set; }
    public Skill_MagicOrb SkillMagicOrb { get; set; }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SkillRoll = GetComponent<Skill_Roll>();
        SkillClone = GetComponent<Skill_Clone>();
        SkillIdleBlock = GetComponent<Skill_IdleBlock>();
        SkillMagicOrb = GetComponent<Skill_MagicOrb>();

        DataManager.Instance.SkillDataModel.ParseJSONData(UpdateByPersistentData);
    }

    public bool GetSkillIsUnlocked(SkillID id)
    {
        Skill skill;
        switch (id)
        {
            case SkillID.Roll:
                skill = SkillRoll;
                break;
            case SkillID.IdleBlock:
                skill = SkillIdleBlock;
                break;
            case SkillID.Clone:
                skill = SkillClone;
                break;
            case SkillID.MagicOrb:
                skill = SkillMagicOrb;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id), id, null);
        }

        return skill.isUnlocked;
    }

    public Skill GetSKill(SkillID id)
    {
        Skill skill;
        switch (id)
        {
            case SkillID.Roll:
                skill = SkillRoll;
                break;
            case SkillID.IdleBlock:
                skill = SkillIdleBlock;
                break;
            case SkillID.Clone:
                skill = SkillClone;
                break;
            case SkillID.MagicOrb:
                skill = SkillMagicOrb;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(id), id, null);
        }

        return skill;
    }

    public bool CanUnlockSkill(SkillID skillID)
    {
        SkillData skillData = DataManager.Instance.LoadSkillData(skillID);
        SkillID[] skillIds = skillData.unlockCondition;
        foreach (var skillId in skillIds)
        {
            switch (skillId)
            {
                case SkillID.Roll:
                    if (SkillRoll.isUnlocked == false)
                    {
                        return false;
                    }

                    break;
                case SkillID.IdleBlock:
                    if (SkillIdleBlock.isUnlocked == false)
                    {
                        return false;
                    }

                    break;
                case SkillID.Clone:
                    if (SkillClone.isUnlocked == false)
                    {
                        return false;
                    }

                    break;
                case SkillID.MagicOrb:
                    if (SkillMagicOrb.isUnlocked == false)
                    {
                        return false;
                    }

                    break;
            }
        }

        return true;
    }

    public void UpdateByPersistentData()
    {
        SkillDataModel skillDataModel = DataManager.Instance.SkillDataModel;
        var skillItemDataList = skillDataModel.itemDataList;
        for (int i = 0; i < skillItemDataList.Count; i++)
        {
            var itemDataModel = skillItemDataList[i];
            var skillData = DataManager.Instance.LoadSkillData(itemDataModel.id);
            Skill skill = GetSKill(skillData.skillID);
            skill.isUnlocked = itemDataModel.isUnlocked;
        }

        var skillDataManifest = GameResources.Instance.SkillDataManifest.SkillDataList;
        for (int i = 0; i < skillDataManifest.Count; i++)
        {
            switch (skillDataManifest[i].skillID)
            {
                case SkillID.Roll:
                    SkillRoll.price = skillDataManifest[i].price;
                    break;
                case SkillID.IdleBlock:
                    SkillIdleBlock.price = skillDataManifest[i].price;
                    break;
                case SkillID.Clone:
                    SkillClone.price = skillDataManifest[i].price;
                    break;
                case SkillID.MagicOrb:
                    SkillMagicOrb.price = skillDataManifest[i].price;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}