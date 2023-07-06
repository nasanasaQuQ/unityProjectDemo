using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<ItemDetails, int> UpdateUIEvent;

    public static event Action BeforeSceneUnloadEvent;
    
    public static event Action AfterSceneUnloadEvent;

    public static event Action<ItemDetails, bool> ItemSelectedEvent;

    public static event Action<ItemName> ItemUsedEvent;

    public static event Action<int> ChangeItemEvent;
    
    public static void CallUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        UpdateUIEvent?.Invoke(itemDetails, index);
    }

    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }
    
    public static void CallAfterSceneUnloadEvent()
    {
        AfterSceneUnloadEvent?.Invoke();
    }


    public static void CallOnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        ItemSelectedEvent?.Invoke(itemDetails, isSelected);
    }
    
    public static void CallItemUsedEvent(ItemName itemName)
    {
        ItemUsedEvent?.Invoke(itemName);
    }
    
    /// <summary>
    /// 添加物品触发
    /// </summary>
    /// <param name="itemName"></param>
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }

    public static event Action<string> ShowDialogueEvent;

    public static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }


    public static event Action<GameState> GameStateChangeEvent;
    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }

    public static event Action CheckGameStateEvent;

    public static void CallCheckGameStateEvent()
    {
        CheckGameStateEvent?.Invoke();
    }

    public static event Action<string> GamePassEvent;
    public static void CallGamePassEvent(string gameName)
    {
        GamePassEvent?.Invoke(gameName);
    }
}
