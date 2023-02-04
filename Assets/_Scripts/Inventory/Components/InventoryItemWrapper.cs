using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemWrapper
{
    public InventoryItemWrapper(ScriptableObject _data, ItemType _type)
    {
        itemData = _data;
        itemType = _type;
    }

    private ScriptableObject itemData;
    public ScriptableObject ItemData => itemData;

    private ItemType itemType;
    public ItemType ItemType => itemType;
}
