using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    private Rigidbody2D playerRigidbody;
    private Vector2 moveDirection;

    Animator animator;
    
    void Awake()
    {
        animator = this.GetComponent<Animator>();
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        InputActions.MoveEvent += UpdateMoveVector;
    }

    private void UpdateMoveVector(Vector2 inputVector) // player input = moveVector(current vector2 from HandlePlayerMove)
    {
        moveDirection = inputVector;
    }

    void HandlePlayerMove(Vector2 moveVector) // use .Move functionality to move player on set veriables, gets updated by UpdateMoveVector() method
    {
        playerRigidbody.MovePosition(playerRigidbody.position + moveVector * moveSpeed * Time.fixedDeltaTime);
    }

    private void FixedUpdate() // moving player by character controller component every frame
    {
        HandlePlayerMove(moveDirection);
    }
    private void OnDisable()
    {
        InputActions.MoveEvent -= UpdateMoveVector;
    }

    private void HandleAnim(Vector2 moveVector) // pass Vector2 ?
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // animator float based on movement direction to play directional anim
        animator.SetFloat("moveVector", horizontal);
        animator.SetFloat("moveVector", vertical);
    }
}
// pass through movement vector and switch based on horiz vert values
// spritesheet link https://opengameart.org/content/lpc-full-plate-golden-armor 