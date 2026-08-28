using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Database/Inventory/InventoryItemData")]
public class InventoryItemData : InventoryItemBaseData
{
    public InventoryItemID itemID;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = itemID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}