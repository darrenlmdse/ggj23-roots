using UnityEngine;

public abstract class InteractableI : MonoBehaviour
{
    [SerializeField]
    private InteractionChannel interactionChannel;

    private GameObject player;

    public void StartPrimaryInteraction()
    {
        StartPrimaryInteractionImplement();
        interactionChannel.RaisePrimaryInteractableInitiated(this);
    }

    public void FinishPrimaryInteraction()
    {
        FinishPrimaryInteractionImplement();
        interactionChannel.RaisePrimaryInteractableDone(this);
    }

    protected abstract void StartPrimaryInteractionImplement();
    protected abstract void FinishPrimaryInteractionImplement();

    public void StartSecondaryInteraction()
    {
        StartSecondaryInteractionImplement();
        interactionChannel.RaiseSecondaryInteractableInitiated(this);
    }

    public void FinishSecondaryInteraction()
    {
        FinishSecondaryInteractionImplement();
        interactionChannel.RaiseSecondaryInteractableDone(this);
    }

    protected abstract void StartSecondaryInteractionImplement();
    protected abstract void FinishSecondaryInteractionImplement();

    public void StartPrimaryActionHold()
    {
        StartPrimaryActionHoldImpement();
        interactionChannel.RaisePrimaryInteractableHoldStart(this);
    }

    public void StopPrimaryActionHold()
    {
        StopPrimaryActionHoldImplement();
        interactionChannel.RaisePrimaryInteractableHoldStop(this);
    }

    protected abstract void StartPrimaryActionHoldImpement();
    protected abstract void StopPrimaryActionHoldImplement();
}
