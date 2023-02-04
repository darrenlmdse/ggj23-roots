using UnityEngine;

public enum IngredientLevel
{
    Normal,
    Powered,
    Super,
}

[CreateAssetMenu(menuName = "Scriptable Objects/Potion/Ingredient")]
public class Ingredient : ScriptableObject
{
    [SerializeField]
    private string displayName;
    public string DisplayName => displayName;

    [SerializeField]
    private IngredientLevel level;
    public IngredientLevel Level => level;
}
