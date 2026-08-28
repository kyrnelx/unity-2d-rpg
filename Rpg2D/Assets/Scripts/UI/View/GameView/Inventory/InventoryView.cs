using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : UIBehaviour
{
    private Button _closeBtn;
    private ScrollRect _inventoryScrollRect;
    private Image _weaponSlotImage;
    private List<InventoryItemView> _equipmentViews = new List<InventoryItemView>();
    private Text _maxHealthStatText;
    private Text _damageStatText;
    private Text _magicPowerStatText;
    private Text _armorStatText;
    private Text _magicResistanceStatText;
    private Text _igniteStatText;
    private Text _chillStatText;
    private Text _lightingStatText;
    private Text _agilityStatText;
    private Text _intelligenceStatText;
    private Text _strengthStatText;
    private Text _vitalityStatText;
    private Text _criticalStatText;
    private Text evasionStatText;
    private Sprite _currentWeaponSprite;

    protected override void ParseComponent()
    {
        _maxHealthStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_MaxHealth/Value");
        _damageStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_Damage/Value");
        _magicPowerStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_MagicDamage/Value");
        _armorStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_Armor/Value");
        _magicResistanceStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_MagicResistance/Value");
        _igniteStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_Ignite/Value");
        _chillStatText = FindComponent<Text>("Middle/Left_Panel/Stats/Stat_Chill/Value");
        _lightingStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Lighting/Value");
        _agilityStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Agility/Value");
        _intelligenceStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Intelligence/Value");
        _strengthStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Strength/Value");
        _vitalityStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Vtality/Value");
        _criticalStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Critical/Value");
        evasionStatText = FindComponent<Text>("Middle/Left_Panel/Stats2/Stat_Evasion/Value");

        _closeBtn = FindComponent<Button>("Top/Button_Back");
        _inventoryScrollRect = FindComponent<ScrollRect>("Middle/Right_Panel/ScrollRect");

        _weaponSlotImage = FindComponent<Image>("Middle/Left_Panel/Character/EquipSlot_L/EquipFrameEmpty/Icon");
        _currentWeaponSprite = _weaponSlotImage.sprite;
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        EventSubscriber.Equip += Equip;
        EventSubscriber.UnEquip += UnEquip;
        RegisterButtonEvent(_closeBtn, OnClickCloseBtn);
    }

    public override void Show()
    {
        base.Show();
        RefreshView();
    }

    private void RefreshView()
    {
        InventoryManager inventory = InventoryManager.Instance;

        // 装备栏
        if (inventory.currentWeaponData != null)
            _weaponSlotImage.sprite = inventory.currentWeaponData.sprite;
        else
            _weaponSlotImage.sprite = _currentWeaponSprite;

        // 属性
        RefreshStatView();

        RefreshEquipmentItemViews();
    }

    private void RefreshStatView()
    {
        PlayerStats playerStats = PlayerManager.Instance.player.playerStats;
        _maxHealthStatText.text = playerStats.maxHealth.GetValue().ToString();
        _damageStatText.text = playerStats.attackPower.GetValue().ToString();
        _magicPowerStatText.text = playerStats.magicPower.GetValue().ToString();
        _armorStatText.text = playerStats.armor.GetValue().ToString();
        _magicResistanceStatText.text = playerStats.magicResistance.GetValue().ToString();
        _igniteStatText.text = playerStats.ignite.GetValue().ToString();
        _chillStatText.text = playerStats.chill.GetValue().ToString();
        _lightingStatText.text = playerStats.lighting.GetValue().ToString();
        _agilityStatText.text = playerStats.agility.GetValue().ToString();
        _intelligenceStatText.text = playerStats.intelligence.GetValue().ToString();
        _strengthStatText.text = playerStats.strength.GetValue().ToString();
        _vitalityStatText.text = playerStats.vitality.GetValue().ToString();
        _criticalStatText.text = playerStats.criticalPower.GetValue().ToString();
        evasionStatText.text = playerStats.evasion.GetValue().ToString();
    }

    public override void Hide()
    {
        DisposeEquipmentItemViews();
        base.Hide();
    }

    #region 装备 卸下 武器

    private void Equip(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                RemoveInventoryItemViewByItemSO(itemData);
                _weaponSlotImage.sprite = itemData.sprite;
                CoroutineManager.Instance.StartWaitForFrame(RefreshStatView);
                break;
            default:
                Debugger.Warning($"点击了未处理的物品类型：[{itemData.itemBaseType}]，物品名称：{itemData.name}");
                break;
        }
    }

    private void RemoveInventoryItemViewByItemSO(InventoryItemBaseData itemSO)
    {
        foreach (var equipment in _equipmentViews)
        {
            if (equipment.itemData == itemSO)
            {
                int number = int.Parse(equipment.numberText.text);
                if (number - 1 == 0)
                {
                    _equipmentViews.Remove(equipment);
                    equipment.Dispose();
                    return;
                }
                else
                {
                    equipment.numberText.text = (number - 1).ToString();
                }
            }
        }
    }

    private void UnEquip(InventoryItemBaseData itemData)
    {
        switch (itemData.itemBaseType)
        {
            case InventoryItemBaseType.Equipment:
                if (AddInventoryItemViewByItemSO(itemData) == false) CreateEquipmentViewByItemSO(itemData);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private bool AddInventoryItemViewByItemSO(InventoryItemBaseData itemData)
    {
        foreach (var equipment in _equipmentViews)
        {
            if (equipment.itemData == itemData)
            {
                equipment.numberText.text = (int.Parse(equipment.numberText.text) + 1).ToString();
                return true;
            }
        }

        return false;
    }

    #endregion

    #region 武器物品

    private void RefreshEquipmentItemViews()
    {
        var equipments = InventoryManager.Instance.equipmentDict;
        foreach (var equipment in equipments)
        {
            CreateEquipmentView(equipment);
        }
    }

    private void CreateEquipmentView(KeyValuePair<InventoryEquipmentID, InventoryItem> equipment)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityHelper.SetParent(obj.transform, _inventoryScrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(equipment.Value.itemSO, equipment.Value.number);
        _equipmentViews.Add(inventoryItemView);
    }

    private void CreateEquipmentViewByItemSO(InventoryItemBaseData itemSO)
    {
        var obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryItemView");
        UnityHelper.SetParent(obj.transform, _inventoryScrollRect.content);
        InventoryItemView inventoryItemView = new InventoryItemView();
        inventoryItemView.SetDisplayObject(obj);
        inventoryItemView.Initialise(itemSO, 1);
        _equipmentViews.Add(inventoryItemView);
    }

    private void DisposeEquipmentItemViews()
    {
        foreach (var itemView in _equipmentViews)
        {
            itemView.Dispose();
        }

        _equipmentViews.Clear();
    }

    #endregion

    private void OnClickCloseBtn()
    {
        TimeManager.Instance.ResumeTime();
        SoundManager.Instance.PlaySharedSfx(AudioID.SfxButtonClick);
        Hide();
    }

    protected override void RemoveEvent()
    {
        EventSubscriber.Equip -= Equip;
        EventSubscriber.UnEquip -= UnEquip;
        UnRegisterButtonEvent(_closeBtn, OnClickCloseBtn);
        base.RemoveEvent();
    }
}