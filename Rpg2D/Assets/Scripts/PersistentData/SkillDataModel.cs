using System.Collections.Generic;
using LitJson;
using UnityEngine.Events;

public class SkillDataModel : JSONDataModel
{
    public List<SkillItemDataModel> itemDataList;

    public SkillDataModel()
    {
        itemDataList = new List<SkillItemDataModel>();
    }

    public override void ParseJSONData(JsonData data, UnityAction callback = null)
    {
        base.ParseJSONData(data);
        if (data == null)
        {
            Debugger.Warning($"[SkillData] jsonData is null in {nameof(ParseJSONData)} | Class: {GetType().Name}");
            callback?.Invoke();
            return;
        }

        itemDataList.Clear();

        for (int i = 0; i < data.Count; i++)
        {
            SkillItemDataModel itemDataModel = new SkillItemDataModel();
            itemDataModel.ParseJSONData(data[i]);
            itemDataList.Add(itemDataModel);
        }

        callback?.Invoke();
    }

    public override JsonData GetJSONData()
    {
        SkillManager skill = SkillManager.Instance;
        JsonData skillData = new JsonData();
        skillData.SetJsonType(JsonType.Array);

        var skillDataList = GameResources.Instance.SkillDataManifest.SkillDataList;
        for (int i = 0; i < skillDataList.Count; i++)
        {
            JsonData skillItemData = new JsonData();
            bool isUnlocked = skill.GetSkillIsUnlocked(skillDataList[i].skillID);
            skillItemData["id"] = skillDataList[i].id;
            skillItemData["isUnlocked"] = isUnlocked ? 1 : 0;
            skillData.Add(skillItemData);
        }

        return skillData;
    }
}