using UnityEngine;

public static class UnityHelper
{
    public static void SetParent(Transform child, Transform parent, bool isStay = false)
    {
        child.SetParent(parent, isStay);
    }

    public static bool IsWebGL()
    {
        return Application.platform == RuntimePlatform.WebGLPlayer;
    }
}