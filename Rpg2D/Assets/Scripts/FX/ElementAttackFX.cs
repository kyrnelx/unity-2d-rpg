using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ElementAttackFX : MonoBehaviour
{
    public UnityAction<ElementAttackFX> callback;
    public float waitForSetTime = 1;
    public ElementStatusType fxType;

    private Coroutine _coroutine;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void StopFxCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    public void PlayFX(float facingDir, Vector3 position)
    {
        transform.position = position + Vector3.right * facingDir;
        transform.rotation = facingDir == 1 ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(new Vector3(0, 180, 0));
        if (fxType == ElementStatusType.Ignite) gameObject.transform.position += Vector3.up * 2f;

        _animator.SetBool("Fx", true);
        _coroutine = CoroutineManager.Instance.StartCoroutine(WaitForSet());
    }

    private IEnumerator WaitForSet()
    {
        yield return new WaitForSeconds(waitForSetTime);
        _animator?.SetBool("Fx", false);
        callback?.Invoke(this);
        _coroutine = null;
    }

    private void OnDestroy()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}