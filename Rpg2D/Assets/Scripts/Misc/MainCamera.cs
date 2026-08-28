using Cinemachine;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Awake()
    {
        _cinemachineVirtualCamera = transform.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        CoroutineManager.Instance.StartWaitForFrame(() =>
        {
            _cinemachineVirtualCamera.Follow = PlayerManager.Instance.player.transform;
        });
    }
}