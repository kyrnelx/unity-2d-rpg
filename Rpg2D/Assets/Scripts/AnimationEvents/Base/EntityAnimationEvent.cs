using UnityEngine;

/// <summary>
/// 实体状态基类
/// 兼容状态设计模式
/// </summary>
public class EntityAnimationEvent : MonoBehaviour
{
    protected Entity entity;

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    public virtual void Trigger()
    {
        entity.stateMachine.currentState.triggered = true;
    }

    public virtual bool IsTriggered()
    {
        return entity.stateMachine.currentState.triggered;
    }
}