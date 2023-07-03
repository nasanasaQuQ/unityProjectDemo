using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    private ItemDetails currentItem;
    private bool isSelected;
    public ItemTooltip tooltip;

    public void SetItem(ItemDetails itemDetails)
    {
        currentItem = itemDetails;
        gameObject.SetActive(true);
        itemImage.sprite = itemDetails.itemSprite;
        itemImage.SetNativeSize();
    }

    public void SetEmpty()
    {
        gameObject.SetActive(false);
    }
    
    
    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        EventHandler.CallOnItemSelectedEvent(currentItem, isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.UpdateItemName(currentItem.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
}
