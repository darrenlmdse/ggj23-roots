using Sirenix.OdinInspector;
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

    [SerializeField]
    private Renderer spotlight;

    [SerializeField]
    private float glowDuration;

    [SerializeField]
    private AnimationCurve glowCurve;

    [SerializeField]
    private Transform plantSpriteTransform;

    [SerializeField]
    private Vector3 squishScaleStart;

    [SerializeField]
    private Vector3 squishScaleEnd;

    [SerializeField]
    private float squishDuration;

    [SerializeField]
    private AnimationCurve squishCurve;

    private float timeElapsed;
    private Color colour;

    private void Awake()
    {
        colour = Color.white;
        spotlight.material.color = colour;
        //spotlight.material.SetColor("_EMISSION", colour);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health > 0)
        {
            StartCoroutine(SquishPlant());
            return;
        }

        DestroyThisPlant();
    }

    // root is responsible for this so Head should call root
    public void DestroyThisPlant()
    {
        head.SignalPlantDestroyed();
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
            FertilizeWithSlime(element);
            head.SetSlime(element);
            return true;
        }
        else
        {
            return false;
        }
    }

    [Button]
    public void FertilizeWithSlime(ElementalType slimeElement)
    {
        element = slimeElement;
        colour = Color.white;

        switch (element)
        {
            case ElementalType.Fire:
                colour = Color.red;
                break;
            case ElementalType.Leaf:
                colour = Color.yellow;
                break;
            case ElementalType.Water:
                colour = Color.blue;
                break;
        }

        spotlight.material.color = colour;
        //spotlight.material.SetColor("_EMISSION", colour);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime / glowDuration;

        colour.a = glowCurve.Evaluate(timeElapsed);
        spotlight.material.color = colour;

        if (timeElapsed >= 1)
        {
            timeElapsed -= 1;
        }
    }

    private IEnumerator SquishPlant()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / squishDuration;

            plantSpriteTransform.localScale = Vector3.Lerp(
                squishScaleStart,
                squishScaleEnd,
                squishCurve.Evaluate(t)
            );

            yield return null;
        }
    }
}
