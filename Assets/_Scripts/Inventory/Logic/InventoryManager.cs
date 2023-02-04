using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private InteractionChannel interactionChannel;

    [SerializeField]
    private GameObject pickupPrefab;

    private void Start()
    {
        interactionChannel.OnPickupInteracted += InteractionChannel_OnPickupInteracted;
        interactionChannel.OnInventoryItemDiscarded += InteractionChannel_OnInventoryItemDiscarded;
    }

    private void OnDestroy()
    {
        interactionChannel.OnPickupInteracted -= InteractionChannel_OnPickupInteracted;
        interactionChannel.OnInventoryItemDiscarded -= InteractionChannel_OnInventoryItemDiscarded;
    }

    private void InteractionChannel_OnInventoryItemDiscarded(
        GameObject _player,
        InventoryItemWrapper _item
    )
    {
        PickupItemWrapper newPickup = Instantiate(
                pickupPrefab,
                _player.transform.position,
                Quaternion.identity
            )
            .GetComponent<PickupItemWrapper>();
        newPickup.SetData(_item.ItemData);
        newPickup.SetType(_item.ItemType);

        interactionChannel.RaiseInventoryItemDropped(_player, _item);
    }

    private void InteractionChannel_OnPickupInteracted(GameObject _player, PickupItemWrapper _pu)
    {
        _player
            .GetComponent<InventoryHolder>()
            .TryAddingToInventory(new InventoryItemWrapper(_pu.PickupData, _pu.Type));

        _player.GetComponent<InteractionInstigator>().RemoveInteractable(_pu);

        _pu.FinishPrimaryInteraction(_player);
        interactionChannel.RaisePickupCollected(_player, _pu);
    }
}
