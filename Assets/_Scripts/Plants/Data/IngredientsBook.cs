using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Plants/IngredientsBook")]
public class IngredientsBook : ScriptableObject
{
    [SerializeField]
    public Ingredient CabbageFire;

    [SerializeField]
    public Ingredient CabbageLeaf;

    [SerializeField]
    public Ingredient CabbageNeutral;

    [SerializeField]
    public Ingredient CabbageWater;

    [SerializeField]
    public Ingredient CarrotFire;

    [SerializeField]
    public Ingredient CarrotLeaf;

    [SerializeField]
    public Ingredient CarrotNeutral;

    [SerializeField]
    public Ingredient CarrotWater;

    [SerializeField]
    public Ingredient EggplantFire;

    [SerializeField]
    public Ingredient EggplantLeaf;

    [SerializeField]
    public Ingredient EggplantNeutral;

    [SerializeField]
    public Ingredient EggplantWater;

    public Ingredient GetIngredient(PlantType pt, ElementalType et)
    {
        switch (pt)
        {
            case PlantType.Cabbage:
                switch (et)
                {
                    case ElementalType.Fire:
                        return CabbageFire;
                    case ElementalType.Leaf:
                        return CabbageLeaf;
                    case ElementalType.Neutral:
                        return CabbageNeutral;
                    case ElementalType.Water:
                        return CabbageWater;
                }
                break;
            case PlantType.Carrot:
                switch (et)
                {
                    case ElementalType.Fire:
                        return CarrotFire;
                    case ElementalType.Leaf:
                        return CarrotLeaf;
                    case ElementalType.Neutral:
                        return CarrotNeutral;
                    case ElementalType.Water:
                        return CarrotWater;
                }
                break;
            case PlantType.Eggplant:
                switch (et)
                {
                    case ElementalType.Fire:
                        return EggplantFire;
                    case ElementalType.Leaf:
                        return EggplantLeaf;
                    case ElementalType.Neutral:
                        return EggplantNeutral;
                    case ElementalType.Water:
                        return EggplantWater;
                }
                break;
        }
        return null;
    }
}
