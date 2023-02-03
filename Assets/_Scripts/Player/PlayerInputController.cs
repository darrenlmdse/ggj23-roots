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
        _context.action.performed += _context =>
        {
            PrimaryAction();
        };
    }

    public void OnAction2(InputAction.CallbackContext _context)
    {
        _context.action.performed += _context =>
        {
            SecondaryAction();
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

    private void PrimaryAction()
    {
        Debug.Log(gameObject.name + " primary action");
        interactionInstigator.StartPrimaryInteraction();
    }

    private void SecondaryAction()
    {
        Debug.Log(gameObject.name + " secondary action");
    }
}
