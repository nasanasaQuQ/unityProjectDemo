using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActive : MonoBehaviour
{
    public ItemName requireItem;

    public bool isDone;

    public void CheckItem(ItemName itemName)
    {
        if (itemName == requireItem && !isDone)
        {
            isDone = true;
            OnClickedAction();
            EventHandler.CallItemUsedEvent(itemName);
        }

        
    }
    
    /// <summary>
    /// 默认是正确的方法执行
    /// </summary>
    protected virtual void OnClickedAction()
    {
        
    }


    public virtual void EmptyClick()
    {
        Debug.Log("test");
    }
    
}
