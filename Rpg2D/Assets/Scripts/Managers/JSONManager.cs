using LitJson;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class JSONManager
{
    private static JSONManager m_instance;
    public static JSONManager Instance => m_instance ?? (m_instance = new JSONManager());

    public async Task<JsonData> LoadJsonDataAsync(string path, UnityAction<JsonData> callback = null)
    {
        string finalPath = Application.streamingAssetsPath + "/" + path + ".json";
        if (File.Exists(finalPath) == false) finalPath = Application.persistentDataPath + "/" + path + ".json";
        if (File.Exists(finalPath) == false)
        {
            Debugger.Warning("Json file not found ->" + path);
            callback?.Invoke(null);
            return null;
        }

        string jsonStr = await File.ReadAllTextAsync(finalPath);
        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        callback?.Invoke(jsonData);
        return jsonData;
    }

    public async Task SaveJsonDataAsync(string data, string path, UnityAction callback = null)
    {
        string finalPath = Application.persistentDataPath + "/" + path + ".json";
        string jsonStr = data;
        await File.WriteAllTextAsync(finalPath, jsonStr);
        callback?.Invoke();
    }
}