using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Potion/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField]
    private Potion potion;
    public Potion Potion => potion;

    [SerializeField]
    private List<KeyValuePair<Ingredient, int>> recipeItems;
    public List<KeyValuePair<Ingredient, int>> RecipeItems => recipeItems;
}
