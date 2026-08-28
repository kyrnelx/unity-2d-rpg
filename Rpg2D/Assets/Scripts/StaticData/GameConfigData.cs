using UnityEngine;

/// <summary>
/// 游戏全局配置文件（ScriptableObject 数据容器）
/// 功能：集中管理游戏版本、构建类型、难度等核心参数
/// </summary>
[CreateAssetMenu(fileName = "GameConfigData", menuName = "Database/GameConfigData")]
public class GameConfigData : ScriptableObject
{
    [Header("Version Settings")] 
    public string gameVersion = "1.0.0";

    [Header("Build Configuration")] 
    public bool isDebugMode;

    [Space(10)] 
    [Tooltip("是否正式发布版本")] 
    public bool isReleaseBuild;

    [Tooltip("是否为试玩版本")] 
    public bool isDemoRelease;

    [Header("Gameplay Settings")] 
    [Tooltip("默认游戏难度")]
    public GameDifficulty defaultDifficulty = GameDifficulty.Normal;
}

/// <summary>
/// 游戏难度等级定义
/// </summary>
public enum GameDifficulty
{
    Easy,
    Normal,
    Hard,
    VeryHard,
    Nightmare
}