using UnityEngine;

public class GameSceneGenerator : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private Transform _playerSpawnPoint;

    private void Awake()
    {
        _playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").transform;
    }

    private void Start()
    {
        Instantiate(playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
    }
}