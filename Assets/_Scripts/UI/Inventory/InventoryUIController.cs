using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField]
    private HorizontalLayoutGroup dynamicSlotsHolder_;

    [SerializeField]
    private GameObject inventorySlotPrefab_;

    [SerializeField]
    private Sprite normalOutlineSprite_;

    [SerializeField]
    private Sprite selectedOutlineSprite_;

    [SerializeField]
    private InventoryHolder inventoryHolder_;

    private void Awake() { }

    private void Start()
    {
        inventoryHolder_.OnInventoryItemAddedToInventory +=
            InventoryChannel_OnInventoryItemAddedToInventory;
        inventoryHolder_.OnInventoryItemSwtichedSlot +=
            InventoryChannel_OnInventoryItemSwtichedSlot;
        inventoryHolder_.OnInventoryItemRemovalInitiated +=
            InventoryChannel_OnInventoryItemRemovalInitiated;
        inventoryHolder_.OnInventoryItemSelected += InventoryChannel_OnInventoryItemSelected;
        inventoryHolder_.Inventory.OnSlotAdded += InventoryHolder_Inventory_OnSlotAdded;
        inventoryHolder_.Inventory.OnSlotRemoved += InventoryHolder_Inventory_OnSlotRemoved;
    }

    private void OnDestroy()
    {
        inventoryHolder_.OnInventoryItemAddedToInventory -=
            InventoryChannel_OnInventoryItemAddedToInventory;
        inventoryHolder_.OnInventoryItemSwtichedSlot -=
            InventoryChannel_OnInventoryItemSwtichedSlot;
        inventoryHolder_.OnInventoryItemRemovedFromInventory -=
            InventoryChannel_OnInventoryItemRemovalInitiated;
        inventoryHolder_.OnInventoryItemSelected -= InventoryChannel_OnInventoryItemSelected;
        inventoryHolder_.Inventory.OnSlotAdded -= InventoryHolder_Inventory_OnSlotAdded;
        inventoryHolder_.Inventory.OnSlotRemoved -= InventoryHolder_Inventory_OnSlotRemoved;
    }

    private void InventoryChannel_OnInventoryItemAddedToInventory(InventorySlot slot)
    {
        Assert.IsNotNull(slot.Item);
        SetSlotIconImage(slot);
    }

    private void InventoryChannel_OnInventoryItemSwtichedSlot(InventorySlot slot)
    {
        SetSlotIconImage(slot);
    }

    private void SetSlotIconImage(InventorySlot slot)
    {
        InventorySlotUIController slotUIController = GetInventorySlot(slot.Index);
        if (slot.Item != null)
        {
            switch (slot.Item.ItemType)
            {
                case ItemType.Potion:
                    slotUIController.SetIconImage((slot.Item.ItemData as PotionData).Sprite);
                    break;
                case ItemType.Ingredient:
                    slotUIController.SetIconImage((slot.Item.ItemData as Ingredient).Sprite);
                    break;
                case ItemType.Slime:
                    slotUIController.SetIconImage((slot.Item.ItemData as SlimePart).Sprite);
                    break;
                default:
                    slotUIController.SetIconImage(null);
                    break;
            }
        }
    }

    private void InventoryChannel_OnInventoryItemRemovalInitiated(InventorySlot slot)
    {
        InventorySlotUIController slotUIController = GetInventorySlot(slot.Index);
        slotUIController.SetIconImage(null);
    }

    private void InventoryChannel_OnInventoryItemSelected(InventorySlot slot)
    {
        InventorySlotUIController slotUIController = GetInventorySlot(slot.Index);
        DeselectAll();
        slotUIController.SetOutlineImage(selectedOutlineSprite_);
    }

    private void InventoryHolder_Inventory_OnSlotAdded(InventorySlot slot)
    {
        InventorySlotUIController newSlotUI = Instantiate(
                inventorySlotPrefab_,
                dynamicSlotsHolder_.transform
            )
            .GetComponent<InventorySlotUIController>();
        //newSlotUI.SetOutlineImage(normalOutlineSprite_);
        newSlotUI.DisableOutline();
        newSlotUI.SetIconImage(null);
    }

    private void InventoryHolder_Inventory_OnSlotRemoved(InventorySlot slot)
    {
        Destroy(GetInventorySlot(slot.Index).gameObject);
    }

    private InventorySlotUIController GetInventorySlot(int _index)
    {
        return dynamicSlotsHolder_.transform
            .GetChild(_index)
            .GetComponent<InventorySlotUIController>();
    }

    private void DeselectAll()
    {
        foreach (Transform t in dynamicSlotsHolder_.transform)
        {
            t.GetComponent<InventorySlotUIController>().DisableOutline();
        }
    }
}
