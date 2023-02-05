using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellHandler : MonoBehaviour
{
    [SerializeField]
    private Transform topTransform;

    [SerializeField]
    private Transform bottomTransform;

    [SerializeField] private float dropDuration;
    [SerializeField] private AnimationCurve dropCurve;

    [Button]
    public void DropPotion(GameObject potionPrefab)
    {
        GameObject spawnedPotion = Instantiate(potionPrefab, topTransform.position, potionPrefab.transform.rotation);

        StartCoroutine(AnimateDrop(spawnedPotion.transform));
    }

    private IEnumerator AnimateDrop(Transform potion)
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / dropDuration;

            potion.position = Vector3.Lerp(topTransform.position, bottomTransform.position, dropCurve.Evaluate(t));

            yield return null;
        }
    }
}
