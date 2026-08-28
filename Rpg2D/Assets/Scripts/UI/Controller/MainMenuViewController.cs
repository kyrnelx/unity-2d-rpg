using UnityEngine;

public class MainMenuViewController : UIController
{
    private MainMenuView _mainMenuView;
    private MenuSettingView _menuSettingView;

    public override void Initialise()
    {
        ShowMenuView();
    }

    #region Menu View

    public void ShowMenuView()
    {
        CreateMenuView();
        _mainMenuView.DisplayTransform.SetAsLastSibling();
        _mainMenuView.Show();
    }

    public void HideMenuView()
    {
        _mainMenuView.Hide();
    }

    private void CreateMenuView()
    {
        _mainMenuView = new MainMenuView();
        GameObject mainMenuViewObj = ResourceLoader.Instance.LoadObjFromResources("UI/MainMenuView");
        _mainMenuView.SetDisplayObject(mainMenuViewObj);
        _mainMenuView.AddUIEvent((name, args) =>
        {
            switch (name)
            {
                case EventConst.OnClickMenuSetting:
                    ShowMenuSettingView();
                    break;
            }
        });
        UIManager.Instance.SetObjectToLayer(mainMenuViewObj.transform, UILayer.Top);
    }

    #endregion

    #region Menu Setting View

    public void ShowMenuSettingView()
    {
        CreateMenuSettingView();
        _menuSettingView.DisplayTransform.SetAsLastSibling();
        _menuSettingView.Show();
    }

    public void HideMenuSettingView()
    {
        _menuSettingView.Hide();
    }

    private void CreateMenuSettingView()
    {
        if (_menuSettingView != null) return;
        _menuSettingView = new MenuSettingView(DataManager.Instance);
        GameObject menuSetting = ResourceLoader.Instance.LoadObjFromResources("UI/MenuSettingView");
        _menuSettingView.SetDisplayObject(menuSetting);
        UIManager.Instance.SetObjectToLayer(menuSetting.transform, UILayer.Top);
    }

    #endregion

    public override void Dispose()
    {
        _menuSettingView?.Dispose();
        _menuSettingView = null;

        _mainMenuView?.Dispose();
        _mainMenuView = null;
    }
}