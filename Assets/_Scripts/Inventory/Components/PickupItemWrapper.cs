using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemWrapper : InteractableI
{
    [SerializeField]
    private ScriptableObject pickupData;
    public ScriptableObject PickupData => pickupData;

    [SerializeField]
    private ItemType type;
    public ItemType Type => type;

    [SerializeField]
    private InteractionChannel interactionChannel;

    [SerializeField]
    private SpriteRenderer underlyingSprite_;

    private void Start()
    {
        RenderSprite();
    }

    private void RenderSprite()
    {
        switch (type)
        {
            case ItemType.Potion:
                underlyingSprite_.sprite = (pickupData as PotionData).Sprite;
                break;
            case ItemType.Ingredient:
                underlyingSprite_.sprite = (pickupData as Ingredient).Sprite;
                break;
            case ItemType.Slime:
                underlyingSprite_.sprite = (pickupData as SlimePart).Sprite;
                break;
            default:
                underlyingSprite_.sprite = null;
                break;
        }
    }

    protected override void StartPrimaryInteractionImplement(GameObject _player)
    {
        interactionChannel.RaisePickupInteracted(_player, this);
    }

    protected override void FinishPrimaryInteractionImplement(GameObject _player)
    {
        RemoveCollectible();
    }

    private void RemoveCollectible()
    {
        Destroy(this.gameObject);
    }

    public void SetData(ScriptableObject _data)
    {
        pickupData = _data;
        RenderSprite();
    }

    public void SetType(ItemType _type)
    {
        type = _type;
        RenderSprite();
    }

    protected override void StartPrimaryActionHoldImpement(GameObject _player)
    {
        return;
    }

    protected override void StopPrimaryActionHoldImplement(GameObject _player)
    {
        return;
    }
}
