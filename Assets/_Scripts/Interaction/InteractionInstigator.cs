using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractionInstigator : MonoBehaviour
{
    private List<InteractableI> nearbyInteractables;
    private bool isInteractionEnabled;

    private void Awake()
    {
        nearbyInteractables = new List<InteractableI>();
        EnableInteraction();
    }

    public void StartPrimaryInteraction()
    {
        if (isInteractionEnabled && HasNearbyInteractables())
        {
            int i = GetNearestInteractable();
            if (i > -1)
            {
                nearbyInteractables[i].StartInteraction();
            }
        }
    }

    private bool HasNearbyInteractables()
    {
        return nearbyInteractables.Count != 0;
    }

    public void EnableInteraction()
    {
        isInteractionEnabled = true;
    }

    public void DisableInteraction()
    {
        isInteractionEnabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        InteractableI interactable = other.GetComponent<InteractableI>();
        if (interactable != null)
        {
            AddInteractable(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        InteractableI interactable = other.GetComponent<InteractableI>();
        if (interactable != null)
        {
            RemoveInteractable(interactable);
        }
    }

    private void AddInteractable(InteractableI interactable)
    {
        nearbyInteractables.Add(interactable);
    }

    private void RemoveInteractable(InteractableI interactable)
    {
        nearbyInteractables.Remove(interactable);
    }

    private InteractableI FindFirst(Predicate<InteractableI> predicate)
    {
        return nearbyInteractables.Find(predicate);
    }

    private int GetNearestInteractable()
    {
        float minDistance = float.MaxValue;
        int interactableId = -1;
        for (int i = 0; i < nearbyInteractables.Count; ++i)
        {
            InteractableI obj = nearbyInteractables[i];
            float newDistance = Vector3.Distance(obj.transform.position, this.transform.position);
            if (newDistance < minDistance)
            {
                minDistance = newDistance;
                interactableId = i;
            }
        }
        return interactableId;
    }
}
