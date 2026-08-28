using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IDisposable, IGrimReaperProvider
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    public bool ResetPlayerHealth { get; set; }
    public bool ReloadData { get; set; }

    #region IGrimReaperProvider

    private EnemyGrimReaper m_grimReaper;
    public EnemyGrimReaper GrimReaper => m_grimReaper;
    public bool IsGrimReaperDead { get; set; }

    #endregion

    private bool m_initialized;
    private uint m_menuBgm;
    private uint m_gameBgm;
    private uint m_bossBgm;
    private ulong m_evilVoiceSfx;
    private ulong m_btnHoverSfx;
    private ulong m_btnClickSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (m_initialized) return;
        m_initialized = true;
        RegisterEvent();
        CreateGameWorld();
    }

    private void Update()
    {
        HandleDebugControls();
    }


    private void CreateGameWorld()
    {
#if UNITY_EDITOR
        SoundManager.Instance.InitialiseHierarchyWindow();
#endif
        DataManager.Instance.LoadGameData(() =>
        {
            SoundManager.Instance.PlayBGM(AudioID.BGMMenu, ref m_menuBgm);
            UIManager.Instance.CreateGameUI();
        });
    }

    private void RegisterEvent()
    {
        EventSubscriber.FromMenuSceneToGameScene += FromMenuSceneToGameScene;
        EventSubscriber.FromGameSceneToMenuScene += FromGameSceneToMenuScene;
        EventSubscriber.ReloadGameScene += ReloadGameScene;
    }

    private void FromMenuSceneToGameScene()
    {
        SoundManager.Instance.StopBGM(m_menuBgm);

        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideMenuView();
        UIManager.Instance.ShowGameView();

        CheckReloadDataAndLoadGameScene();
    }

    private void FromGameSceneToMenuScene()
    {
        SoundManager.Instance.StopBGM(m_gameBgm);

        UIManager.Instance.ShowLoadingView();
        UIManager.Instance.HideGameView();
        UIManager.Instance.CreateMenuView();

        SceneLoader.Instance.LoadSceneAsync(SceneID.MainMenuScene, () =>
        {
            SoundManager.Instance.PlayBGM(AudioID.BGMMenu, ref m_menuBgm);
            UIManager.Instance.HideLoadingView();
        });
    }

    private void ReloadGameScene()
    {
        SoundManager.Instance.StopBGM(m_gameBgm);

        UIManager.Instance.ShowLoadingView();

        CheckReloadDataAndLoadGameScene();
    }

    private void CheckReloadDataAndLoadGameScene()
    {
        if (ReloadData)
        {
            ReloadData = false;
            DataManager.Instance.LoadGameData(LoadGameScene);
        }
        else
        {
            LoadGameScene();
        }
    }

    private void LoadGameScene()
    {
        SceneLoader.Instance.LoadSceneAsync(SceneID.MainGameScene, () =>
        {
            SoundManager.Instance.PlayBGM(AudioID.BGMGame, ref m_gameBgm);
            UIManager.Instance.HideLoadingView();
        });
    }

    public void ChallengeBoss(bool isChallenge)
    {
        if (isChallenge)
        {
            m_grimReaper = FindObjectOfType<EnemyGrimReaper>();
            SoundManager.Instance.StopBGM(m_gameBgm);
            SoundManager.Instance.PlayBGM(AudioID.BGMBoss, ref m_bossBgm);
            SoundManager.Instance.PlaySfx(AudioID.SfxGrimReaperVoice, ref m_evilVoiceSfx);
            if (m_grimReaper != null)
                m_grimReaper.stateMachine.ChangeState(m_grimReaper.BattleState);
        }
        else
        {
            SoundManager.Instance.StopBGM(m_bossBgm);
            SoundManager.Instance.PlayBGM(AudioID.BGMGame, ref m_gameBgm);
            SoundManager.Instance.StopSfx(m_evilVoiceSfx);
            if (m_grimReaper != null && IsGrimReaperDead == false)
                m_grimReaper.stateMachine.ChangeState(m_grimReaper.IdleState);
        }
    }

    public void Dispose()
    {
        EventSubscriber.FromMenuSceneToGameScene -= FromMenuSceneToGameScene;
        EventSubscriber.ReloadGameScene -= ReloadGameScene;
        EventSubscriber.FromGameSceneToMenuScene -= FromGameSceneToMenuScene;
    }

    private void HandleDebugControls()
    {
        if (!DataManager.Instance.GetGameConfigData().isDebugMode) return;
    }
}