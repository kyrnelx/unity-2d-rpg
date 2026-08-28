using UnityEngine.UI;

public class GameWinView : UIBehaviour
{
    private Button _playAgainBtn;
    private Button _returnBtn;
    private Button _saveBtn;
    private IDataProvider _dataProvider;

    public GameWinView(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    protected override void ParseComponent()
    {
        _playAgainBtn = FindComponent<Button>("Middle/PlayAgain");
        _returnBtn = FindComponent<Button>("Middle/Return");
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_playAgainBtn, OnClickPlayAgainBtn);
        RegisterButtonEvent(_returnBtn, OnClickReturnBtn);
    }

    private void OnClickReturnBtn()
    {
        Hide();
        _dataProvider.SetJSONData();
        EventSubscriber.FromGameSceneToMenuScene?.Invoke();
        SoundManager.Instance.PlaySharedSfx(AudioID.SfxButtonClick);
    }

    private void OnClickPlayAgainBtn()
    {
        Hide();
        EventSubscriber.ReloadGameScene?.Invoke();
        SoundManager.Instance.PlaySharedSfx(AudioID.SfxButtonClick);
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        UnRegisterButtonEvent(_playAgainBtn, OnClickPlayAgainBtn);
        UnRegisterButtonEvent(_returnBtn, OnClickReturnBtn);
    }
}