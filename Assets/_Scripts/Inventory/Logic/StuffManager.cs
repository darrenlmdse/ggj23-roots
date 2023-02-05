using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffManager : MonoBehaviour
{
    [SerializeField]
    private InteractionChannel interactionChannel;

    [SerializeField]
    private CombatChannel combatChannel;

    [SerializeField]
    private SlimeBook slimeBook;

    [SerializeField]
    private GameObject pickupPrefab;

    private void Start()
    {
        interactionChannel.OnPickupInteracted += InteractionChannel_OnPickupInteracted;
        interactionChannel.OnInventoryItemDiscarded += InteractionChannel_OnInventoryItemDiscarded;
        interactionChannel.OnPotionBrewed += InteractionChannel_OnPotionBrewed;
        interactionChannel.OnPlantHarvested += InteractionChannel_OnPlantHarvested;

        combatChannel.OnSlimeKilled += CombatChannel_OnSlimeKilled;
    }

    private void OnDestroy()
    {
        interactionChannel.OnPickupInteracted -= InteractionChannel_OnPickupInteracted;
        interactionChannel.OnInventoryItemDiscarded -= InteractionChannel_OnInventoryItemDiscarded;
        interactionChannel.OnPotionBrewed -= InteractionChannel_OnPotionBrewed;
        interactionChannel.OnPlantHarvested -= InteractionChannel_OnPlantHarvested;

        combatChannel.OnSlimeKilled -= CombatChannel_OnSlimeKilled;
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
        if (
            _player
                .GetComponent<InventoryHolder>()
                .TryAddingToInventory(new InventoryItemWrapper(_pu.PickupData, _pu.Type))
        )
        {
            _player.GetComponent<InteractionInstigator>().RemoveInteractable(_pu);

            _pu.FinishPrimaryInteraction(_player);
            interactionChannel.RaisePickupCollected(_player, _pu);
        }
    }

    private void InteractionChannel_OnPotionBrewed(GameObject _player, PotionData _potion)
    {
        PickupItemWrapper newPickup = Instantiate(
                pickupPrefab,
                _player.transform.position + new Vector3(-2f, 0.3f, -2f),
                Quaternion.identity
            )
            .GetComponent<PickupItemWrapper>();
        newPickup.SetData(_potion);
        newPickup.SetType(ItemType.Potion);
    }

    private void InteractionChannel_OnPlantHarvested(Vector3 plantPos, Ingredient ingredient)
    {
        int harvestCount = 2;
        if (ingredient.ElementType == ElementalType.Neutral)
        {
            harvestCount += 1;
        }
        int sign = 1;

        for (int i = 0; i < harvestCount; ++i)
        {
            PickupItemWrapper newPickup = Instantiate(
                    pickupPrefab,
                    plantPos + new Vector3((0f + ((float)i) / 2.5f) * sign, 0.3f, -0.1f),
                    Quaternion.identity
                )
                .GetComponent<PickupItemWrapper>();
            newPickup.SetData(ingredient);
            newPickup.SetType(ItemType.Ingredient);

            sign *= -1;
        }
    }

    private void CombatChannel_OnSlimeKilled(Vector3 pos, ElementalType type)
    {
        PickupItemWrapper newPickup = Instantiate(
                pickupPrefab,
                pos + new Vector3(0f, 0.3f, -0.1f),
                Quaternion.identity
            )
            .GetComponent<PickupItemWrapper>();
        newPickup.SetData(slimeBook.GetSlime(type));
        newPickup.SetType(ItemType.Slime);
    }
}
