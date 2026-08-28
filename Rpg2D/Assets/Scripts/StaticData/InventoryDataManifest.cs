using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryDataManifest", menuName = "Database/Inventory/InventoryDataManifest")]
public class InventoryDataManifest : ScriptableObject
{
    public List<InventoryItemBaseData> equipmentDataList;
}