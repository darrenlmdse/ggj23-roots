using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkParticleHandler : MonoBehaviour
{
    // Scalar is negative for rabbit
    [SerializeField] private float offsetScalar = 1;
    [SerializeField] private ParticleSystem dustParticles;

    private Vector3 previousPosition;

    private void Update()
    {
        if (transform.position == previousPosition)
        {
            return;
        }

        dustParticles.transform.position = transform.position + GameSetupManager.Instance.Offset * offsetScalar;
        dustParticles.Play();

        previousPosition = transform.position;
    }
}
