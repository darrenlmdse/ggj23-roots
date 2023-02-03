using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPlacementHandler : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject plantPrefab;
    [SerializeField] private GameObject rootPrefab;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        Ray ray = new Ray(transform.position + transform.forward, -Vector3.up);

        if (Physics.Raycast(ray, out RaycastHit hit, 2))
        {
            Instantiate(plantPrefab, hit.transform.position, Quaternion.identity);
            Instantiate(rootPrefab, hit.transform.position + offset, Quaternion.identity);
        }
    }
}
