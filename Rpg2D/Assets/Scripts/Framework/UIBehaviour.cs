using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class UIBehaviour : IDisposable
{
    public GameObject DisplayObject;
    public Transform DisplayTransform;
    public RectTransform DisplayRectTransform;
    public Action<string, object> UIEvent;

    public void SetDisplayObject(GameObject obj)
    {
        DisplayObject = obj;
        DisplayTransform = obj.transform;
        DisplayRectTransform = obj.GetComponent<RectTransform>();
        ParseComponent();
        AddEvent();
    }

    public virtual void Refresh(params object[] args)
    {
    }

    public virtual void Show()
    {
        DisplayObject.SetActive(true);
    }

    public virtual void Hide()
    {
        DisplayObject.SetActive(false);
    }

    public void AddUIEvent(Action<string, object> uiEvent)
    {
        UIEvent += uiEvent;
    }

    public void RemoveUIEvent(Action<string, object> uiEvent)
    {
        UIEvent -= uiEvent;
    }

    protected void NotifyViewEvent(string evtType, object data = null)
    {
        UIEvent?.Invoke(evtType, data);
    }


    protected Transform Find(string path)
    {
        Transform target = DisplayTransform.Find(path);
        if (target == null)
            Debugger.Error($"[UIBehaviour] Failed to find transform at path '{path}'");
        return target;
    }

    protected GameObject FindGameObject(string path)
    {
        GameObject target = DisplayTransform.Find(path).gameObject;
        if (target == null)
            Debugger.Error($"[UIBehaviour] Failed to find transform at path '{path}'");
        return target;
    }

    protected T FindComponent<T>(string path)
    {
        T t = DisplayTransform.Find(path).GetComponent<T>();
        if (t == null)
            Debugger.Error($"[UIBehaviour] Failed to find component of type '{typeof(T).Name}' at path '{path}'");
        return t;
    }

    protected void RegisterButtonEvent(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    protected void UnRegisterButtonEvent(Button button, UnityAction action)
    {
        button.onClick.RemoveListener(action);
    }

    protected abstract void ParseComponent();

    protected virtual void AddEvent()
    {
    }

    protected virtual void RemoveEvent()
    {
    }

    public virtual void Dispose()
    {
        RemoveEvent();
        UnityEngine.Object.DestroyImmediate(DisplayObject);
        DisplayObject = null;
        DisplayTransform = null;
        DisplayRectTransform = null;
        UIEvent = null;
    }
}