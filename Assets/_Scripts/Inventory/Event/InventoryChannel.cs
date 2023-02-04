using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Channels/Inventory Channel")]
public class InventoryChannel : ScriptableObject
{
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
}
