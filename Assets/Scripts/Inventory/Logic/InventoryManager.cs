using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>, ISaveable
{
    
    public ItemDataList_SO itemData;
    
    [SerializeField] public List<ItemName> itemList = new List<ItemName>();
    
    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterSceneUnloadEvent += OnAfterSceneUnloadEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;


    }
    

    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterSceneUnloadEvent -= OnAfterSceneUnloadEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;

    }

    private void OnStartNewGameEvent(int obj)
    {
        itemList.Clear();
    }
    
    private void OnAfterSceneUnloadEvent()
    {
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, -1);
        else
            for (var i = 0; i < itemList.Count; i++)
            {
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemList[i]),i);
            }
    }

    // 切换物品ui
    private void OnChangeItemEvent(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            ItemDetails itemDetails = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUIEvent(itemDetails, index);
        }
    }
    
    private void OnItemUsedEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);
        
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, -1);
    }

    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }

    public void AddItem(ItemName itemName)
    {   

        {
            if (!itemList.Contains(itemName))
            {
                itemList.Add(itemName);
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
            }
        }
        

    }

    private int GetItemIndex(ItemName itemName)
    {
        for (var i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == itemName)
                return i;
        }

        return -1;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData gameSaveData = new GameSaveData();
        gameSaveData.itemList = itemList;
        return gameSaveData;
    }

    public void RestoreGameData(GameSaveData gameSaveData)
    {
        itemList = gameSaveData.itemList;
    }
}
