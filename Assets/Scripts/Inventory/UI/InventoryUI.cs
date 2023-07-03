using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button leftButton, rightButton;

    public int currentIndex;

    public SlotUI slotUI;

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }

    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;

    }


    public void OnUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        if (itemDetails == null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDetails);
        }
        
        
    }
}
