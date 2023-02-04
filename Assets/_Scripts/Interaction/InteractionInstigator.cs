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

    public void StartPrimaryActionPress()
    {
        if (isInteractionEnabled && HasNearbyInteractables())
        {
            int i = GetNearestInteractable();
            if (i > -1)
            {
                nearbyInteractables[i].StartPrimaryInteraction();
            }
        }
    }

    public void StartPrimaryActionHold()
    {
        if (isInteractionEnabled && HasNearbyInteractables())
        {
            int i = GetNearestInteractable();
            if (i > -1)
            {
                nearbyInteractables[i].StartPrimaryActionHold();
            }
        }
    }

    public void StopPrimaryActionPress()
    {
        if (isInteractionEnabled && HasNearbyInteractables())
        {
            int i = GetNearestInteractable();
            if (i > -1)
            {
                nearbyInteractables[i].StopPrimaryActionHold();
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

    private void OnTriggerEnter(Collider _other)
    {
        InteractableI interactable = _other.GetComponent<InteractableI>();
        if (interactable != null)
        {
            AddInteractable(interactable);
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        InteractableI interactable = _other.GetComponent<InteractableI>();
        if (interactable != null)
        {
            RemoveInteractable(interactable);
        }
    }

    private void AddInteractable(InteractableI _interactable)
    {
        nearbyInteractables.Add(_interactable);
    }

    private void RemoveInteractable(InteractableI _interactable)
    {
        nearbyInteractables.Remove(_interactable);
    }

    private InteractableI FindFirst(Predicate<InteractableI> _predicate)
    {
        return nearbyInteractables.Find(_predicate);
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
