using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlacementHandler : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        Ray ray = new Ray(transform.position + transform.forward, -Vector3.up);

        if (Physics.Raycast(ray, out RaycastHit hit, 2))
        {
            // TODO: Add plant type choices
            PlantManager.Instance.PlantSeed(hit.point, PlantType.Pumpkin);
        }
    }
}
