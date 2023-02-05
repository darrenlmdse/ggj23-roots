using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Plants/PlantsBook")]
public class PlantsBook : ScriptableObject
{
    // CABBAGE
    [SerializeField]
    public Sprite cabbageYoung;

    [SerializeField]
    public Sprite cabbageGrown;

    [SerializeField]
    public Sprite cabbagePickup;

    // CARROT
    [SerializeField]
    public Sprite carrotYoung;

    [SerializeField]
    public Sprite carrotGrown;

    [SerializeField]
    public Sprite carrotPickup;

    // EGGPLANT
    [SerializeField]
    public Sprite eggplantYoung;

    [SerializeField]
    public Sprite eggplantGrown;

    [SerializeField]
    public Sprite eggplantPickup;
}
