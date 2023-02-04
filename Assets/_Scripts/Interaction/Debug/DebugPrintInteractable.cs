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

    protected override void StartPrimaryInteractionImplement(GameObject _player)
    {
        Debug.Log(_player.gameObject.name + "initiated primary press on" + gameObject.name);
        FinishPrimaryInteraction(_player);
    }

    protected override void FinishPrimaryInteractionImplement(GameObject _player)
    {
        Debug.Log(_player.gameObject.name + "finished primary press on" + gameObject.name);
    }

    protected override void StartPrimaryActionHoldImpement(GameObject _player)
    {
        Debug.Log(_player.gameObject.name + "started primary hold on" + gameObject.name);
        isHeld = true;
    }

    protected override void StopPrimaryActionHoldImplement(GameObject _player)
    {
        Debug.Log(_player.gameObject.name + "stopped primary hold on" + gameObject.name);
        Debug.Log("held for " + heldTime);
        isHeld = false;
    }
}
