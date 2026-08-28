using UnityEngine.UI;

public class InventoryItemView : UIBehaviour
{
    public InventoryItemBaseData itemData;
    public Text numberText;
    private Image _iconImage;
    private Button _btn;

    protected override void ParseComponent()
    {
        _iconImage = FindComponent<Image>("GradeFrame/ItemIcon");
        numberText = FindComponent<Text>("Number");
        _btn = DisplayObject.GetComponent<Button>();
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_btn, OnClickBtn);
    }

    public void Initialise(InventoryItemBaseData itemSO, int number)
    {
        this.itemData = itemSO;
        _iconImage.sprite = itemSO.sprite;
        numberText.text = number.ToString();
    }

    private void OnClickBtn()
    {
        if (itemData.itemBaseType == InventoryItemBaseType.Equipment)
        {
             SoundManager.Instance.PlaySharedSfx(AudioID.SfxEquip);
            EventSubscriber.Equip?.Invoke(itemData);
        }
    }

    protected override void RemoveEvent()
    {
        UnRegisterButtonEvent(_btn, OnClickBtn);
        base.RemoveEvent();
    }
}