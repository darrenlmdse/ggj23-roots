using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPrintInteractable : InteractableI
{
    protected override void DoneInteractionImplement()
    {
        Debug.Log("interaction done " + gameObject.name);
    }

    protected override void StartInteractionImplement()
    {
        Debug.Log("interaction initiated " + gameObject.name);
    }
}
