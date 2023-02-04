using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Potion/Potion")]
public class Potion : ScriptableObject
{
    [SerializeField]
    private string displayName;
    public string DisplayName => displayName;

    // TODO(darren): implement.
    // effects can go here or maybe just the scale value and maybe effects should be
    // determined by a Potion Manager
}
