using System;
using UnityEngine.UI;

public class SkillItemView : UIBehaviour
{
    public SkillID SkillID { get; set; }
    private ulong _sfx;
    private Button _btn;
    private bool isUnlocked;
    private Image _lockImage;
    private IDataProvider _dataProvider;

    public SkillItemView(IDataProvider dataProvider, SkillID skillID)
    {
        _dataProvider = dataProvider;
        SkillID = skillID;
    }

    protected override void ParseComponent()
    {
        _btn = DisplayObject.GetComponent<Button>();
        _lockImage = FindComponent<Image>("Lock");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_btn, OnClickBtn);
    }

    public void SetUnlock(bool isUnlocked)
    {
        _lockImage.fillAmount = isUnlocked ? 0 : 1;
    }

    private void OnClickBtn()
    {
        Skill skill = null;
        switch (SkillID)
        {
            case SkillID.Roll:
                skill = SkillManager.Instance.SkillRoll;
                break;
            case SkillID.IdleBlock:
                skill = SkillManager.Instance.SkillIdleBlock;
                break;
            case SkillID.Clone:
                skill = SkillManager.Instance.SkillClone;
                break;
            case SkillID.MagicOrb:
                skill = SkillManager.Instance.SkillMagicOrb;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (skill.isUnlocked) return;
        if (SkillManager.Instance.CanUnlockSkill(SkillID) == false) return;
        if (_dataProvider.Coin < skill.price) return;

        _dataProvider.Coin -= skill.price;
        EventSubscriber.OnCoinChange?.Invoke();
        skill.isUnlocked = true;
        SetUnlock(true);
        SoundManager.Instance.PlaySfx(AudioID.SfxUnlockSkill, ref _sfx);
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnRegisterButtonEvent(_btn, OnClickBtn);
    }
}