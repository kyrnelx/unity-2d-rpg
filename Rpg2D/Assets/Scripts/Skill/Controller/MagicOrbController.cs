using System;
using UnityEngine;
using UnityEngine.Events;

public class MagicOrbController : MonoBehaviour
{
    public UnityAction<MagicOrbController> callback;

    [SerializeField] private float atkCooldown = 0.25f;
    [SerializeField] private float orbitSpeed = 360f;
    [SerializeField] private float orbitRadius = 2f;
    [SerializeField] private float duration = 7.5f;

    private Player _player;
    private float _atkTimer;
    private float _lifeTimer;
    private float _currentAngle;

    public void Setup(float atkCooldown, float orbitSpeed, float orbitRadius, float duration)
    {
        this.atkCooldown = atkCooldown;
        this.orbitSpeed = orbitSpeed;
        this.orbitRadius = orbitRadius;
        this.duration = duration;

        _lifeTimer = 0;
    }

    public void Release()
    {
        _player = PlayerManager.Instance.player;
        Vector3 initialDirection = transform.position - _player.transform.position + Vector3.up * 1.25f;
        _currentAngle = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        if (_player == null) return;

        _lifeTimer += Time.deltaTime;
        _atkTimer += Time.deltaTime;

        _currentAngle += orbitSpeed * Time.deltaTime;

        float radians = _currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(
            Mathf.Cos(radians) * orbitRadius,
            Mathf.Sin(radians) * orbitRadius + 1.25f,
            0);

        transform.position = _player.transform.position + offset;
        transform.up = offset.normalized;

        if (_lifeTimer > duration)
        {
            callback?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_atkTimer < atkCooldown) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            _atkTimer = 0;
            PlayerManager.Instance.player.playerStats.DoDamage(enemy.entityStats);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_atkTimer < atkCooldown) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            _atkTimer = 0;
            PlayerManager.Instance.player.playerStats.DoDamage(enemy.entityStats);
        }
    }
}