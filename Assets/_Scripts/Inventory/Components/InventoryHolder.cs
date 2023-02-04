using System;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField]
    private int slotCounts = 3;
    public int SlotCounts => slotCounts;

    [SerializeField]
    private InteractionChannel interactionChannel;

    private Inventory inventory;
    public Inventory Inventory => inventory;
    private InventorySlot currentSelectedSlot;

    private bool isDiscarding;
    private InventorySlot discardingSlot;

    private void Awake()
    {
        inventory = new Inventory();
        isDiscarding = false;
    }

    private void Start()
    {
        interactionChannel.OnInventoryItemDropped += InteractionChannel_OnInventoryItemDropped;

        for (uint i = 0; i < slotCounts; ++i)
        {
            inventory.CreateSlot();
        }

        SelectSlot(0);
    }

    private void OnDestroy()
    {
        interactionChannel.OnInventoryItemDropped -= InteractionChannel_OnInventoryItemDropped;
    }

    private void InteractionChannel_OnInventoryItemDropped(
        GameObject _player,
        InventoryItemWrapper _pu
    )
    {
        if (_player == gameObject)
        {
            FininshDiscardslot();
        }
    }

    public bool TryAddingToInventory(InventoryItemWrapper _item)
    {
        InventorySlot slot = inventory.FindFirst(slot => slot.Item == null);
        if (slot == null)
        {
            Debug.Log("Inventory full!");
            return false;
        }
        slot.StoreItem(_item);
        return true;
    }

    public void SelectPreviousSlot()
    {
        int newIndex = currentSelectedSlot.Index - 1;
        if (newIndex < 0)
        {
            newIndex = slotCounts - 1;
        }
        SelectSlot(newIndex);
    }

    public void SelectNextSlot()
    {
        int newIndex = currentSelectedSlot.Index + 1;
        if (newIndex >= slotCounts)
        {
            newIndex = 0;
        }
        SelectSlot(newIndex);
    }

    public void StartDiscardSlot()
    {
        if (!isDiscarding && currentSelectedSlot.Item != null)
        {
            isDiscarding = true;
            discardingSlot = currentSelectedSlot;
            interactionChannel.RaiseInventoryItemDiscarded(gameObject, currentSelectedSlot.Item);
        }
    }

    public void FininshDiscardslot()
    {
        isDiscarding = false;
        discardingSlot.Clear();
        discardingSlot = null;
    }

    private void SelectSlot(int _index)
    {
        if (_index >= slotCounts)
        {
            return;
        }
        currentSelectedSlot = inventory.GetSlot(_index);
    }
}
