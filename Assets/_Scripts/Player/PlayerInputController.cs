using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InteractionInstigator))]
public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;

    private CharacterController controller;
    private InteractionInstigator interactionInstigator;
    private Vector3 playerVelocity;

    private Vector2 movementInput = Vector2.zero;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        interactionInstigator = gameObject.GetComponent<InteractionInstigator>();
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
        // TODO(darren): implement.
        // we should disable movement
        _context.action.started += context =>
        {
            StartPrimaryActionHold();
        };
        /*
        _context.action.performed += context =>
        {
            FinishPrimaryActionHold();
        };
        */
        _context.action.canceled += context =>
        {
            StopPrimaryActionHold();
        };
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
            gameObject.transform.forward = move;
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
    }

    private void StartPrimaryActionHold()
    {
        Debug.Log(gameObject.name + " started primary action hold");
        interactionInstigator.StartPrimaryActionHold();
    }

    private void StopPrimaryActionHold()
    {
        Debug.Log(gameObject.name + " stopped primary action hold");
        interactionInstigator.StopPrimaryActionPress();
    }

    private void FinishPrimaryActionHold()
    {
        Debug.Log(gameObject.name + " finished primary action hold");
    }
}
