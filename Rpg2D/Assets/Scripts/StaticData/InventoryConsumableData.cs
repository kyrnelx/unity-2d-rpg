using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Database/Inventory/InventoryConsumableData")]
public class InventoryConsumableData : InventoryItemStatData
{
    public InventoryConsumableID consumableID;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = consumableID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}