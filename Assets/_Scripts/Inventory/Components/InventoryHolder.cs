using System;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField]
    private int slotCounts = 3;
    public int SlotCounts => slotCounts;

    [SerializeField]
    private InteractionChannel interactionChannel;

    public delegate void InventorySlotCallback(InventorySlot slot);

    // pick-up events
    public InventorySlotCallback OnInventoryItemAddedToInventory;

    // in-invetory events
    public InventorySlotCallback OnInventoryItemSelected;
    public InventorySlotCallback OnInventoryItemSwtichedSlot;

    // drop-off events
    public InventorySlotCallback OnInventoryItemUsed;
    public InventorySlotCallback OnInventoryItemRemovalInitiated;
    public InventorySlotCallback OnInventoryItemRemovedFromInventory;

    public void RaiseInventoryItemAddedToInventory(InventorySlot slot)
    {
        OnInventoryItemAddedToInventory?.Invoke(slot);
    }

    public void RaiseInventoryItemSwitchedSlot(InventorySlot slot)
    {
        OnInventoryItemSwtichedSlot?.Invoke(slot);
    }

    public void RaiseInventoryItemSelected(InventorySlot slot)
    {
        OnInventoryItemSelected?.Invoke(slot);
    }

    public void RaiseInventoryItemUsed(InventorySlot slot)
    {
        OnInventoryItemUsed?.Invoke(slot);
    }

    public void RaiseInventoryItemRemovalInitiated(InventorySlot slot)
    {
        OnInventoryItemRemovalInitiated?.Invoke(slot);
    }

    public void RaiseInventoryItemRemovedFromInventory(InventorySlot slot)
    {
        OnInventoryItemRemovedFromInventory?.Invoke(slot);
    }

    private Inventory inventory;
    public Inventory Inventory => inventory;
    private InventorySlot currentSelectedSlot;
    public InventorySlot CurrentSelectedSlot => currentSelectedSlot;

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
        RaiseInventoryItemAddedToInventory(slot);

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
            RaiseInventoryItemRemovalInitiated(discardingSlot);
            interactionChannel.RaiseInventoryItemDiscarded(gameObject, currentSelectedSlot.Item);
        }
    }

    public void FininshDiscardslot()
    {
        isDiscarding = false;
        discardingSlot.Clear();
        discardingSlot = null;
        RaiseInventoryItemRemovedFromInventory(discardingSlot);
    }

    public void ClearCurrentSlot()
    {
        RaiseInventoryItemRemovalInitiated(currentSelectedSlot);
        currentSelectedSlot.Clear();
        RaiseInventoryItemRemovedFromInventory(currentSelectedSlot);
    }

    private void SelectSlot(int _index)
    {
        if (_index >= slotCounts)
        {
            return;
        }
        currentSelectedSlot = inventory.GetSlot(_index);
        RaiseInventoryItemSelected(currentSelectedSlot);
    }
}
