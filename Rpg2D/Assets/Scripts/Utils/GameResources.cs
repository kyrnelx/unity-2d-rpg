using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
                DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }

    [Header("Audio")] public AudioDataManifest AudioDataManifest;
    [Header("Inventory")] public InventoryDataManifest InventoryDataManifest;
    [Header("Skill")] public SkillDataManifest SkillDataManifest;
    [Header("Enemy")] public EnemyDataManifest EnemyDataManifest;
    [Header("Game Config")] public GameConfigData GameConfigData;
    [Header("Character Data")] public CharacterData CharacterData;
}