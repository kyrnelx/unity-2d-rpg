using UnityEngine;

/// <summary>
/// 动画事件基类
/// 实体使用状态设计模式禁用此Trigger，在状态类定义本状态Trigger
/// </summary>
public class AnimationEvent : MonoBehaviour
{
    private bool _isTriggered;

    public bool IsTriggered
    {
        get
        {
            if (_isTriggered)
            {
                _isTriggered = false;
                return true;
            }

            return false;
        }
    }

    public void Trigger()
    {
        _isTriggered = true;
    }
}