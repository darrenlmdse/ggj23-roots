using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InteractionInstigator))]
[RequireComponent(typeof(InventoryHolder))]
public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private UnityEvent attackEvent;

    private CharacterController controller;
    private InteractionInstigator interactionInstigator;
    private InventoryHolder inventoryHolder;
    private Vector3 playerVelocity;

    private Vector2 movementInput = Vector2.zero;

    private bool isMoving;
    private bool isAttacking;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        interactionInstigator = gameObject.GetComponent<InteractionInstigator>();
        inventoryHolder = gameObject.GetComponent<InventoryHolder>();
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        movementInput = _context.ReadValue<Vector2>();
    }

    public void OnAction1(InputAction.CallbackContext _context)
    {
        _context.action.performed += context =>
        {
            StartPrimaryActionPress();
        };
    }

    public void OnAction2(InputAction.CallbackContext _context)
    {
        _context.action.performed += context =>
        {
            StartSecondaryActionPress();
        };
    }

    public void OnAction1Hold(InputAction.CallbackContext _context)
    {
        /*
        // TODO(darren): implement.
        _context.action.started += context =>
        {
            StartPrimaryActionHold();
        };
        _context.action.canceled += context =>
        {
            StopPrimaryActionHold();
        };
        */
    }

    public void OnInventoryPrev(InputAction.CallbackContext _context)
    {
        _context.action.performed += context =>
        {
            inventoryHolder.SelectPreviousSlot();
        };
    }

    public void OnInventoryNext(InputAction.CallbackContext _context)
    {
        _context.action.performed += context =>
        {
            inventoryHolder.SelectNextSlot();
        };
    }

    public void OnInventoryDiscard(InputAction.CallbackContext _context)
    {
        _context.action.performed += context =>
        {
            inventoryHolder.StartDiscardSlot();
        };
    }

    public void OnInventoryUse(InputAction.CallbackContext _context)
    {
        // TODO(darren): implement.
    }

    void Update()
    {
        CheckHorizontalMove();
    }

    private void CheckHorizontalMove()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            if (!isMoving)
            {
                isMoving = true;

                if (!isAttacking)
                {
                    // animator.CrossFade("Run", 0.1f);
                }
            }

            //gameObject.transform.forward = move;
        }
        else if (isMoving)
        {
            isMoving = false;

            if (!isAttacking)
            {
                //animator.CrossFade("Idle", 0.1f);
            }
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void StartPrimaryActionPress()
    {
        Debug.Log(gameObject.name + " primary action press");
        interactionInstigator.StartPrimaryActionPress();
    }

    private void StartSecondaryActionPress()
    {
        Debug.Log(gameObject.name + " secondary action press");
        StartCoroutine(WaitForAttackEnd());
    }

    private void StartPrimaryActionHold()
    {
        Debug.Log(gameObject.name + " started primary action hold");
        interactionInstigator.StartPrimaryActionHold();
    }

    private void StopPrimaryActionHold()
    {
        Debug.Log(gameObject.name + " stopped primary action hold");
        interactionInstigator.StopPrimaryActionHold();
    }

    private void FinishPrimaryActionHold()
    {
        Debug.Log(gameObject.name + " finished primary action hold");
    }

    private IEnumerator WaitForAttackEnd()
    {
        isAttacking = true;
        // animator.CrossFade("Attack", 0.1f);
        attackEvent?.Invoke();

        yield return new WaitForSeconds(0.75f);
        isAttacking = false;
    }
}
