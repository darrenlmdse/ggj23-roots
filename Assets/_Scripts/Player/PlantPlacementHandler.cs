using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlacementHandler : MonoBehaviour
{
    public void PlantSeed()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);

        if (Physics.Raycast(ray, out RaycastHit hit, 2))
        {
            //Debug.Log(hit.transform.name);
            // TODO: Add plant type choices
            PlantManager.Instance.PlantSeed(hit.transform.position, PlantType.Cabbage);
        }
    }
}
