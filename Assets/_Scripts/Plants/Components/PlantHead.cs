using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHead : MonoBehaviour
{
    private PlantRoot myRoot;

    public delegate void HeadEventCallback();
    public HeadEventCallback OnHeadPlanted;
    public HeadEventCallback OnHeadHaversted;

    public void SetRoot(PlantRoot _root)
    {
        myRoot = _root;
    }
}
