using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    
    public ItemDataList_SO itemData;

    public Button buttonName;
    
    public SlotUI slotUI;
    

    private int currentIndex;

    private int currentSizeMax;

    private int currentSizeMin = 0;
    private void Start()
    {
        buttonName = GetComponent<Button>();
        
        buttonName.onClick.AddListener(OnButtonClick);
    }

    

    /// <summary>
    /// 切换物品(如果有)
    /// </summary>
    public void OnButtonClick()
    {
        var itemlist = GetItemList();
        currentSizeMax = itemlist.Count;
        currentIndex = currentSizeMax + 1;
        if (currentSizeMax <= 1)
        {
            Debug.Log("pass");
        }
        else
        {
            Debug.Log(buttonName.name);
            switch (buttonName.name)
            {   
                case "Right Button":
                {
                    if (GlobalVar.NOWITEMIEDEX >= itemlist.Count)
                    {
                        Debug.Log("too mach");
                    }
                    else
                    {
                        if (GlobalVar.NOWITEMIEDEX < currentSizeMax - 1)
                        {
                            slotUI.ChangeItemSprite(itemData.GetItemDetails(itemlist[GlobalVar.NOWITEMIEDEX + 1] ));
                            GlobalVar.NOWITEMIEDEX++;
                        }

                    }
                    break;
                }
                
                case "Left Button":
                {   
                    if (currentIndex <= itemlist.Count)
                    {
                        Debug.Log("too slow");
                    }
                    else
                    {
                        if (GlobalVar.NOWITEMIEDEX > currentSizeMin)
                        {
                            slotUI.ChangeItemSprite(itemData.GetItemDetails(itemlist[GlobalVar.NOWITEMIEDEX - 1] ));
                            GlobalVar.NOWITEMIEDEX--;
                        }
                    }
                    break;
                    /*slotUI.ChangeItemSprite(itemData.GetItemDetails(itemlist[currentIndex - 1]));
                    break;*/
                }

            }
            
            //Debug.Log("message");
            //slotUI.ChangeItemSprite(itemData.GetItemDetails(itemlist[1]));
            //itemlist[-1].
            //slotUI.ChangeItemSprite(itemlist);
        }
    }

    private List<ItemName> GetItemList()
    {
        return InventoryManager.Instance.itemList;
    }
    
}
