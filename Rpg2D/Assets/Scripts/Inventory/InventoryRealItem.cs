using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryRealItem : MonoBehaviour
{
    public InventoryItemBaseData itemData;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Pop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "player")
        {
            EventSubscriber.OnInventoryRealItemPickup?.Invoke(itemData);
            Destroy(gameObject);
        }
    }

    public void Pop()
    {
        _rb.velocity = new Vector2(Random.Range(-5f, 5f), Random.Range(16, 20f));
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (itemData != null) gameObject.name = itemData.name;
    }
#endif
}