using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPrintInteractable : InteractableI
{
    private float heldTime;
    private bool isHeld;

    private void Awake()
    {
        heldTime = 0f;
        isHeld = false;
    }

    private void Update()
    {
        if (isHeld)
        {
            heldTime += Time.deltaTime;
        }
        else
        {
            heldTime = 0f;
        }
    }

    protected override void StartPrimaryInteractionImplement()
    {
        Debug.Log("primary press initiated " + gameObject.name);
        FinishPrimaryInteraction();
    }

    protected override void FinishPrimaryInteractionImplement()
    {
        Debug.Log("primary press done " + gameObject.name);
    }

    protected override void FinishSecondaryInteractionImplement()
    {
        Debug.Log("secondary interaction initiated " + gameObject.name);
    }

    protected override void StartSecondaryInteractionImplement()
    {
        Debug.Log("secondary interaction initiated " + gameObject.name);
        FinishSecondaryInteraction();
    }

    protected override void StartPrimaryActionHoldImpement()
    {
        Debug.Log("primary hold started " + gameObject.name);
        isHeld = true;
    }

    protected override void StopPrimaryActionHoldImplement()
    {
        Debug.Log("primary hold stopped " + gameObject.name);
        Debug.Log("primary hold for " + heldTime);
        isHeld = false;
    }
}
