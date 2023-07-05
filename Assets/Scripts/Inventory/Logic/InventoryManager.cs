using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    
    public ItemDataList_SO itemData;
    
    [SerializeField] public List<ItemName> itemList = new List<ItemName>();
    
    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        var index = GetItemIndex(itemName);
        itemList.RemoveAt(index);
        
        if (itemList.Count == 0)
            EventHandler.CallUpdateUIEvent(null, -1);
    }

    public void AddItem(ItemName itemName)
    {   
        // 假设只有一个物体
        if (itemList.Count == 0)
        {
            if (!itemList.Contains(itemName))
            {
                itemList.Add(itemName);
                EventHandler.CallUpdateUIEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
            }
        }
        // 多个物体
        else
        {   
            Debug.Log("新添加了一个物品");
            itemList.Add(itemName);
            EventHandler.CallAddItemEvent(itemData.GetItemDetails(itemName));
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
    
}
