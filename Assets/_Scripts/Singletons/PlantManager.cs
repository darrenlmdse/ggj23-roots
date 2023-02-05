using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;

    [SerializeField]
    private GameObject plantPrefab;

    //[SerializeField]
    //private GameObject rootPrefab;

    private List<Vector3> plantPoints;
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

        plantPoints = new List<Vector3>();
        roots = new List<Transform>();
    }

    public void PlantSeed(Vector3 plantPoint, PlantType p_plantType)
    {
        GameObject plant = Instantiate(plantPrefab, plantPoint, Quaternion.identity);
        plant.GetComponent<PlantHead>().SetUp(p_plantType);
        plantPoints.Add(plantPoint);
        roots.Add(plant.GetComponentInChildren<RootHandler>().transform);
    }

    public void RemoveRoot(Transform root)
    {
        if (!roots.Contains(root))
        {
            return;
        }

        roots.Remove(root);
    }

    public void RemovePlantPoint(Vector3 _plantPoint)
    {
        if (!plantPoints.Contains(_plantPoint))
        {
            return;
        }

        plantPoints.Remove(_plantPoint);
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
        foreach (Vector3 plantPoint in plantPoints)
        {
            if (Vector3.Distance(plantPoint, plantPos) <= kCompareDistance)
            {
                return true;
            }
        }

        return false;
    }
}
