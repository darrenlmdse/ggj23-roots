using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHead : MonoBehaviour
{
    [SerializeField]
    private RootHandler root;

    [SerializeField]
    private SpriteRenderer topSpriteRenderer;

    [SerializeField]
    private Sprout sprout;

    [SerializeField]
    private float growthTimeS = 10f;

    [SerializeField]
    private PlantsBook plantsBook;

    private PlantType plantType;

    private bool hasSlime;
    private bool hasGrown;

    private void Awake()
    {
        hasSlime = false;
        hasGrown = false;
    }

    private void Start()
    {
        sprout.enabled = true;
        RenderPlantHead();
    }

    private void Update() { }

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
        // TODO(darren): implement.
        // modify elemental type
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
}
