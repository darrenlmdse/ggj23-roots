using System;
using System.Collections.Generic;

public class Inventory
{
    private readonly List<InventorySlot> slots = new List<InventorySlot>();

    public int SlotCount => slots.Count;

    public delegate void SlotUpdateCallback(InventorySlot slot);
    public SlotUpdateCallback OnSlotAdded;
    public SlotUpdateCallback OnSlotRemoved;

    public InventorySlot CreateSlot()
    {
        InventorySlot newSlot = new InventorySlot(slots.Count);
        slots.Add(newSlot);

        OnSlotAdded?.Invoke(newSlot);
        return newSlot;
    }

    public void DestroyLastSlot()
    {
        slots[slots.Count - 1].Clear();
        OnSlotRemoved?.Invoke(slots[slots.Count - 1]);
        slots.RemoveAt(slots.Count - 1);
    }

    public void DestroySlot(InventorySlot _slot)
    {
        _slot.Clear();
        OnSlotRemoved?.Invoke(_slot);
        slots.Remove(_slot);
    }

    public void Clear()
    {
        slots.ForEach(slot => slot.Clear());
    }

    public void ForEach(Action<InventorySlot> _action)
    {
        slots.ForEach(slot => _action(slot));
    }

    public InventorySlot FindFirst(Predicate<InventorySlot> _predicate)
    {
        return slots.Find(_predicate);
    }

    public InventorySlot GetSlot(int _index)
    {
        if (_index >= slots.Count)
        {
            return null;
        }

        return slots[_index];
    }
}
