using LitJson;
using UnityEngine.Events;

public class SkillItemDataModel : JSONDataModel
{
    public string id;
    public bool isUnlocked;

    public override void ParseJSONData(JsonData data, UnityAction callback = null)
    {
        base.ParseJSONData(data, callback);
        if (data == null)
        {
            Debugger.Warning($"{GetType().Name} {nameof(ParseJSONData)} The data is null.");
            callback?.Invoke();
            return;
        }

        if (data.Keys.Contains("id") && data["id"] != null)
        {
            id = data["id"].ToString();
        }

        if (data.Keys.Contains("isUnlocked") && data["isUnlocked"] != null)
        {
            isUnlocked = (int)data["isUnlocked"] == 1;
        }

        callback?.Invoke();
    }
}