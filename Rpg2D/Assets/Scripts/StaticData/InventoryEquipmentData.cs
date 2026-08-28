using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Database/Inventory/InventoryEquipmentData")]
public class InventoryEquipmentData : InventoryItemStatData
{
    public InventoryEquipmentID equipmentID;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = equipmentID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}