using System.Collections.Generic;
using LitJson;
using UnityEngine.Events;

public class InventoryDataModel : JSONDataModel
{
    public string currentWeaponID;
    public List<InventoryItemDataModel> equipmentDataList;

    public InventoryDataModel()
    {
        equipmentDataList = new List<InventoryItemDataModel>();
    }

    public override void ParseJSONData(JsonData data, UnityAction callback = null)
    {
        if (data == null)
        {
            Debugger.Warning($"[InventoryData] jsonData is null in {nameof(ParseJSONData)} | Class: {GetType().Name}");
            callback?.Invoke();
            return;
        }

        // 清除数据
        currentWeaponID = null;
        equipmentDataList.Clear();

        // 加载装备的武器
        if (data.Keys.Contains("weapon") && data["weapon"] != null && data["weapon"].ToString() != "")
        {
            currentWeaponID = data["weapon"].ToString();
        }

        // 加载物品数据
        if (data.Keys.Contains("equipments") && data["equipments"] != null && data["equipments"].Count > 0)
        {
            for (int i = 0; i < data["equipments"].Count; i++)
            {
                InventoryItemDataModel itemDataModel = new InventoryItemDataModel();
                itemDataModel.ParseJSONData(data["equipments"][i]);
                equipmentDataList.Add(itemDataModel);
            }
        }

        // 刷新背包
        callback?.Invoke();
    }

    public override JsonData GetJSONData()
    {
        InventoryManager inventory = InventoryManager.Instance;
        JsonData inventoryData = new JsonData();

        inventoryData["weapon"] = inventory.currentWeaponData == null ? "" : inventory.currentWeaponData.id;

        var equipmentDict = inventory.equipmentDict;
        JsonData equipmentDataList = new JsonData();
        equipmentDataList.SetJsonType(JsonType.Array);
        foreach (var kv in equipmentDict)
        {
            JsonData equipmentJson = new JsonData();
            equipmentJson["guid"] = kv.Value.itemSO.id;
            equipmentJson["number"] = kv.Value.number;
            equipmentDataList.Add(equipmentJson);
        }

        inventoryData["equipments"] = equipmentDataList;

        return inventoryData;
    }
}