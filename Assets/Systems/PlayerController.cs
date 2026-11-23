using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 moveInput;


    private Rigidbody2D playerRb;

    [SerializeField] private Animator playerAnimatorController;

    private int MoveInputXHash = Animator.StringToHash("MoveInputX");
    private int MoveInputYHash = Animator.StringToHash("MoveInputY");
    private int IsMovingHash = Animator.StringToHash("IsMoving");

    private int LastMoveInputXHash = Animator.StringToHash("LastMoveX");
    private int LastMoveInputYHash = Animator.StringToHash("LastMoveY");

    private int Emoteing = Animator.StringToHash("isEmoteing");

    private Vector2 LastInput;

    private bool isEmoteing = false;
    


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        if (playerRb == null) Debug.LogError("Rigidbody2D component not found on the player object.");

        playerAnimatorController = GetComponentInChildren<Animator>();

    }


    private void Update()
    {
        HandlePlayerAnimations();
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    private void LateUpdate()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        
    }
    public void OnEmote(InputAction.CallbackContext context)
    {
        isEmoteing = true;

    }

    private void HandlePlayerMovement()
    {
        playerRb.MovePosition(playerRb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void HandlePlayerAnimations()
    {
        if(moveInput != Vector2.zero)
        {
            playerAnimatorController.SetFloat(MoveInputXHash, moveInput.x);
            playerAnimatorController.SetFloat(MoveInputYHash, moveInput.y);
            playerAnimatorController.SetBool(IsMovingHash, true);
            LastInput = moveInput;
            playerAnimatorController.SetBool(Emoteing, false);


        }
        else
        {

            
            playerAnimatorController.SetBool(IsMovingHash, false);
            
            playerAnimatorController.SetFloat(LastMoveInputXHash, LastInput.x);
            
            
            playerAnimatorController.SetFloat(LastMoveInputYHash, LastInput.y);
            

            
            
        }
        if(Input.GetKeyDown(KeyCode.LeftControl) && moveInput == Vector2.zero)
        {
            playerAnimatorController.SetBool(Emoteing, true);
            
            Debug.Log("made it in");
        }
        
        
    }





}
















