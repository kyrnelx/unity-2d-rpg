# 2D RPG · Rpg2D

Unity **2022.3.62f3c1**，**Built-in 渲染管线** + 2D 特性包（Cinemachine / Tilemap / TextMeshPro / I2 Localization），横版动作 RPG：主菜单 → 森林清怪 → 进 Boss 区打「死神」→ 传送门通关。

**运行**：用 Unity 打开本目录 → `Assets/Scenes/MainMenuScene.unity` → Play（Build Settings 已按 Menu → Game 顺序配置）。

## 结构

```
Assets/
├─ Scenes/            # MainMenuScene（启动）、MainGameScene
├─ Scripts/
│  ├─ Framework/      # 自研框架（独立 asmdef「Framework」）：资源/场景加载、协程、对象池、UI 基类
│  ├─ Managers/       # Game / Data / Player / Skill / Inventory / Sound / UI / JSON 管理器（单例）
│  ├─ Entities/       # Entity 基类 + 状态机（Player / Orc / GrimReaper）+ 属性系统
│  ├─ StaticData/     # ScriptableObject 数据定义（Audio / Enemy / Skill / Inventory / GameConfig）
│  ├─ PersistentData/ # 存档模型（JSON + PlayerPrefs）
│  ├─ UI/             # Controller + View（菜单 / 对局 / 背包 / 技能 / 设置 / 结算）
│  ├─ Skill/ Abilities/ Inventory/ FX/ AnimationEvents/ Misc/ Utils/ Enums/
├─ Data/              # 数据资产（.asset）+ 4 个 Manifest
├─ Prefabs/           # 玩家 / 敌人 / Boss / 13 把武器 / UI 元件（共 28 个）
├─ Resources/         # UIRoot、GameResources、I2Languages、UI/*.prefab
├─ Texture/ Animations/ Sound/ Fonts/ TextMesh Pro/ CSV/
└─ Plugins/           # I2 Localization、LitJson
```

## 玩法与操作

| 操作 | 键位 |
|---|---|
| 移动 | `W A S D` / 方向键 |
| 跳跃 | `空格` |
| 普攻 | `鼠标左键`（点在 UI 上不触发） |
| 翻滚 | `F` |
| 格挡 | `鼠标右键` |
| 魔法球 | `G`（需解锁） |

- 敌人：Light / Dark Orc 巡逻追击；Boss「死神」有出现、战斗、施法、死亡等状态，进入 Boss 区域会自动切 Boss BGM 并进入战斗。
- 技能：翻滚 / 格挡 / 分身 / 魔法球，按 `SkillData.unlockCondition` 逐级解锁，解锁进度存档。
- 通关：击杀死神后走进 `Portal` → `OnGameWin` → 结算界面；玩家死亡 → `DeadView`（重新挑战 / 返回主菜单，重开时回满血）。

## 架构

| 层 | 说明 |
|---|---|
| `Framework`（asmdef） | `ResourceLoader`（Resources 同步/异步）、`SceneLoader`、`CoroutineManager`、`ObjectPool`、`TimeManager`、`Debugger`、`UIController` / `UIBehaviour`（UI 基类） |
| `Managers` | 均为 `DontDestroyOnLoad` 单例：`GameManager`（流程/场景切换/Boss 状态）、`DataManager`（数据 + 存档）、`PlayerManager`、`SkillManager`、`InventoryManager`、`SoundManager`（按 AudioID 播放 BGM/SFX）、`UIManager`（动态创建 UI）、`JSONManager` |
| `Entities` | `Entity` 基类 + `StateMachine`/`State`；Player、Orc、GrimReaper 各有独立状态类；`Stat` 属性系统（生命/攻击/护甲/暴击/元素等，装备可加 `AddModifier`） |
| `StaticData` + `Data` | 全部数值走 ScriptableObject：`GameResources.prefab` 引用 4 个 Manifest，运行时按 ID 取技能/装备/敌人/音频数据 |
| `PersistentData` | `GameDataModel`（金币/音量 → PlayerPrefs）；`PlayerDataModel` / `InventoryDataModel` / `SkillDataModel` 用 LitJson 序列化，经 `JSONManager` 存成 `persistentDataPath` 下的 `PlayerData.json` / `InventoryData.json` / `SkillData.json`（读不到会回退 StreamingAssets；WebGL 只持久化 PlayerPrefs 部分） |
| `UI` | `UIManager` 从 `Resources/UI/UIRoot` 实例化常驻 Canvas（Bottom / Middle / Top 三层），各界面由 Controller 按需创建/销毁 |

- 事件用静态类 `EventSubscriber`（如 `FromMenuSceneToGameScene`、`OnGameWin`、`OnPlayerDead`、`Equip`）。
- 音频/背包/技能都走「ID 枚举 + Manifest」：新增内容时先加 `.asset`，再挂到对应 Manifest。

## 第三方与资源

- **I2 Localization**：`Assets/Plugins/I2`，语言表 `Resources/I2Languages.asset`，源表 `Assets/CSV/I2Localization.csv`（菜单/加载界面使用 `I2.Loc`）。
- **Cinemachine 2.10.3**（Virtual Camera）、**TextMeshPro 3.0.9**、**2D Feature Set 2.0.1**。
- 美术：`Texture/Forest|Town|Characters|UI|FX`；动画 `Animations/`（含 `AnimationEvents/` 里的事件脚本）；音效 28MB 在 `Sound/`。

## 开发提示

- 场景 `MainGameScene` 里 `GameSceneGenerator` 会在 `PlayerSpawnPoint` 生成玩家；地形用 `Tilemap_Ground` / `Tilemap_Decoration`。
- 触发区域：`BossArea`（进入/离开切换 Boss 战）、`Portal`（通关）、`DeadZone`（掉出地图死亡）。
- 清档：主菜单 → 设置 → 清空存档（`DataManager.ClearJSONData()`），同时删除 `persistentDataPath` 下的 `PlayerData.json` / `InventoryData.json` / `SkillData.json`。
- 调试开关：`Assets/Data/GameConfigData.asset` 的 `isDebugMode`。
- 编辑器下 `SoundManager` 会把 BGM/SFX 音源挂到层级窗口，方便实时看播放状态。
