using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : InteractableI
{
    [SerializeField]
    private PotionBook potionBook;

    private const int kIngredientPoolSize = 3;
    private List<Ingredient> ingredientsPool;

    private void Awake()
    {
        ingredientsPool = new List<Ingredient>();
    }

    private PotionData TryMakePotion()
    {
        if (ingredientsPool.Count != kIngredientPoolSize)
        {
            return null;
        }

        int fire = 0;
        int water = 0;
        int leaf = 0;
        for (int i = 0; i < kIngredientPoolSize; ++i)
        {
            if (ingredientsPool[i].PlantType != ingredientsPool[0].PlantType)
            {
                // only 3 of a kind
                return null;
            }
            switch (ingredientsPool[i].ElementType)
            {
                case ElementalType.Fire:
                    fire++;
                    break;
                case ElementalType.Water:
                    water++;
                    break;
                case ElementalType.Leaf:
                    leaf++;
                    break;
            }
        }

        BuffType buff = BuffType.Health;
        if (ingredientsPool[0].PlantType == PlantType.Carrot)
        {
            buff = BuffType.Speed;
        }
        else if (ingredientsPool[0].PlantType == PlantType.Eggplant)
        {
            buff = BuffType.Attack;
        }

        // TODO(darren): implement
        if (fire == water && fire == leaf)
        {
            return MakePotionData(ElementalType.Neutral, buff);
        }
        else if (fire > water && fire > leaf)
        {
            return MakePotionData(ElementalType.Fire, buff);
        }
        else if (water > fire && water > leaf)
        {
            return MakePotionData(ElementalType.Water, buff);
        }
        else
        {
            return MakePotionData(ElementalType.Leaf, buff);
        }
    }

    private PotionData MakePotionData(ElementalType element, BuffType buff)
    {
        switch (buff)
        {
            case BuffType.Health:
                switch (element)
                {
                    case ElementalType.Neutral:
                        return potionBook.kHealthNeutralPotion;
                    case ElementalType.Fire:
                        return potionBook.kHealthFirePotion;
                    case ElementalType.Water:
                        return potionBook.kHealthWaterPotion;
                    case ElementalType.Leaf:
                        return potionBook.kHealthLeafPotion;
                }
                break;
            case BuffType.Speed:
                switch (element)
                {
                    case ElementalType.Neutral:
                        return potionBook.kSpeedNeutralPotion;
                    case ElementalType.Fire:
                        return potionBook.kSpeedFirePotion;
                    case ElementalType.Water:
                        return potionBook.kSpeedWaterPotion;
                    case ElementalType.Leaf:
                        return potionBook.kSpeedLeafPotion;
                }
                break;
            case BuffType.Attack:
                switch (element)
                {
                    case ElementalType.Neutral:
                        return potionBook.kAttackNeutralPotion;
                    case ElementalType.Fire:
                        return potionBook.kAttackFirePotion;
                    case ElementalType.Water:
                        return potionBook.kAttackWaterPotion;
                    case ElementalType.Leaf:
                        return potionBook.kAttackLeafPotion;
                }
                break;
        }
        return null;
    }

    private bool TryAddIngredient(Ingredient _ingredient)
    {
        if (ingredientsPool.Count >= 3)
        {
            Debug.Log("Cauldron Full!");
            return false;
        }
        ingredientsPool.Add(_ingredient);
        return true;
    }

    private List<Ingredient> ClearAllIngredients()
    {
        List<Ingredient> ret = new List<Ingredient>();
        foreach (Ingredient i in ingredientsPool)
        {
            ret.Add(i);
        }
        ingredientsPool.Clear();

        return ret;
    }

    // TODO(darren): implement.
    protected override void StartPrimaryActionHoldImpement(GameObject _player) { }

    protected override void StopPrimaryActionHoldImplement(GameObject _player) { }

    protected override void StartPrimaryInteractionImplement(GameObject _player)
    {
        InventorySlot currentSlot = _player.GetComponent<InventoryHolder>().CurrentSelectedSlot;
        if (
            currentSlot == null
            || currentSlot.Item == null
            || currentSlot.Item.ItemType != ItemType.Ingredient
        )
        {
            return;
        }

        TryAddIngredient(currentSlot.Item.ItemData as Ingredient);
    }

    protected override void FinishPrimaryInteractionImplement(GameObject _player)
    {
        throw new System.NotImplementedException();
    }
}
