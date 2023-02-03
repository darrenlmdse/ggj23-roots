using UnityEngine;

public abstract class InteractableI : MonoBehaviour
{
    [SerializeField]
    private InteractionChannel interactionChannel;

    private bool isOverlapped;
    private GameObject player;

    private void Awake()
    {
        isOverlapped = false;
    }

    private void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isOverlapped = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOverlapped = false;
            player = null;
        }
    }

    public void StartInteraction()
    {
        interactionChannel.RaiseInteractableInitiated(this);
        StartInteractionImplement();
    }

    public void DoneInteraction()
    {
        interactionChannel.RaiseInteractableDone(this);
        DoneInteractionImplement();
    }

    protected abstract void StartInteractionImplement();
    protected abstract void DoneInteractionImplement();
}
