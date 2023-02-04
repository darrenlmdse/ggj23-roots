using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : InteractableI
{
    [SerializeField]
    private List<Recipe> recipesBook;
    public List<Recipe> RecipesBook => recipesBook;

    private Dictionary<Ingredient, int> ingredientsPool;

    private void Awake()
    {
        ingredientsPool = new Dictionary<Ingredient, int>();
    }

    protected override void StartPrimaryInteractionImplement(GameObject _player)
    {
        throw new System.NotImplementedException();
    }

    protected override void FinishPrimaryInteractionImplement(GameObject _player)
    {
        throw new System.NotImplementedException();
    }

    private Potion TryMakePotion()
    {
        foreach (Recipe recipe in recipesBook)
        {
            Dictionary<Ingredient, int> pool = new Dictionary<Ingredient, int>();
            DeepCopyRecipesDictionary(ingredientsPool, pool);
            foreach (KeyValuePair<Ingredient, int> recipeItem in recipe.RecipeItems)
            {
                if (pool.ContainsKey(recipeItem.Key))
                {
                    pool[recipeItem.Key] -= 1;
                    if (pool[recipeItem.Key] == 0)
                    {
                        pool.Remove(recipeItem.Key);
                    }
                }
                else
                {
                    return null;
                }
            }
            if (pool.Count > 0)
            {
                return null;
            }
            else
            {
                return recipe.Potion;
            }
        }
        // if no matching recipe is found
        return null;
    }

    private void AddIngredient(Ingredient _ingredient)
    {
        if (ingredientsPool.ContainsKey(_ingredient))
        {
            ingredientsPool[_ingredient] += 1;
        }
        else
        {
            ingredientsPool.Add(_ingredient, 1);
        }
    }

    private List<KeyValuePair<Ingredient, int>> ClearAllIngredients()
    {
        List<KeyValuePair<Ingredient, int>> ret = new List<KeyValuePair<Ingredient, int>>();
        foreach (KeyValuePair<Ingredient, int> kvp in ingredientsPool)
        {
            ret.Add(kvp);
        }
        ingredientsPool.Clear();

        return ret;
    }

    private void DeepCopyRecipesDictionary(
        Dictionary<Ingredient, int> _source,
        Dictionary<Ingredient, int> _dest
    )
    {
        _dest.Clear();
        foreach (KeyValuePair<Ingredient, int> kvp in _source)
        {
            _dest.Add(kvp.Key, kvp.Value);
        }
    }

    // TODO(darren): implement.
    protected override void StartPrimaryActionHoldImpement(GameObject _player) { }

    protected override void StopPrimaryActionHoldImplement(GameObject _player) { }
}
