using UnityEngine;

public class MainGameViewController : UIController
{
    private MainGameView _mainGameView;
    private InventoryView _inventoryView;
    private SkillView _skillView;
    private DeadView _deadView;
    private GameSettingView _gameSettingView;
    private GameWinView _gameWinView;

    public override void Initialise()
    {
        ShowGameView();

        EventSubscriber.OnPlayerDead += ShowDeadView;
        EventSubscriber.OnGameWin += ShowGameWinView;
    }

    #region Game View

    private void CreateGameView()
    {
        if (_mainGameView != null) return;
        _mainGameView = new MainGameView();
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/MainGameView");
        _mainGameView.SetDisplayObject(obj);
        _mainGameView.AddUIEvent(OnGameViewAction);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
    }

    public void ShowGameView()
    {
        CreateGameView();
        _mainGameView.Show();
    }

    public void HideGameView()
    {
        _mainGameView.Hide();
    }

    private void OnGameViewAction(string evtType, object data)
    {
        switch (evtType)
        {
            case EventConst.OnClickInventory:
                OnClickInventory();
                break;
            case EventConst.OnClickGameSetting:
                OnClickGameSetting();
                break;
            case EventConst.OnClickSkill:
                OnClickSkill();
                break;
        }
    }

    private void OnClickSkill()
    {
        TimeManager.Instance.PauseTime();
        ShowSkillView();
    }

    private void OnClickGameSetting()
    {
        TimeManager.Instance.PauseTime();
        ShowGameSettingView();
    }

    private void OnClickInventory()
    {
        TimeManager.Instance.PauseTime();
        ShowInventoryView();
    }

    #endregion

    #region Dead View

    private void CreateDeadView()
    {
        if (_deadView != null) return;
        _deadView = new DeadView();
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/DeadView");
        _deadView.SetDisplayObject(obj);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
    }

    private void ShowDeadView()
    {
        CreateDeadView();
        _gameWinView?.Hide();
        _deadView.Show();
    }

    private void HideDeadView()
    {
        _deadView.Hide();
    }

    #endregion

    #region Inventory View

    private void CreateInventoryView()
    {
        if (_inventoryView != null) return;
        _inventoryView = new InventoryView();
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/InventoryView");
        _inventoryView.SetDisplayObject(obj);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
    }

    private void ShowInventoryView()
    {
        CreateInventoryView();
        _inventoryView.DisplayTransform.SetAsLastSibling();
        _inventoryView.Show();
    }

    private void HideInventoryView()
    {
        _inventoryView.Hide();
    }

    #endregion

    #region Skill View

    private void CreateSkillView()
    {
        if (_skillView != null) return;
        _skillView = new SkillView(DataManager.Instance);
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/SkillView");
        _skillView.SetDisplayObject(obj);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
    }

    private void ShowSkillView()
    {
        CreateSkillView();
        _skillView.DisplayTransform.SetAsLastSibling();
        _skillView.Show();
    }

    private void HideSkillView()
    {
        _skillView.Hide();
    }

    #endregion

    #region Game Setting View

    private void CreateGameSettingView()
    {
        if (_gameSettingView != null) return;
        _gameSettingView = new GameSettingView(DataManager.Instance);
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/GameSettingView");
        _gameSettingView.SetDisplayObject(obj);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
    }

    private void ShowGameSettingView()
    {
        CreateGameSettingView();
        _gameSettingView.DisplayTransform.SetAsLastSibling();
        _gameSettingView.Show();
    }

    private void HideGameSettingView()
    {
        _gameSettingView.Hide();
    }

    #endregion

    #region Game Win View

    private void CreateGameWinView()
    {
        if (_gameWinView != null) return;
        _gameWinView = new GameWinView(DataManager.Instance);
        GameObject obj = ResourceLoader.Instance.LoadObjFromResources("UI/GameWinView");
        _gameWinView.SetDisplayObject(obj);
        UIManager.Instance.SetObjectToLayer(obj.transform, UILayer.Middle);
    }

    private void ShowGameWinView()
    {
        CreateGameWinView();
        _gameWinView.DisplayTransform.SetAsLastSibling();
        _gameWinView.Show();
    }

    private void HideGameWinView()
    {
        _gameWinView.Hide();
    }

    #endregion

    public override void Dispose()
    {
        EventSubscriber.OnPlayerDead -= ShowDeadView;
        EventSubscriber.OnGameWin -= ShowGameWinView;

        _deadView?.Dispose();
        _deadView = null;

        _skillView?.Dispose();
        _skillView = null;

        _inventoryView?.Dispose();
        _inventoryView = null;

        _mainGameView.RemoveUIEvent(OnGameViewAction);
        _mainGameView?.Dispose();
        _mainGameView = null;

        _gameWinView?.Dispose();
        _gameWinView = null;
    }
}