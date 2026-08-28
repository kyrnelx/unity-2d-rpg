using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour, IDataProvider
{
    private static DataManager instance;
    public static DataManager Instance => instance;

    public GameDataModel GameDataModel { get; set; } = new GameDataModel();
    public PlayerDataModel PlayerDataModel { get; set; } = new PlayerDataModel();
    public InventoryDataModel InventoryDataModel { get; set; } = new InventoryDataModel();
    public SkillDataModel SkillDataModel { get; set; } = new SkillDataModel();

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetJSONData()
    {
        PlayerDataModel.SetJSONData(PlayerDataModel.GetJSONData());
        InventoryDataModel.SetJSONData(InventoryDataModel.GetJSONData());
        SkillDataModel.SetJSONData(SkillDataModel.GetJSONData());
    }

    public int Coin
    {
        get => GameDataModel.coin;
        set => GameDataModel.coin = value;
    }

    public float SoundVolume
    {
        get => GameDataModel.soundVolume;
        set => GameDataModel.soundVolume = value;
    }

    public float MusicVolume
    {
        get => GameDataModel.musicVolume;
        set => GameDataModel.musicVolume = value;
    }

    public void ClearJSONData()
    {
        SkillDataModel.SetJSONData(null);
        InventoryDataModel.SetJSONData(null);
        PlayerDataModel.SetJSONData(null);
        GameDataModel.coin = 0;
    }

    public void ClearCacheData()
    {
        GameDataModel.ResetCoin();
        ClearJSONData();
        InventoryManager.Instance.UpdateByPersistentData();
        SkillManager.Instance.UpdateByPersistentData();
        PlayerManager.Instance.UpdateByPersistentData();
    }

    public void DeleteFile()
    {
        GameDataModel.ResetData();

        if (UnityHelper.IsWebGL())
        {
            return;
        }

        string playerDataPath = Application.persistentDataPath + "/" + "PlayerData" + ".json";
        if (System.IO.File.Exists(playerDataPath))
        {
            System.IO.File.Delete(playerDataPath);
        }

        string inventoryDataPath = Application.persistentDataPath + "/" + "InventoryData" + ".json";
        if (System.IO.File.Exists(inventoryDataPath))
        {
            System.IO.File.Delete(inventoryDataPath);
        }

        string skillDataPath = Application.persistentDataPath + "/" + "SkillData" + ".json";
        if (System.IO.File.Exists(skillDataPath))
        {
            System.IO.File.Delete(skillDataPath);
        }
    }

    public void SaveGameData(UnityAction callback = null)
    {
        GameDataModel.Save();

        if (UnityHelper.IsWebGL())
        {
            callback?.Invoke();
            return;
        }

        SavePlayerData(() =>
        {
            Debugger.Info(
                $"[GameDataManager] Save Player Data Successfully | Class: {GetType().Name}");
            SaveInventoryData(() =>
            {
                Debugger.Info(
                    $"[GameDataManager] Save Inventory Data Successfully | Class: {GetType().Name}");
                SaveSkillData(() =>
                {
                    Debugger.Info(
                        $"[GameDataManager] Save Skill Data Successfully | Class: {GetType().Name}");
                    callback?.Invoke();
                });
            });
        });
    }

    public void LoadGameData(UnityAction callback = null)
    {
        GameDataModel.Load();

        if (UnityHelper.IsWebGL())
        {
            callback?.Invoke();
            return;
        }

        LoadPlayerData(() =>
        {
            Debugger.Info(
                $"[GameDataManager] Load Player Data Successfully | Class: {GetType().Name}");
            LoadInventoryData(() =>
            {
                Debugger.Info(
                    $"[GameDataManager] Load Inventory Data Successfully | Class: {GetType().Name}");
                LoadSkillData(() =>
                {
                    Debugger.Info(
                        $"[GameDataManager] Load Skill Data Successfully | Class: {GetType().Name}");
                    callback?.Invoke();
                });
            });
        });
    }

    public void LoadPlayerData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("PlayerData", data =>
        {
            PlayerDataModel.SetJSONData(data);
            callback?.Invoke();
        });
    }

    public void SavePlayerData(UnityAction callback = null)
    {
        if (PlayerDataModel.JSONData == null)
        {
            callback?.Invoke();
            Debugger.Warning(
                "PlayerDataModel.JSONData is null. Please check if the JSON file is loaded correctly or if the data model is initialized.");
            return;
        }

        JSONManager.Instance.SaveJsonDataAsync(PlayerDataModel.JSONData.ToJson(), "PlayerData", callback);
    }

    public void LoadInventoryData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("InventoryData", data =>
        {
            InventoryDataModel.SetJSONData(data);
            callback?.Invoke();
        });
    }

    public void SaveInventoryData(UnityAction callback = null)
    {
        if (InventoryDataModel.JSONData == null)
        {
            callback?.Invoke();
            Debugger.Warning(
                "InventoryDataModel.JSONData is null. Please check if the JSON file is loaded correctly or if the data model is initialized.");
            return;
        }

        JSONManager.Instance.SaveJsonDataAsync(InventoryDataModel.JSONData.ToJson(), "InventoryData", callback);
    }

    public void LoadSkillData(UnityAction callback = null)
    {
        JSONManager.Instance.LoadJsonDataAsync("SkillData", data =>
        {
            SkillDataModel.SetJSONData(data);
            callback?.Invoke();
        });
    }

    public void SaveSkillData(UnityAction callback = null)
    {
        if (SkillDataModel.JSONData == null)
        {
            callback?.Invoke();
            Debugger.Warning(
                "SkillDataModel.JSONData is null. Please check if the JSON file is loaded correctly or if the data model is initialized.");
            return;
        }

        JSONManager.Instance.SaveJsonDataAsync(SkillDataModel.JSONData.ToJson(), "SkillData", callback);
    }

    public SkillData LoadSkillData(SkillID skillID)
    {
        foreach (var skillData in GameResources.Instance.SkillDataManifest.SkillDataList)
        {
            if (skillID == skillData.skillID)
            {
                return skillData;
            }
        }

        Debugger.Warning(
            $"[GameDataManager] SkillData not found in {nameof(LoadSkillData)} | Class: {GetType().Name}");
        return null;
    }

    public SkillData LoadSkillData(string guid)
    {
        var skillData = GameResources.Instance.SkillDataManifest.SkillDataList;
        for (int i = 0; i < skillData.Count; i++)
        {
            if (guid == skillData[i].id)
            {
                return skillData[i];
            }
        }

        return null;
    }

    public AudioData LoadAudioData(AudioID audioID)
    {
        AudioData audioData = GameResources.Instance.AudioDataManifest.audioDataList.Find(x => x.audioID == audioID);
        if (audioData == null)
        {
            Debugger.Error($"AudioData not found for audioID: {audioID}. " +
                           $"Available audioIDs: {string.Join(", ", GameResources.Instance.AudioDataManifest.audioDataList.Select(x => x.audioID))}");
        }

        return audioData;
    }

    public InventoryItemBaseData LoadInventoryItemData(string guid)
    {
        List<InventoryItemBaseData> configurationData = GameResources.Instance.InventoryDataManifest.equipmentDataList;

        foreach (var itemData in configurationData)
        {
            if (guid == itemData.id)
            {
                return itemData;
            }
        }

        Debugger.Error($"[Inventory Load Data Error] 无法找到物品: {guid}");
        return null;
    }

    public EnemyData LoadEnemyData(EnemyID enemyID)
    {
        EnemyDataManifest enemyDataManifest = GameResources.Instance.EnemyDataManifest;
        EnemyData enemyData = enemyDataManifest.enemyDataList.Find(x => x.enemyID == enemyID);

        if (enemyData == null)
        {
            string availableIDs = string.Join(", ", enemyDataManifest.enemyDataList.Select(x => x.enemyID.ToString()));
            Debugger.Error($"EnemyData not found for enemyID: {enemyID}. Available enemyIDs: {availableIDs}");
            return null;
        }

        return enemyData;
    }

    public GameConfigData GetGameConfigData()
    {
        return GameResources.Instance.GameConfigData;
    }
}