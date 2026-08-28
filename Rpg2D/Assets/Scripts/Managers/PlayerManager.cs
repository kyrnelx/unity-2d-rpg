using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager m_instance;
    public static PlayerManager Instance => m_instance;

    public Player player;
    public bool IsPlayerDead { get; set; }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Initialize()
    {
        player = FindObjectOfType<Player>();
        DataManager.Instance.PlayerDataModel.ParseJSONData(UpdateByPersistentData);
    }

    public void UpdateByPersistentData()
    {
        if (GameManager.Instance.ResetPlayerHealth)
        {
            GameManager.Instance.ResetPlayerHealth = false;
            player.playerStats.currentHealth = GameResources.Instance.CharacterData.maxHealth;
            EventSubscriber.OnPlayerHealthChange?.Invoke(1);
        }
        else
        {
            float percentage = (float)player.playerStats.currentHealth / player.playerStats.maxHealth.GetValue();
            EventSubscriber.OnPlayerHealthChange?.Invoke(percentage);
        }
    }
}