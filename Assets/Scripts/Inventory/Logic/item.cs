using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public ItemName itemName;

    public void OnItemClick()
    {   
        InventoryManager.Instance.AddItem(itemName);
        this.gameObject.SetActive(false);
    } 
}
