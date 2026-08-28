using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryEquipmentData", menuName = "Database/Inventory/InventoryMaterialData")]
public class InventoryMaterialData : InventoryItemBaseData
{
    public InventoryMaterialID materialID;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        name = materialID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}