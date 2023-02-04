using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Channels/Interaction Channel")]
public class InteractionChannel : ScriptableObject
{
    public delegate void PickupCallback(GameObject _player, PickupItemWrapper _pu);

    public PickupCallback OnPickupInteracted;
    public PickupCallback OnPickupCollected;

    public void RaisePickupInteracted(GameObject _player, PickupItemWrapper _pu)
    {
        OnPickupInteracted?.Invoke(_player, _pu);
    }

    public void RaisePickupCollected(GameObject _player, PickupItemWrapper _pu)
    {
        OnPickupCollected?.Invoke(_player, _pu);
    }

    public delegate void InventoryItemCallback(GameObject _player, InventoryItemWrapper _pu);

    public InventoryItemCallback OnInventoryItemDiscarded;
    public InventoryItemCallback OnInventoryItemDropped;

    public void RaiseInventoryItemDiscarded(GameObject _player, InventoryItemWrapper _item)
    {
        OnInventoryItemDiscarded?.Invoke(_player, _item);
    }

    public void RaiseInventoryItemDropped(GameObject _player, InventoryItemWrapper _item)
    {
        OnInventoryItemDropped?.Invoke(_player, _item);
    }

    public delegate void PotionCallback(GameObject _player, PotionData _potion);
    public PotionCallback OnPotionBrewed;

    public void RaisePotionBrewed(GameObject _player, PotionData _potion)
    {
        OnPotionBrewed?.Invoke(_player, _potion);
    }
}
