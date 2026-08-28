using System;
using UnityEngine;

public class UIManager
{
    private static UIManager m_instance;
    public static UIManager Instance => m_instance ?? (m_instance = new UIManager());

    public Canvas UICanvas;
    public Camera UICamera;
    public Transform Bottom;
    public Transform Middle;
    public Transform Top;
    private MainMenuViewController _mainMenuViewController;
    private MainGameViewController _mainGameViewController;
    private LoadingView _loadingView;
    private NoticeBoardView _noticeBoardView;

    public void CreateGameUI()
    {
        GameObject uiRoot = ResourceLoader.Instance.LoadObjFromResources("UI/UIRoot");
        UICanvas = uiRoot.transform.Find("UICanvas").GetComponent<Canvas>();
        UICamera = uiRoot.transform.Find("UICamera").GetComponent<Camera>();
        Bottom = uiRoot.transform.Find("UICanvas").transform.Find("Bottom").transform;
        Middle = uiRoot.transform.Find("UICanvas").transform.Find("Middle").transform;
        Top = uiRoot.transform.Find("UICanvas").transform.Find("Top").transform;
        GameObject.DontDestroyOnLoad(uiRoot);
        CreateMenuView();
    }

    public void CreateMenuView()
    {
        if (_mainMenuViewController != null)
        {
            _mainMenuViewController.ShowMenuView();
            return;
        }

        _mainMenuViewController = new MainMenuViewController();
        _mainMenuViewController.Initialise();
    }

    public void HideMenuView()
    {
        _mainMenuViewController.HideMenuView();
    }

    public void ShowGameView()
    {
        if (_mainGameViewController != null)
        {
            _mainGameViewController.ShowGameView();
            return;
        }

        _mainGameViewController = new MainGameViewController();
        _mainGameViewController.Initialise();
    }

    public void HideGameView()
    {
        _mainGameViewController.HideGameView();
    }

    #region Loading View

    public void ShowLoadingView()
    {
        CreateLoadingView();
        _loadingView.Show();
    }

    public void HideLoadingView()
    {
        _loadingView.Hide();
    }

    private void CreateLoadingView()
    {
        if (_loadingView != null) return;
        _loadingView = new LoadingView();
        GameObject loadingViewObj = ResourceLoader.Instance.LoadObjFromResources("UI/LoadingView");
        _loadingView.SetDisplayObject(loadingViewObj);
        SetObjectToLayer(loadingViewObj.transform, UILayer.Top);
    }

    #endregion

    #region Notice Board View

    public void ShowNoticeBoardView(string notice)
    {
        CreateNoticeBoardView();
        _noticeBoardView.Show();
    }

    public void HideNoticeBoardView()
    {
        _noticeBoardView.Hide();
    }

    private void CreateNoticeBoardView()
    {
        if (_noticeBoardView != null) return;
        _noticeBoardView = new NoticeBoardView();
        GameObject noticeView = ResourceLoader.Instance.LoadObjFromResources("UI/NoticeBoardView");
        _noticeBoardView.SetDisplayObject(noticeView);
        SetObjectToLayer(noticeView.transform, UILayer.Top);
    }

    #endregion

    public void SetObjectToLayer(Transform obj, UILayer layer)
    {
        switch (layer)
        {
            case UILayer.Bottom:
                obj.SetParent(Bottom, false);
                break;
            case UILayer.Middle:
                obj.SetParent(Middle, false);
                break;
            case UILayer.Top:
                obj.SetParent(Top, false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
        }
    }
}

public enum UILayer
{
    Bottom,
    Middle,
    Top
}

// Depth Only 在物体销毁后不更新画布