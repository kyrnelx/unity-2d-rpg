using UnityEngine.UI;

public class SkillView : UIBehaviour
{
    private Button _closeBtn;
    private SkillItemView _skill_RollView;
    private SkillItemView _skill_CloneView;
    private SkillItemView _skill_IdleBlockView;
    private SkillItemView _skill_MagicOrbView;
    private Text _coinText;
    private IDataProvider _dataProvider;

    public SkillView(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
        EventSubscriber.OnCoinChange += RefreshCoin;
    }

    protected override void ParseComponent()
    {
        _closeBtn = FindComponent<Button>("Top/Button_Back");
        _skill_RollView = new SkillItemView(DataManager.Instance, SkillID.Roll);
        _skill_RollView.SetDisplayObject(FindGameObject("Middle/Skill_Roll"));
        _skill_CloneView = new SkillItemView(DataManager.Instance, SkillID.Clone);
        _skill_CloneView.SetDisplayObject(FindGameObject("Middle/Skill_Clone"));
        _skill_IdleBlockView = new SkillItemView(DataManager.Instance, SkillID.IdleBlock);
        _skill_IdleBlockView.SetDisplayObject(FindGameObject("Middle/Skill_IdleBlock"));
        _skill_MagicOrbView = new SkillItemView(DataManager.Instance, SkillID.MagicOrb);
        _skill_MagicOrbView.SetDisplayObject(FindGameObject("Middle/Skill_MagicOrb"));
        _coinText = FindComponent<Text>("Top/TopBar/Status_All/StatusGold/Text");
        EventSubscriber.OnCoinChange += RefreshCoin;
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_closeBtn, OnClickCloseBtn);
    }

    public override void Show()
    {
        base.Show();
        _skill_RollView.SetUnlock(SkillManager.Instance.SkillRoll.isUnlocked);
        _skill_CloneView.SetUnlock(SkillManager.Instance.SkillClone.isUnlocked);
        _skill_IdleBlockView.SetUnlock(SkillManager.Instance.SkillIdleBlock.isUnlocked);
        _skill_MagicOrbView.SetUnlock(SkillManager.Instance.SkillMagicOrb.isUnlocked);
        RefreshCoin();
    }

    private void RefreshCoin()
    {
        _coinText.text = _dataProvider.Coin.ToString();
    }

    private void OnClickCloseBtn()
    {
        TimeManager.Instance.ResumeTime();
        SoundManager.Instance.PlaySharedSfx(AudioID.SfxButtonClick);
        Hide();
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        EventSubscriber.OnCoinChange -= RefreshCoin;
        EventSubscriber.OnCoinChange -= RefreshCoin;
        UnRegisterButtonEvent(_closeBtn, OnClickCloseBtn);
    }
}