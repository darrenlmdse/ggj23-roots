using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Channels/Interaction Channel")]
public class InteractionChannel : ScriptableObject
{
    /*COLLECTIBLES*/
    public delegate void InteractionCallbacl(InteractableI _interactable);

    public InteractionCallbacl OnPrimaryInteractableInitiated;
    public InteractionCallbacl OnPrimaryInteractableDone;

    public InteractionCallbacl OnSecondaryInteractableInitiated;
    public InteractionCallbacl OnSecondaryInteractableDone;

    public InteractionCallbacl OnPrimaryInteractableHoldStart;
    public InteractionCallbacl OnPrimaryInteractableHoldStop;
    public InteractionCallbacl OnPrimaryInteractableHoldComplete;

    public void RaisePrimaryInteractableInitiated(InteractableI _interactable)
    {
        OnPrimaryInteractableInitiated?.Invoke(_interactable);
    }

    public void RaisePrimaryInteractableDone(InteractableI _interactable)
    {
        OnPrimaryInteractableDone?.Invoke(_interactable);
    }

    public void RaiseSecondaryInteractableInitiated(InteractableI _interactable)
    {
        OnSecondaryInteractableInitiated?.Invoke(_interactable);
    }

    public void RaiseSecondaryInteractableDone(InteractableI _interactable)
    {
        OnSecondaryInteractableDone?.Invoke(_interactable);
    }

    public void RaisePrimaryInteractableHoldStart(InteractableI _interactable)
    {
        OnPrimaryInteractableHoldStart?.Invoke(_interactable);
    }

    public void RaisePrimaryInteractableHoldStop(InteractableI _interactable)
    {
        OnPrimaryInteractableHoldStop?.Invoke(_interactable);
    }

    public void RaisePrimaryInteractableHoldComplete(InteractableI _interactable)
    {
        OnPrimaryInteractableHoldComplete?.Invoke(_interactable);
    }
}
