using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItemWrapper : InteractableI
{
    [SerializeField]
    private ScriptableObject pickupData = null;
    public ScriptableObject PickupData => pickupData;

    [SerializeField]
    private ItemType type = ItemType.Empty;
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
        if (pickupData == null || type == ItemType.Empty)
        {
            return;
        }

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
}
