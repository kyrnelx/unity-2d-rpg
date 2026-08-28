using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourceLoader
{
    private static ResourceLoader m_instance;
    public static ResourceLoader Instance => m_instance ?? (m_instance = new ResourceLoader());

    public T LoadFromResources<T>(string path) where T : Object
    {
        T t = Resources.Load<T>(path);
        if (t == null)
            Debugger.Error($"Failed to load asset at path: {path}. Type: {typeof(T).Name} not found.");
        return t;
    }

    public async void LoadFromResourcesAsync<T>(string path, Action<T> onSuccess, Action<Exception> onError = null)
        where T : Object
    {
        try
        {
            var request = Resources.LoadAsync<T>(path);

            while (!request.isDone)
            {
                await Task.Yield();
            }

            if (request.asset == null || !(request.asset is T))
            {
                throw new Exception($"Failed to load asset at path: {path}. Type: {typeof(T).Name} not found.");
            }

            onSuccess?.Invoke((T)request.asset);
        }
        catch (Exception ex)
        {
            onError?.Invoke(ex);
        }
    }

    public GameObject LoadObjFromResources(string path)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        if (obj == null)
            Debugger.Error($"Failed to load asset at path: {path}. Asset not found.");
        obj = GameObject.Instantiate(obj);
        return obj;
    }

    public GameObject LoadObjFromResources(string path, Vector2 position, Quaternion quaternion)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        if (obj == null)
            Debugger.Error($"Failed to load asset at path: {path}. Asset not found.");
        obj = GameObject.Instantiate(obj, position, quaternion);
        return obj;
    }
}