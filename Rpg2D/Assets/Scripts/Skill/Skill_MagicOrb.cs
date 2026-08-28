using System.Collections;
using UnityEngine;

public class Skill_MagicOrb : Skill
{
    [SerializeField] private GameObject magicOrbPrefab;
    [SerializeField] private float amount = 5f;
    [SerializeField] private float generateInterval = 0.15f;

    [SerializeField] private float atkCooldown = 0.25f;
    [SerializeField] private float orbitSpeed = 360f;
    [SerializeField] private float orbitRadius = 2f;
    [SerializeField] private float duration = 7.5f;

    private ObjectPool<MagicOrbController> _magicOrbPool;
    private WaitForSeconds _generationCooldown;

    private void Awake()
    {
        _magicOrbPool = new ObjectPool<MagicOrbController>();
        _generationCooldown = new WaitForSeconds(generateInterval);
    }

    public override void Release(params object[] parameters)
    {
        base.Release(parameters);
        CoroutineManager.Instance.StartCoroutine(GenerateMagicOrb());
    }

    private IEnumerator GenerateMagicOrb()
    {
        for (int i = 0; i < amount; i++)
        {
            yield return _generationCooldown;
            MagicOrbController controller = _magicOrbPool.Get();
            if (controller == null)
            {
                controller = Instantiate(magicOrbPrefab).GetComponent<MagicOrbController>();
                controller.callback = _magicOrbPool.Set;
            }

            controller.gameObject.SetActive(true);
            controller.Setup(atkCooldown, orbitSpeed, orbitRadius, duration);
            controller.Release();
        }
    }

    ~Skill_MagicOrb()
    {
        while (_magicOrbPool.pool.Count > 0)
        {
            Destroy(_magicOrbPool.pool.Dequeue().gameObject);
        }

        _magicOrbPool.pool.Clear();
    }
}