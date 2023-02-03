using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Channels/Interaction Channel")]
public class InteractionChannel : ScriptableObject
{
    /*COLLECTIBLES*/
    public delegate void InteractionCallbacl(InteractableI interactable);

    public InteractionCallbacl OnInteractableInitiated;
    public InteractionCallbacl OnInteractableDone;

    public void RaiseInteractableInitiated(InteractableI interactable)
    {
        OnInteractableInitiated?.Invoke(interactable);
    }

    public void RaiseInteractableDone(InteractableI interactable)
    {
        OnInteractableDone?.Invoke(interactable);
    }
}
