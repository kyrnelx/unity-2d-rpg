using System;
using UnityEngine;

public class EventSubscriber
{
    public static Action FromMenuSceneToGameScene
    {
        get;
        set;
    }

    public static Action OnPlayerDead
    {
        get; 
        set;
    }

    public static Action ReloadGameScene
    {
        get; 
        set;
    }

    public static Action FromGameSceneToMenuScene
    {
        get; 
        set;
    }

    public static Action<InventoryItemBaseData> OnInventoryRealItemPickup
    {
        get; 
        set;
    }

    public static Action<InventoryItemBaseData> Equip
    {
        get;
        set;
    }

    public static Action<InventoryItemBaseData> UnEquip
    {
        get; 
        set;
    }

    public static Action<Transform> PlayerAttack
    {
        get;
        set;
    }
    
    public static Action<float> OnPlayerHealthChange
    {
        get;
        set;
    }

    public static Action OnGameWin
    {
        get;
        set;
    }

    public static Action OnCoinChange
    {
        get;
        set;
    }

    public static Action<Entity> PlayFleshVFX
    {
        get;
        set;
    }

    public static Action OnDeleteGameData
    {
        get;
        set;
    }
}