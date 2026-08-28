using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager m_instance;
    public static InventoryManager Instance => m_instance;

    public Dictionary<InventoryEquipmentID, InventoryItem> equipmentDict;
    public Dictionary<InventoryConsumableID, InventoryItem> consumableDict;
    public Dictionary<InventoryMaterialID, InventoryItem> materialDict;
    public Dictionary<InventoryItemID, InventoryItem> itemDict;
    public List<InventoryItem> allItemList = new List<InventoryItem>();
    public InventoryEquipmentData currentWeaponData;

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

        equipmentDict = new Dictionary<InventoryEquipmentID, InventoryItem>();
        consumableDict = new Dictionary<InventoryConsumableID, InventoryItem>();
        materialDict = new Dictionary<InventoryMaterialID, InventoryItem>();
        itemDict = new Dictionary<InventoryItemID, InventoryItem>();

        EventSubscriber.OnInventoryRealItemPickup += AddItemByItemData;
        EventSubscriber.Equip += Equip;
        EventSubscriber.UnEquip += UnEquip;

        DataManager.Instance.InventoryDataModel.ParseJSONData(UpdateByPersistentData);
    }

    public void AddItemByItemData(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                InventoryEquipmentData inventoryEquipmentData = itemData as InventoryEquipmentData;
                if (equipmentDict.Keys.Contains(inventoryEquipmentData.equipmentID))
                {
                    equipmentDict[inventoryEquipmentData.equipmentID].Add();
                }
                else
                {
                    equipmentDict.Add(inventoryEquipmentData.equipmentID, new InventoryItem(inventoryEquipmentData));
                }

                break;
            case InventoryItemBaseType.Consumable:
                InventoryConsumableData inventoryConsumableSo = itemData as InventoryConsumableData;
                if (consumableDict.Keys.Contains(inventoryConsumableSo.consumableID))
                {
                    consumableDict[inventoryConsumableSo.consumableID].Add();
                }
                else
                {
                    consumableDict.Add(inventoryConsumableSo.consumableID, new InventoryItem(inventoryConsumableSo));
                }

                break;
            case InventoryItemBaseType.Material:
                InventoryMaterialData inventoryMaterialSo = itemData as InventoryMaterialData;
                if (materialDict.Keys.Contains(inventoryMaterialSo.materialID))
                {
                    materialDict[inventoryMaterialSo.materialID].Add();
                }
                else
                {
                    materialDict.Add(inventoryMaterialSo.materialID, new InventoryItem(inventoryMaterialSo));
                }

                break;
            case InventoryItemBaseType.Item:
                InventoryItemData inventoryItemSo = itemData as InventoryItemData;
                if (itemDict.Keys.Contains(inventoryItemSo.itemID))
                {
                    itemDict[inventoryItemSo.itemID].Add();
                }
                else
                {
                    itemDict.Add(inventoryItemSo.itemID, new InventoryItem(inventoryItemSo));
                }

                break;
            default:
                Debugger.Error($"[Inventory Add Item Error] 无效的物品类型: {itemData.itemBaseType}. " +
                               $"物品名称: {itemData.name}, 物品ID: {itemData.id}, " +
                               $"物品类型的枚举值: {Enum.GetName(typeof(InventoryItemBaseType), itemData.itemBaseType)}. " +
                               "请检查该物品的类型和数据设置。");
                break;
        }

        AddToList(itemData);
    }

    public void RemoveItemByItemData(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                if (itemData is InventoryEquipmentData equipmentItemData)
                {
                    if (equipmentDict.TryGetValue(equipmentItemData.equipmentID, out var equipmentItem))
                    {
                        equipmentItem.Remove();
                        if (equipmentItem.number <= 0)
                        {
                            equipmentDict.Remove(equipmentItemData.equipmentID);
                        }
                    }
                }

                break;
            case InventoryItemBaseType.Consumable:
                if (itemData is InventoryConsumableData consumableItemData)
                {
                    if (consumableDict.TryGetValue(consumableItemData.consumableID, out var consumableItem))
                    {
                        consumableItem.Remove();
                        if (consumableItem.number <= 0)
                        {
                            consumableDict.Remove(consumableItemData.consumableID);
                        }
                    }
                }

                break;
            case InventoryItemBaseType.Material:
                if (itemData is InventoryMaterialData materialItemData)
                {
                    if (materialDict.TryGetValue(materialItemData.materialID, out var materialItem))
                    {
                        materialItem.Remove();
                        if (materialItem.number <= 0)
                        {
                            materialDict.Remove(materialItemData.materialID);
                        }
                    }
                }

                break;
            case InventoryItemBaseType.Item:
                if (itemData is InventoryItemData itemItemData)
                {
                    if (itemDict.TryGetValue(itemItemData.itemID, out var typeItem))
                    {
                        typeItem.Remove();
                        if (typeItem.number <= 0)
                        {
                            itemDict.Remove(itemItemData.itemID);
                        }
                    }
                }

                break;
            default:
                Debugger.Error($"[Inventory Remove Item Error] 无效的物品类型: {itemData.itemBaseType}. " +
                               $"物品名称: {itemData.name}, 物品ID: {itemData.id}");
                break;
        }

        RemoveFromList(itemData);
    }

    private void Equip(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                if (currentWeaponData != null)
                    EventSubscriber.UnEquip?.Invoke(currentWeaponData);
                currentWeaponData = itemData as InventoryEquipmentData;
                RemoveItemByItemData(itemData);
                break;
        }
    }

    private void UnEquip(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                AddItemByItemData(itemData);
                break;
        }
    }

    private void AddToList(InventoryItemBaseData itemSO)
    {
        foreach (var item in allItemList)
        {
            if (item.itemSO == itemSO)
            {
                item.Add();
                return;
            }
        }

        allItemList.Add(new InventoryItem(itemSO));
    }

    private void RemoveFromList(InventoryItemBaseData itemSO)
    {
        foreach (var item in allItemList)
        {
            if (item.itemSO == itemSO)
            {
                item.Remove();
                if (item.number == 0)
                {
                    allItemList.Remove(item);
                }

                return;
            }
        }
    }

    public void UpdateByPersistentData()
    {
        InventoryDataModel inventoryDataModel = DataManager.Instance.InventoryDataModel;
        if (inventoryDataModel.currentWeaponID != null)
        {
            currentWeaponData =
                DataManager.Instance.LoadInventoryItemData(inventoryDataModel
                    .currentWeaponID) as InventoryEquipmentData;
        }
        else
        {
            currentWeaponData = null;
        }

        var equipmentDataList = inventoryDataModel.equipmentDataList;
        for (int i = 0; i < equipmentDataList.Count; i++)
        {
            InventoryItemDataModel itemDataModel = equipmentDataList[i];
            var itemData = DataManager.Instance.LoadInventoryItemData(equipmentDataList[i].id);
            for (int j = 0; j < itemDataModel.number; j++)
            {
                AddItemByItemData(itemData);
            }
        }
    }

    ~InventoryManager()
    {
        EventSubscriber.OnInventoryRealItemPickup -= AddItemByItemData;
        EventSubscriber.Equip -= Equip;
        EventSubscriber.UnEquip -= UnEquip;
    }
}