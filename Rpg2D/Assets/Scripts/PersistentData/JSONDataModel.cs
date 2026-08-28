using LitJson;
using UnityEngine.Events;

public abstract class JSONDataModel
{
    public JsonData JSONData { get; set; }

    public void SetJSONData(JsonData data)
    {
        JSONData = data;
    }

    public void ParseJSONData(UnityAction callback = null)
    {
        ParseJSONData(JSONData, callback);
    }

    public virtual void ParseJSONData(JsonData data, UnityAction callback = null)
    {
        JSONData = data;
    }

    public virtual JsonData GetJSONData()
    {
        return null;
    }
}