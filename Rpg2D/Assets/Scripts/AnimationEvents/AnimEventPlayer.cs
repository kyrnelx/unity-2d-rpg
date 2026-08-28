using System.Collections;
using UnityEngine;

public class AnimEventPlayer : EntityAnimationEvent
{
    public LayerMask attackLayer;
    public Transform attackPoint;

    [SerializeField] private GameObject hitFxPrefab;

    private Player _player;
    private ObjectPool<GameObject> _hitFxPool = new ObjectPool<GameObject>();
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.44f);

    protected override void Start()
    {
        base.Awake();
        _player = PlayerManager.Instance.player;
        EventSubscriber.PlayFleshVFX += PlayHitVFX;
    }

    public void Attack1()
    {
        Attack(0);
    }

    public void Attack2()
    {
        Attack(1);
    }

    public void Attack3()
    {
        Attack(2);
    }

    private void Attack(int rangeIndex)
    {
        EventSubscriber.PlayerAttack?.Invoke(attackPoint);

        // 施加轻微攻击移动力
        _player.SetVelocity(new Vector2(_player.attackSlightForce[rangeIndex] * _player.facingDir,
            _player.Velocity.y));

        Collider2D[] cds =
            Physics2D.OverlapCircleAll(_player.attackPoint.position, _player.attackRangeArray[rangeIndex], attackLayer);
        foreach (var cd in cds)
        {
            Entity target = cd.GetComponent<Entity>();
            if (target == null) continue;

            _player.CinemachineImpulseSource.GenerateImpulse();
            PlayHitVFX(target);

            SoundManager.Instance.PlaySwordFleshSfx();
            target.Knockback(_player.knockbackForce);
            _player.playerStats.DoDamage(target.entityStats);
        }
    }

    private void PlayHitVFX(Entity target)
    {
        GameObject hitFx = _hitFxPool.Get();
        if (hitFx == null)
        {
            hitFx = Instantiate(hitFxPrefab);
        }

        hitFx.transform.rotation = Mathf.Approximately(_player.facingDir, 1)
            ? Quaternion.Euler(0, 0, 0)
            : Quaternion.Euler(0, 180, 0);
        hitFx.transform.position = target.transform.position;
        CoroutineManager.Instance.StartCoroutine(WaitForRecycle(hitFx));
    }

    private IEnumerator WaitForRecycle(GameObject fx)
    {
        yield return _waitForSeconds;
        fx.transform.position += Vector3.up * 100f;
        _hitFxPool.Set(fx);
    }

    private void OnDestroy()
    {
        EventSubscriber.PlayFleshVFX -= PlayHitVFX;
        while (_hitFxPool.pool.Count > 0)
        {
            var item = _hitFxPool.pool.Dequeue();
            Destroy(item);
        }
    }
}