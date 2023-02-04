using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootHandler : MonoBehaviour
{
    [SerializeField] private GameObject plant;
    [SerializeField] private float health = 20;
    [SerializeField] private ElementalType element = ElementalType.Neutral;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health > 0)
        {
            return;
        }

        PlantManager.Instance.RemoveRoot(transform);

        // destroy plant
        Destroy(plant);
    }
}
