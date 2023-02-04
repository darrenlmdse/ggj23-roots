using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TopsideSpawner : MonoBehaviour
{
    [SerializeField] private Material undergoundMaterial;
    [SerializeField] private Material surfaceMaterial;

    [Button]
    private void FixMaterials()
    {
        MeshRenderer[] tiles = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer tile in tiles)
        {
            if (tile.material == undergoundMaterial)
            {
                tile.material = surfaceMaterial;
            }
        }
    }
}
