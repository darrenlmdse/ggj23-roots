using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Channels/Combat Channel")]
public class CombatChannel : ScriptableObject
{
    public delegate void RootCombatCallback(RootHandler root);
    public RootCombatCallback OnRootAttacked;

    public delegate void PlantCombatCallback(PlantHead root);

    public PlantCombatCallback OnRootDestroyed;

    public void RaiseRootAttacked(RootHandler root)
    {
        OnRootAttacked?.Invoke(root);
    }

    public void RaiseRootDestroyed(PlantHead root)
    {
        OnRootDestroyed?.Invoke(root);
    }

    public delegate void SlimeCallback(Vector3 deadPos, ElementalType element);
    public SlimeCallback OnSlimeKilled;

    public void RaiseSlimeKilled(Vector3 deadPos, ElementalType element)
    {
        OnSlimeKilled?.Invoke(deadPos, element);
    }
}
