using UnityEngine;

public class PlayerFX : EntityFX
{
    [SerializeField] private GameObject fireFxPrefab;
    [SerializeField] private GameObject iceFxPrefab;
    [SerializeField] private GameObject lightingFxPrefab;
    private Player _player;
    private ObjectPool<ElementAttackFX> _iceFxPool = new ObjectPool<ElementAttackFX>();
    private ObjectPool<ElementAttackFX> _lightingFxPool = new ObjectPool<ElementAttackFX>();
    private ObjectPool<ElementAttackFX> _fireFxPool = new ObjectPool<ElementAttackFX>();

    protected override void Awake()
    {
        base.Awake();
        _player = GetComponent<Player>();
        EventSubscriber.PlayerAttack = null;
        EventSubscriber.PlayerAttack += PlayAttackFX;
    }

    public void PlayAttackFX(Transform transform)
    {
        if (InventoryManager.Instance.currentWeaponData == null) return;

        if (InventoryManager.Instance.currentWeaponData.equipmentID == InventoryEquipmentID.FlameSword)
        {
            ElementAttackFX fireFx = _fireFxPool.Get();
            if (fireFx == null)
            {
                fireFx = Instantiate(fireFxPrefab).GetComponent<ElementAttackFX>();
                fireFx.callback += _fireFxPool.Set;
            }

            fireFx.PlayFX(_player.facingDir, transform.position);
        }
        else if (InventoryManager.Instance.currentWeaponData.equipmentID == InventoryEquipmentID.IceSword)
        {
            ElementAttackFX iceFx = _iceFxPool.Get();
            if (iceFx == null)
            {
                iceFx = Instantiate(iceFxPrefab).GetComponent<ElementAttackFX>();
                iceFx.callback += _iceFxPool.Set;
            }

            iceFx.PlayFX(_player.facingDir, transform.position);
        }
        else if (InventoryManager.Instance.currentWeaponData.equipmentID == InventoryEquipmentID.ThunderClaw)
        {
            ElementAttackFX lightingFx = _lightingFxPool.Get();
            if (lightingFx == null)
            {
                lightingFx = Instantiate(lightingFxPrefab).GetComponent<ElementAttackFX>();
                lightingFx.callback += _lightingFxPool.Set;
            }

            lightingFx.PlayFX(_player.facingDir, transform.position);
        }
    }

    ~PlayerFX()
    {
        while (_fireFxPool.pool.Count > 0)
        {
            var fx = _fireFxPool.pool.Dequeue();
            fx.StopFxCoroutine();
            Destroy(fx.gameObject);
        }

        _fireFxPool.pool.Clear();

        while (_iceFxPool.pool.Count > 0)
        {
            var fx = _iceFxPool.pool.Dequeue();
            fx.StopFxCoroutine();
            Destroy(fx.gameObject);
        }

        _iceFxPool.pool.Clear();

        while (_lightingFxPool.pool.Count > 0)
        {
            var fx = _lightingFxPool.pool.Dequeue();
            fx.StopFxCoroutine();
            Destroy(fx.gameObject);
        }

        _lightingFxPool.pool.Clear();

        EventSubscriber.PlayerAttack -= PlayAttackFX;
    }
}