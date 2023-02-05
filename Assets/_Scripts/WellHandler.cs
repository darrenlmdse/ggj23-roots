using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellHandler : InteractableI
{
    [SerializeField]
    private Transform topTransform;

    [SerializeField]
    private Transform bottomTransform;

    [SerializeField]
    private GameObject pickupPrefab;

    [SerializeField]
    private float dropDuration;

    [SerializeField]
    private AnimationCurve dropCurve;

    [Button]
    public bool DropPotion(PotionData potionData)
    {
        GameObject spawnedPotion = Instantiate(
            pickupPrefab,
            topTransform.position,
            Quaternion.identity
        );
        PickupItemWrapper newPickup = spawnedPotion.GetComponent<PickupItemWrapper>();
        newPickup.SetData(potionData);
        newPickup.SetType(ItemType.Potion);

        StartCoroutine(AnimateDrop(spawnedPotion.transform));
        return true;
    }

    private IEnumerator AnimateDrop(Transform potion)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / dropDuration;

            potion.position = Vector3.Lerp(
                topTransform.position,
                bottomTransform.position,
                dropCurve.Evaluate(t)
            );

            yield return null;
        }
    }

    protected override void StartPrimaryInteractionImplement(GameObject _player)
    {
        InventorySlot currentSlot = _player.GetComponent<InventoryHolder>().CurrentSelectedSlot;
        if (
            currentSlot == null
            || currentSlot.Item == null
            || currentSlot.Item.ItemType != ItemType.Potion
        )
        {
            return;
        }

        if (DropPotion(currentSlot.Item.ItemData as PotionData))
        {
            FinishPrimaryInteraction(_player);
        }
    }

    protected override void FinishPrimaryInteractionImplement(GameObject _player)
    {
        _player.GetComponent<InventoryHolder>().ClearCurrentSlot();
    }
}
