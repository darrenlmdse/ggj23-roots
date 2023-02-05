using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO(darren): implement.
// extend InteractableI to feed slime here
public class RootHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject plant;

    [SerializeField]
    private float health = 20;

    [SerializeField]
    private ElementalType element = ElementalType.Neutral;

    [SerializeField]
    private Renderer spotlight;

    [SerializeField]
    private float glowDuration;

    [SerializeField]
    private AnimationCurve glowCurve;

    [SerializeField]
    private Transform plantSpriteTransform;

    [SerializeField]
    private Vector3 squishScaleStart;
    [SerializeField]
    private Vector3 squishScaleEnd;

    [SerializeField]
    private float squishDuration;

    [SerializeField]
    private AnimationCurve squishCurve;

    private float timeElapsed;
    private Color colour;

    private void Awake()
    {
        colour = Color.white;
        spotlight.material.color = colour;
        //spotlight.material.SetColor("_EMISSION", colour);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health > 0)
        {
            StartCoroutine(SquishPlant());
            return;
        }

        PlantManager.Instance.RemoveRoot(transform);
        PlantManager.Instance.RemovePlantPoint(plant.transform.position);

        // destroy plant
        Destroy(plant);
    }

    [Button]
    public void FertilizeWithSlime(ElementalType slimeElement)
    {
        element = slimeElement;
        colour = Color.white;

        switch (element)
        {
            case ElementalType.Fire:
                colour = Color.red;
                break;
            case ElementalType.Leaf:
                colour = Color.yellow;
                break;
            case ElementalType.Water:
                colour = Color.blue;
                break;
        }

        spotlight.material.color = colour;
        //spotlight.material.SetColor("_EMISSION", colour);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime / glowDuration;

        colour.a = glowCurve.Evaluate(timeElapsed);
        spotlight.material.color = colour;

        if (timeElapsed >= 1)
        {
            timeElapsed -= 1;
        }
    }

    private IEnumerator SquishPlant()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / squishDuration;

            plantSpriteTransform.localScale = Vector3.Lerp(squishScaleStart, squishScaleEnd, squishCurve.Evaluate(t));

            yield return null;
        }
    }
}
