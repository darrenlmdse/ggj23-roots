using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHead : InteractableI
{
    [SerializeField]
    private RootHandler root;
    public RootHandler Root => root;

    [SerializeField]
    private SpriteRenderer topSpriteRenderer;

    [SerializeField]
    private PlantsBook plantsBook;

    [SerializeField]
    private IngredientsBook ingredientsBook;

    [SerializeField]
    private InteractionChannel interactionChannel;

    [SerializeField]
    private CombatChannel combatChannel;

    private float growthTimeS = 5f;

    private float healPotionEffect = 5f;

    private PlantType plantType;

    private bool hasSlime;
    private bool hasGrown;

    private float timeElapsed;

    private void Awake()
    {
        hasSlime = false;
        hasGrown = false;
        timeElapsed = 0f;
    }

    private void Start()
    {
        RenderPlantHead();
    }

    private void Update()
    {
        if (!hasGrown)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= growthTimeS)
            {
                SetGrown();
            }
        }
    }

    public void SetUp(PlantType _plantType)
    {
        plantType = _plantType;
    }

    private void RenderPlantHead()
    {
        topSpriteRenderer.sprite = GetHeadSprite();
    }

    private Sprite GetHeadSprite()
    {
        switch (plantType)
        {
            case PlantType.Cabbage:
                return IsFullyGrowth() ? plantsBook.cabbageGrown : plantsBook.cabbageYoung;
            case PlantType.Carrot:
                return IsFullyGrowth() ? plantsBook.carrotGrown : plantsBook.carrotYoung;
            case PlantType.Eggplant:
                return IsFullyGrowth() ? plantsBook.eggplantGrown : plantsBook.eggplantYoung;
        }
        return null;
    }

    public void SetSlime(ElementalType slimeType)
    {
        hasSlime = true;
        RenderPlantHead();
    }

    public void SetGrown()
    {
        hasGrown = true;
        RenderPlantHead();
    }

    private bool IsFullyGrowth()
    {
        return hasSlime && hasGrown;
    }

    protected override void StartPrimaryInteractionImplement(GameObject _player)
    {
        // either harvest
        if (IsFullyGrowth())
        {
            interactionChannel.RaisePlantHarvested(
                this.transform.position,
                ingredientsBook.GetIngredient(plantType, root.Element)
            );
            _player.GetComponent<InteractionInstigator>().RemoveInteractable(this);
            root.DestroyThisPlant();
            return;
        }

        // or put potion in if available
        InventorySlot currentSlot = _player.GetComponent<InventoryHolder>().CurrentSelectedSlot;
        if (
            currentSlot == null
            || currentSlot.Item == null
            || currentSlot.Item.ItemType != ItemType.Potion
        )
        {
            return;
        }

        if (SupplyPotion(currentSlot.Item.ItemData as PotionData))
        {
            FinishPrimaryInteraction(_player);
        }
    }

    protected override void FinishPrimaryInteractionImplement(GameObject _player)
    {
        _player.GetComponent<InventoryHolder>().ClearCurrentSlot();
    }

    private bool SupplyPotion(PotionData potionData)
    {
        switch (potionData.BuffType)
        {
            case BuffType.Health:
                root.Heal(healPotionEffect);
                return true;

            case BuffType.Attack:
                combatChannel.RaiseRootAttacked(root);
                return true;

            case BuffType.Speed:
                SetGrown();
                return true;
        }

        return false;
    }

    public void SignalPlantDestroyed()
    {
        combatChannel.RaiseRootDestroyed(this);
    }
}
