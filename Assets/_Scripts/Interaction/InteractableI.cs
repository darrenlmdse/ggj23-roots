using UnityEngine;

public abstract class InteractableI : MonoBehaviour
{
    public void StartPrimaryInteraction(GameObject _player)
    {
        StartPrimaryInteractionImplement(_player);
    }

    public void FinishPrimaryInteraction(GameObject _player)
    {
        FinishPrimaryInteractionImplement(_player);
    }

    protected abstract void StartPrimaryInteractionImplement(GameObject _player);
    protected abstract void FinishPrimaryInteractionImplement(GameObject _player);

    public void StartPrimaryActionHold(GameObject _player)
    {
        StartPrimaryActionHoldImpement(_player);
    }

    public void StopPrimaryActionHold(GameObject _player)
    {
        StopPrimaryActionHoldImplement(_player);
    }

    protected abstract void StartPrimaryActionHoldImpement(GameObject _player);
    protected abstract void StopPrimaryActionHoldImplement(GameObject _player);
}
