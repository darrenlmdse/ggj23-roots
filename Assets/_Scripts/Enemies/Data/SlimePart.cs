using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Potion/Slime")]
public class SlimePart : ScriptableObject
{
    [SerializeField]
    private ElementalType elementalType;
    public ElementalType ElementalType => elementalType;

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite => sprite;
}
