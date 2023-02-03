using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkParticleHandler : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private ParticleSystem dustParticles;

    private Vector3 previousPosition;

    private void Update()
    {
        if (transform.position == previousPosition)
        {
            return;
        }

        dustParticles.transform.position = transform.position + offset;
        dustParticles.Play();

        previousPosition = transform.position;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // TODO: This should be handled with physics layers instead
    //    GroundBlockCollisionHandler groundBlock = other.GetComponent<GroundBlockCollisionHandler>();

    //    if (groundBlock != null)
    //    {
    //        dustParticles.transform.position = groundBlock.transform.position + offset;
    //        dustParticles.Play();
    //    }
    //}
}
