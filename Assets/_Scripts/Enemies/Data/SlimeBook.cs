using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Books/SlimesBook")]
public class SlimeBook : ScriptableObject
{
    [SerializeField]
    public SlimePart fireSlime;

    [SerializeField]
    public SlimePart leafSlime;

    [SerializeField]
    public SlimePart waterSlime;

    public SlimePart GetSlime(ElementalType type)
    {
        switch (type)
        {
            case ElementalType.Fire:
                return fireSlime;
            case ElementalType.Leaf:
                return leafSlime;
            case ElementalType.Water:
                return waterSlime;
        }

        return null;
    }
}
