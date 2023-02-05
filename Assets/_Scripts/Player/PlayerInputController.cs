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

    [SerializeField]
    private PotionDrinker potionDrinker;

    private CharacterController controller;
    private InteractionInstigator interactionInstigator;
    private InventoryHolder inventoryHolder;
    private Vector3 playerVelocity;

    private Vector2 movementInput = Vector2.zero;

    private bool isMoving;
    private bool isAttacking;
    private bool canDrinkPotion;

    private Vector3 adjustedForward;
    private Vector3 adjustedRight;
    private Vector3 baseScale;

    public float SpeedModifier = 1f;

    private void Awake()
    {
        adjustedForward = (Vector3.forward + Vector3.right).normalized;
        adjustedRight = (-Vector3.forward + Vector3.right).normalized;
        baseScale = animator.transform.localScale;
        canDrinkPotion = false;
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        interactionInstigator = gameObject.GetComponent<InteractionInstigator>();
        inventoryHolder = gameObject.GetComponent<InventoryHolder>();

        if (potionDrinker != null)
        {
            canDrinkPotion = true;
        }
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
        _context.action.performed += context =>
        {
            if (canDrinkPotion)
            {
                InventorySlot currentSlot = inventoryHolder.CurrentSelectedSlot;
                if (
                    currentSlot == null
                    || currentSlot.Item == null
                    || currentSlot.Item.ItemType != ItemType.Potion
                )
                {
                    return;
                }
                if (
                    potionDrinker.DrinkPotion(
                        inventoryHolder.CurrentSelectedSlot.Item.ItemData as PotionData
                    )
                )
                {
                    inventoryHolder.ClearCurrentSlot();
                    // TODO: Play potion sound here
                }
            }
        };
    }

    void Update()
    {
        CheckHorizontalMove();
    }

    private void CheckHorizontalMove()
    {
        Vector3 move = adjustedForward * movementInput.y + adjustedRight * movementInput.x; //new Vector3(movementInput.x, 0, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed * SpeedModifier);

        if (movementInput.x > 0)
        {
            animator.transform.localScale = new Vector3(baseScale.x, baseScale.y, baseScale.z);
        }
        else if (movementInput.x < 0)
        {
            animator.transform.localScale = new Vector3(-baseScale.x, baseScale.y, baseScale.z);
        }

        if (move != Vector3.zero)
        {
            if (!isMoving)
            {
                isMoving = true;

                if (!isAttacking)
                {
                    animator.CrossFade("Run", 0.1f);
                }
            }

            //gameObject.transform.forward = move;
        }
        else if (isMoving)
        {
            isMoving = false;

            if (!isAttacking)
            {
                animator.CrossFade("Idle", 0.1f);
            }
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void StartPrimaryActionPress()
    {
        //Debug.Log(gameObject.name + " primary action press");
        interactionInstigator.StartPrimaryActionPress();
    }

    private void StartSecondaryActionPress()
    {
        //Debug.Log(gameObject.name + " secondary action press");
        StartCoroutine(WaitForAttackEnd());
        InventorySlot currentEquip = inventoryHolder.CurrentSelectedSlot;
        if (!canDrinkPotion)
        {
            if (currentEquip.Item != null && currentEquip.Item.ItemType == ItemType.Ingredient)
            {
                // try planting instead
                Ray ray = new Ray(transform.position, -Vector3.up);
                if (Physics.Raycast(ray, out RaycastHit hit, 2))
                {
                    Vector3 testPlantPos = hit.transform.position;
                    if (PlantManager.Instance.HasPlantHead(testPlantPos))
                    {
                        // cannot plant
                    }
                    else
                    {
                        // can plant
                        PlantManager.Instance.PlantSeed(
                            testPlantPos,
                            (currentEquip.Item.ItemData as Ingredient).PlantType
                        );
                        GetComponent<InventoryHolder>().ClearCurrentSlot();
                    }
                }
            }
        }
    }

    private void StartPrimaryActionHold()
    {
        //Debug.Log(gameObject.name + " started primary action hold");
        interactionInstigator.StartPrimaryActionHold();
    }

    private void StopPrimaryActionHold()
    {
        //Debug.Log(gameObject.name + " stopped primary action hold");
        interactionInstigator.StopPrimaryActionHold();
    }

    private void FinishPrimaryActionHold()
    {
        //Debug.Log(gameObject.name + " finished primary action hold");
    }

    private IEnumerator WaitForAttackEnd()
    {
        isAttacking = true;
        animator.CrossFade("Attack", 0.1f);
        attackEvent?.Invoke();

        yield return new WaitForSeconds(0.75f);
        isAttacking = false;
    }
}
