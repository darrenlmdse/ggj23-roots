using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    public delegate void ItemChangeCallback(InventorySlot slot);

    private InventoryItemWrapper item_;
    public InventoryItemWrapper Item => item_;

    private int index_;
    public int Index => index_;

    public InventorySlot(int index)
    {
        index_ = index;
    }

    public void StoreItem(InventoryItemWrapper _item)
    {
        if (CanSlotContainItem(_item))
        {
            item_ = _item;
        }
        else
        {
            throw new FailedToMoveItemToSlotException();
        }
    }

    public void Clear()
    {
        item_ = null;
    }

    public bool CanSlotContainItem(InventoryItemWrapper _item)
    {
        return item_ == null;
    }
}
