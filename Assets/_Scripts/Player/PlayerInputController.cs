using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;

    private CharacterController controller;
    private Vector3 playerVelocity;

    private Vector2 movementInput = Vector2.zero;
    private bool action1 = false;
    private bool action2 = false;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnAction1(InputAction.CallbackContext context)
    {
        //action1 = context.ReadValue<bool>();
        action1 = context.action.triggered;
    }

    public void OnAction2(InputAction.CallbackContext context)
    {
        //action2 = context.ReadValue<bool>();
        action2 = context.action.triggered;
    }

    void Update()
    {
        HorizontalMove();
        if (action1)
        {
            PrimaryAction();
        }
        else if (action2)
        {
            SecondaryAction();
        }
    }

    private void HorizontalMove()
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
    }

    private void SecondaryAction()
    {
        Debug.Log(gameObject.name + " secondary action");
    }
}
