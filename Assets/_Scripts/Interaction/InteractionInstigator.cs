using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractionInstigator : MonoBehaviour
{
    [SerializeField]
    private CombatChannel combatChannel;

    private List<InteractableI> nearbyInteractables;
    private bool isInteractionEnabled;

    private void Awake()
    {
        nearbyInteractables = new List<InteractableI>();
        EnableInteraction();
    }

    private void Start()
    {
        combatChannel.OnRootDestroyed += CombatChannel_OnRootDestroyed;
    }

    private void OnDestroy()
    {
        combatChannel.OnRootDestroyed -= CombatChannel_OnRootDestroyed;
    }

    public bool StartPrimaryActionPress()
    {
        if (isInteractionEnabled && HasNearbyInteractables())
        {
            int i = GetNearestInteractable();
            if (i > -1)
            {
                nearbyInteractables[i].StartPrimaryInteraction(this.gameObject);
                return true;
            }
        }

        return false;
    }

    public void StartPrimaryActionHold()
    {
        if (isInteractionEnabled && HasNearbyInteractables())
        {
            int i = GetNearestInteractable();
            if (i > -1)
            {
                //nearbyInteractables[i].StartPrimaryActionHold(this.gameObject);
            }
        }
    }

    public void StopPrimaryActionHold()
    {
        if (isInteractionEnabled && HasNearbyInteractables())
        {
            int i = GetNearestInteractable();
            if (i > -1)
            {
                //nearbyInteractables[i].StopPrimaryActionHold(this.gameObject);
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

    public void AddInteractable(InteractableI _interactable)
    {
        if (!nearbyInteractables.Contains(_interactable))
        {
            nearbyInteractables.Add(_interactable);
            //Debug.Log("added: " + _interactable.name);
        }
    }

    public void RemoveInteractable(InteractableI _interactable)
    {
        nearbyInteractables.Remove(_interactable);
        //Debug.Log("remove: " + _interactable.name);
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

    private void CombatChannel_OnRootDestroyed(RootHandler root)
    {
        if (nearbyInteractables.Contains(root))
        {
            nearbyInteractables.Remove(root);
        }
    }
}
