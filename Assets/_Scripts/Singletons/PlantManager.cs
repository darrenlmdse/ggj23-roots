using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;

    [SerializeField]
    private GameObject plantPrefab;

    [SerializeField]
    private GameObject rootPrefab;

    private List<Transform> heads;
    private List<Transform> roots;

    [SerializeField]
    private Dictionary<PlantType, GameObject> plants;

    private const float kCompareDistance = 0.4f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        heads = new List<Transform>();
        roots = new List<Transform>();
    }

    public void PlantSeed(Vector3 plantPoint, PlantType p_plantType)
    {
        Instantiate(plantPrefab, plantPoint, Quaternion.identity);

        roots.Add(
            Instantiate(
                rootPrefab,
                plantPoint + GameSetupManager.Instance.Offset,
                Quaternion.identity
            ).transform
        );
    }

    public Transform GetClosestRoot(Vector3 enemyPosition)
    {
        if (roots.Count < 1)
        {
            return null;
        }

        int closestRootIndex = 0;
        float shortestDistance = (enemyPosition - roots[0].position).magnitude;

        for (int i = 1; i < roots.Count; i++)
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

    public bool HasPlantHead(Vector3 plantPos)
    {
        foreach (Transform head in heads)
        {
            if (Vector3.Distance(head.position, plantPos) <= kCompareDistance)
            {
                return true;
            }
        }

        return false;
    }
}
