using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Potion/PotionBook")]
public class PotionBook : ScriptableObject
{
    // HEALTH
    [SerializeField]
    public PotionData kHealthNeutralPotion;

    [SerializeField]
    public PotionData kHealthFirePotion;

    [SerializeField]
    public PotionData kHealthLeafPotion;

    [SerializeField]
    public PotionData kHealthWaterPotion;

    // SPEED
    [SerializeField]
    public PotionData kSpeedNeutralPotion;

    [SerializeField]
    public PotionData kSpeedFirePotion;

    [SerializeField]
    public PotionData kSpeedLeafPotion;

    [SerializeField]
    public PotionData kSpeedWaterPotion;

    // ATTACK
    [SerializeField]
    public PotionData kAttackNeutralPotion;

    [SerializeField]
    public PotionData kAttackFirePotion;

    [SerializeField]
    public PotionData kAttackLeafPotion;

    [SerializeField]
    public PotionData kAttackWaterPotion;
}
