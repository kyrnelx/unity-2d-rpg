using I2.Loc;
using UnityEngine;
using UnityEngine.UI;
using Screen = UnityEngine.Device.Screen;

public class MenuSettingView : UIBehaviour
{
    private Slider _musicSlider;
    private Slider _soundSlider;
    private Button _closeBtn;
    private Button _saveBtn;
    private Button _clearBtn;
    private Button _leftResolutionBtn;
    private Button _rightResolutionBtn;
    private Text _resolutionText;
    private Button _leftScreenModeBtn;
    private Button _rightScreenModeBtn;
    private Text _screenModeText;
    private Localize _localize;
    private IDataProvider _dataProvider;

    private Vector2[] _resolutionList =
    {
        new Vector2(1920, 1080),
        new Vector2(1280, 720),
        new Vector2(960, 540),
        new Vector2(640, 360),
        new Vector2(480, 270),
        new Vector2(320, 180),
    };

    private int _currentResolutionIndex = 0;

    private FullScreenMode[] _screenModeList =
    {
        FullScreenMode.ExclusiveFullScreen,
        FullScreenMode.FullScreenWindow,
        FullScreenMode.MaximizedWindow,
        FullScreenMode.Windowed,
    };

    private int _currentScreenModeIndex = 1;

    public MenuSettingView(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    protected override void ParseComponent()
    {
        _musicSlider = FindComponent<Slider>("Middle/MusicSlider");
        _soundSlider = FindComponent<Slider>("Middle/SoundSlider");
        _closeBtn = FindComponent<Button>("Top/Button_Back");
        _saveBtn = FindComponent<Button>("Top/Save");
        _clearBtn = FindComponent<Button>("Top/Clear");
        _leftResolutionBtn = FindComponent<Button>("Middle/Resolution/Left");
        _rightResolutionBtn = FindComponent<Button>("Middle/Resolution/Right");
        _leftScreenModeBtn = FindComponent<Button>("Middle/ScreenMode/Left");
        _rightScreenModeBtn = FindComponent<Button>("Middle/ScreenMode/Right");
        _resolutionText = FindComponent<Text>("Middle/Resolution/Text");
        _screenModeText = FindComponent<Text>("Middle/ScreenMode/Text");
        _localize = _screenModeText.GetComponent<Localize>();
        _localize.SetTerm(_screenModeList[_currentScreenModeIndex].ToString());
    }

    protected override void AddEvent()
    {
        base.AddEvent();
        RegisterButtonEvent(_closeBtn, OnClickCloseBtn);
        RegisterButtonEvent(_saveBtn, OnClickSaveBtn);
        RegisterButtonEvent(_clearBtn, OnClickClearBtn);
        RegisterButtonEvent(_leftResolutionBtn, OnClickLeftResolutionBtn);
        RegisterButtonEvent(_rightResolutionBtn, OnClickRightResolutionBtn);
        RegisterButtonEvent(_leftScreenModeBtn, OnClickLeftScreenModeBtn);
        RegisterButtonEvent(_rightScreenModeBtn, OnClickRightScreenModeBtn);
        _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.AddListener(OnSoundSliderValueChanged);
    }

    private void OnClickRightScreenModeBtn()
    {
        if (_currentScreenModeIndex == _screenModeList.Length - 1)
        {
            _currentScreenModeIndex = 0;
        }
        else
        {
            _currentScreenModeIndex++;
        }

        _localize.SetTerm(_screenModeList[_currentScreenModeIndex].ToString());
        Screen.fullScreenMode = _screenModeList[_currentScreenModeIndex];
    }

    private void OnClickLeftScreenModeBtn()
    {
        if (_currentScreenModeIndex == 0)
        {
            _currentScreenModeIndex = _screenModeList.Length - 1;
        }
        else
        {
            _currentScreenModeIndex--;
        }

        _localize.SetTerm(_screenModeList[_currentScreenModeIndex].ToString());
        Screen.fullScreenMode = _screenModeList[_currentScreenModeIndex];
    }

    private void OnClickRightResolutionBtn()
    {
        if (_currentResolutionIndex == _resolutionList.Length - 1)
        {
            _currentResolutionIndex = 0;
        }
        else
        {
            _currentResolutionIndex++;
        }

        _resolutionText.text =
            $"{_resolutionList[_currentResolutionIndex].x}x{_resolutionList[_currentResolutionIndex].y}";
        bool currentFullscreenState = Screen.fullScreen;
        Screen.SetResolution((int)_resolutionList[_currentResolutionIndex].x,
            (int)_resolutionList[_currentResolutionIndex].y, currentFullscreenState);
    }

    private void OnClickLeftResolutionBtn()
    {
        if (_currentResolutionIndex == 0)
        {
            _currentResolutionIndex = _resolutionList.Length - 1;
        }
        else
        {
            _currentResolutionIndex--;
        }

        _resolutionText.text =
            $"{_resolutionList[_currentResolutionIndex].x}x{_resolutionList[_currentResolutionIndex].y}";
        bool currentFullscreenState = Screen.fullScreen;
        Screen.SetResolution((int)_resolutionList[_currentResolutionIndex].x,
            (int)_resolutionList[_currentResolutionIndex].y, currentFullscreenState);
    }

    private void OnClickClearBtn()
    {
        _dataProvider.ClearJSONData();
        _dataProvider.DeleteFile();
        EventSubscriber.OnDeleteGameData?.Invoke();
    }

    private void OnClickSaveBtn()
    {
        _dataProvider.SaveGameData();
    }

    public override void Show()
    {
        base.Show();
        _musicSlider.value = _dataProvider.MusicVolume;
        _soundSlider.value = _dataProvider.SoundVolume;
    }

    private void OnSoundSliderValueChanged(float arg0)
    {
        SoundManager.Instance.UpdateSfxVolume(arg0);
        _dataProvider.SoundVolume = arg0;
    }

    private void OnMusicSliderValueChanged(float arg0)
    {
        SoundManager.Instance.UpdateMusicVolume(arg0);
        _dataProvider.MusicVolume = arg0;
    }

    private void OnClickCloseBtn()
    {
        SoundManager.Instance.PlaySharedSfx(AudioID.SfxButtonClick);
        Hide();
    }

    protected override void RemoveEvent()
    {
        _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChanged);
        _soundSlider.onValueChanged.RemoveListener(OnSoundSliderValueChanged);
        UnRegisterButtonEvent(_saveBtn, OnClickSaveBtn);
        UnRegisterButtonEvent(_closeBtn, OnClickCloseBtn);
        UnRegisterButtonEvent(_clearBtn, OnClickClearBtn);
        UnRegisterButtonEvent(_leftResolutionBtn, OnClickLeftResolutionBtn);
        UnRegisterButtonEvent(_rightResolutionBtn, OnClickRightResolutionBtn);
        UnRegisterButtonEvent(_leftScreenModeBtn, OnClickLeftScreenModeBtn);
        UnRegisterButtonEvent(_rightScreenModeBtn, OnClickRightScreenModeBtn);
        base.RemoveEvent();
    }
}