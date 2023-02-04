using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Potion/PotionData")]
public class PotionData : ScriptableObject
{
    [SerializeField]
    private string displayName;
    public string DisplayName => displayName;

    [SerializeField]
    private ElementalType elementType;
    public ElementalType ElementType => elementType;

    [SerializeField]
    private BuffType buffType;
    public BuffType BuffType => buffType;

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite => sprite;
}
