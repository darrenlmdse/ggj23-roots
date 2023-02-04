using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveRoomHandler : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] wallRenderers;

    private const float FADE_DURATION = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<WalkParticleHandler>() == null)
        {
            return;
        }

        foreach (Renderer wall in wallRenderers)
        {
            StopAllCoroutines();
            StartCoroutine(WallDisabler());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<WalkParticleHandler>() == null)
        {
            return;
        }
        foreach (Renderer wall in wallRenderers)
        {
            StopAllCoroutines();
            StartCoroutine(WallEnabler());
        }
    }

    private IEnumerator WallEnabler()
    {
        Color colour = wallRenderers[0].material.color;

        while (colour.a < 1)
        {
            colour.a += Time.deltaTime / FADE_DURATION;

            foreach (Renderer wall in wallRenderers)
            {
                wall.material.color = colour;
            }

            yield return null;
        }

        foreach (Renderer wall in wallRenderers)
        {
            wall.material.SetFloat("_Mode", 0);
        }
    }

    private IEnumerator WallDisabler()
    {
        Color colour = wallRenderers[0].material.color;

        foreach (Renderer wall in wallRenderers)
        {
            wall.material.SetFloat("_Mode", 2);
        }

        while (colour.a > 0)
        {
            colour.a -= Time.deltaTime / FADE_DURATION;

            foreach (Renderer wall in wallRenderers)
            {
                wall.material.color = colour;
            }

            yield return null;
        }
    }
}
