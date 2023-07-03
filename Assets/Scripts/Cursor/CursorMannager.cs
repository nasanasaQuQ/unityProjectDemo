using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMannager : MonoBehaviour
{

    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y ,0));

    private bool canClick;

    private ItemName currentItemName;

    public RectTransform hand;

    private bool holdItem;
    // 检测鼠标点击范围
    private Collider2D CheckObjectMousePosition()
    {

        return Physics2D.OverlapPoint(mouseWorldPos);
    }

    private void Update()
    {
        canClick = CheckObjectMousePosition();

        if (hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }
        
        if (canClick && Input.GetMouseButtonDown(0))
        {
            // 检测鼠标互动情况
            ClickAction(CheckObjectMousePosition().gameObject);
        }
    }

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;

    }

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;


    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        currentItemName = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }

    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        holdItem = isSelected;
        if (isSelected)
        {
            currentItemName = itemDetails.itemName;
        }
        hand.gameObject.SetActive(isSelected);
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "teleport":
            {
                var teleport = clickObject.GetComponent<Telport>();
                teleport?.TeleportToSence();
                break;
            }
                
            case "item":
            {
                var item = clickObject.GetComponent<item>();
                item?.OnItemClick();
                break;
            }

            case "InterActive":
            {
                var interActive = clickObject.GetComponent<InterActive>();
                if (holdItem)
                    interActive?.CheckItem(currentItemName);
                else
                    interActive?.EmptyClick();
                break;
            }


        }
    }
}
