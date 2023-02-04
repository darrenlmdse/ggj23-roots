using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;

    [SerializeField] private GameObject plantPrefab;
    [SerializeField] private GameObject rootPrefab;

    private List<Transform> roots;

    public enum PlantType
    {
        Pumpkin, Carrot, Potato
    }

    [SerializeField] private Dictionary<PlantType, GameObject> plants;

    public void PlantSeed(Vector3 plantPoint, PlantType p_plantType)
    {
        Instantiate(plantPrefab, plantPoint, Quaternion.identity);
        Instantiate(rootPrefab, plantPoint + GameSetupManager.Instance.Offset, Quaternion.identity);
    }

    public Transform GetClosestRoot(Vector3 enemyPosition)
    {
        if (roots.Count < 1)
        {
            return null;
        }

        int closestRootIndex = 0;
        float shortestDistance = (enemyPosition - roots[0].position).magnitude;

        for (int i=1; i<roots.Count; i++)
        {
            float distance = (enemyPosition - roots[0].position).magnitude;

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestRootIndex = i;
            }
        }

        return roots[closestRootIndex];
    }
}