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

    public static event Action<ItemDetails> AddItemEvent;
    
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
    public static void CallAddItemEvent(ItemDetails itemDetails)
    {
        AddItemEvent?.Invoke(itemDetails);
    }

}
