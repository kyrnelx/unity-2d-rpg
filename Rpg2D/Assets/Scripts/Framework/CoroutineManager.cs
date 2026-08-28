using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager m_instance;
    public static CoroutineManager Instance => m_instance;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartWaitForFrame(UnityAction callback)
    {
        StartCoroutine(WaitForFrame(callback));
    }

    private IEnumerator WaitForFrame(UnityAction callback)
    {
        yield return null;
        callback?.Invoke();
    }

    public void StartWaitForFrames(int frames, UnityAction callback)
    {
        StartCoroutine(WaitForFrames(frames, callback));
    }

    private IEnumerator WaitForFrames(int frames, UnityAction callback)
    {
        for (int i = 0; i < frames; i++)
        {
            yield return null;
        }

        callback?.Invoke();
    }

    public void StartWaitForSeconds(float seconds, UnityAction callback)
    {
        StartCoroutine(WaitForSeconds(seconds, callback));
    }

    private IEnumerator WaitForSeconds(float seconds, UnityAction callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }
}