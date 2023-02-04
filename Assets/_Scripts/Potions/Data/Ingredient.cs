using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Potion/Ingredient")]
public class Ingredient : ScriptableObject
{
    [SerializeField]
    private PlantType plantType;
    public PlantType PlantType => plantType;

    [SerializeField]
    private ElementalType elementType = ElementalType.Neutral;
    public ElementalType ElementType => elementType;

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite => sprite;
}
