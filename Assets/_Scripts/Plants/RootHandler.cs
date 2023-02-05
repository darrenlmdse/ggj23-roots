using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootHandler : InteractableI
{
    private const float MAX_HEALTH = 20;

    [SerializeField]
    private GameObject plant;

    [SerializeField]
    private PlantHead head;

    [SerializeField]
    private float health = MAX_HEALTH;

    [SerializeField]
    private ElementalType element = ElementalType.Neutral;
    public ElementalType Element => element;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health > 0)
        {
            return;
        }

        DestroyThisPlant();
    }

    // root is responsible for this so Head should call root
    public void DestroyThisPlant()
    {
        PlantManager.Instance.RemoveRoot(transform);
        PlantManager.Instance.RemovePlantPoint(plant.transform.position);

        // destroy plant
        Destroy(plant);
    }

    public void Heal(float healing)
    {
        health += healing;
        if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }
    }

    protected override void StartPrimaryInteractionImplement(GameObject _player)
    {
        InventorySlot currentSlot = _player.GetComponent<InventoryHolder>().CurrentSelectedSlot;
        if (
            currentSlot == null
            || currentSlot.Item == null
            || currentSlot.Item.ItemType != ItemType.Slime
        )
        {
            return;
        }

        if (SupplySlime(currentSlot.Item.ItemData as SlimePart))
        {
            FinishPrimaryInteraction(_player);
        }
    }

    protected override void FinishPrimaryInteractionImplement(GameObject _player)
    {
        _player.GetComponent<InventoryHolder>().ClearCurrentSlot();
    }

    private bool SupplySlime(SlimePart slimePart)
    {
        // there's no neutral slime and we only accept the first slime part for now
        if (element == ElementalType.Neutral)
        {
            element = slimePart.ElementalType;
            head.SetSlime(element);
            return true;
        }
        else
        {
            return false;
        }
    }
}
