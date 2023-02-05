using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO(darren): implement.
// extend InteractableI to feed slime here
public class RootHandler : InteractableI
{
    [SerializeField]
    private GameObject plant;

    [SerializeField]
    private float health = 20;

    [SerializeField]
    private ElementalType element = ElementalType.Neutral;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health > 0)
        {
            return;
        }

        PlantManager.Instance.RemoveRoot(transform);
        PlantManager.Instance.RemovePlantPoint(plant.transform.position);

        // destroy plant
        Destroy(plant);
    }

    protected override void StartPrimaryInteractionImplement(GameObject _player)
    {
        throw new System.NotImplementedException();
    }

    protected override void FinishPrimaryInteractionImplement(GameObject _player)
    {
        throw new System.NotImplementedException();
    }

    private void FeedSlime() { }
}
