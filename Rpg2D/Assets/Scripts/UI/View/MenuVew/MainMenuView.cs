using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : UIBehaviour
{
    private Button _playBtn;
    private Button _settingBtn;
    private Button _exitBtn;
    private Text _coinText;
    private TMP_Dropdown _languageDropdown;

    protected override void ParseComponent()
    {
        _coinText = FindComponent<Text>("Middle/Status/Status_Gold/Text");
        _playBtn = FindComponent<Button>("Middle/Play");
        _settingBtn = FindComponent<Button>("Middle/Setting");
        _exitBtn = FindComponent<Button>("Middle/Exit");
        _languageDropdown = FindComponent<TMP_Dropdown>("Middle/Status/LanguageDropdown");
        _languageDropdown.onValueChanged.AddListener(OnSelectValueChanged);
        HandleUIOnWebGL();
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_playBtn, OnClickPlayBtn);
        RegisterButtonEvent(_settingBtn, OnClickSettingBtn);
        RegisterButtonEvent(_exitBtn, OnClickExitBtn);
        EventSubscriber.OnDeleteGameData += RefreshCoin;
    }

    public override void Show()
    {
        base.Show();
        SetLanguage();
    }

    private void OnClickExitBtn()
    {
        SoundManager.Instance.PlaySharedSfx(AudioID.SfxButtonClick);
        GameManager.Instance.Dispose();
        Application.Quit();
    }

    private void OnClickPlayBtn()
    {
        EventSubscriber.FromMenuSceneToGameScene?.Invoke();
        SoundManager.Instance.PlaySharedSfx(AudioID.SfxButtonClick);
    }

    private void OnClickSettingBtn()
    {
        NotifyViewEvent(EventConst.OnClickMenuSetting);
        SoundManager.Instance.PlaySharedSfx(AudioID.SfxButtonClick);
    }

    private void OnSelectValueChanged(int index)
    {
        switch (index)
        {
            case 0:
                LocalizationManager.CurrentLanguage = "Chinese (Simplified)";
                break;
            case 1:
                LocalizationManager.CurrentLanguage = "English";
                break;
            case 2:
                LocalizationManager.CurrentLanguage = "Chinese (Traditional)";
                break;
            case 3:
                LocalizationManager.CurrentLanguage = "Japanese";
                break;
            case 4:
                LocalizationManager.CurrentLanguage = "Korean";
                break;
        }
    }

    private void HandleUIOnWebGL()
    {
        if (UnityHelper.IsWebGL())
            _exitBtn.gameObject.SetActive(false);
    }

    private void SetLanguage()
    {
        RefreshCoin();
        switch (LocalizationManager.CurrentLanguage)
        {
            case "Chinese (Simplified)":
                _languageDropdown.value = 0;
                break;
            case "English":
                _languageDropdown.value = 1;
                break;
            case "Chinese (Traditional)":
                _languageDropdown.value = 2;
                break;
            case "Japanese":
                _languageDropdown.value = 3;
                break;
            case "Korean":
                _languageDropdown.value = 4;
                break;
        }
    }

    private void RefreshCoin()
    {
        _coinText.text = DataManager.Instance.GameDataModel.coin.ToString();
    }

    protected override void RemoveEvent()
    {
        base.RemoveEvent();
        EventSubscriber.OnDeleteGameData -= RefreshCoin;
        _languageDropdown.onValueChanged.RemoveListener(OnSelectValueChanged);
        UnRegisterButtonEvent(_playBtn, OnClickPlayBtn);
        UnRegisterButtonEvent(_settingBtn, OnClickSettingBtn);
        UnRegisterButtonEvent(_exitBtn, OnClickExitBtn);
    }
}