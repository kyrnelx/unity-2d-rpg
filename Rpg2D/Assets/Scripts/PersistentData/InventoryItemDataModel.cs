using LitJson;
using UnityEngine.Events;

public class InventoryItemDataModel : JSONDataModel
{
    public string id;
    public int number;

    public override void ParseJSONData(JsonData data, UnityAction callback = null)
    {
        base.ParseJSONData(data, callback);
        if (data == null)
        {
            Debugger.Warning(
                $"[InventoryItemDataModel] jsonData is null in {nameof(ParseJSONData)} | Class: {GetType().Name}");
            callback?.Invoke();
            return;
        }

        if (data.Keys.Contains("guid") && data["guid"] != null)
        {
            id = data["guid"].ToString();
        }

        if (data.Keys.Contains("number") && data["number"] != null)
        {
            number = int.Parse(data["number"].ToString());
        }

        callback?.Invoke();
    }
}