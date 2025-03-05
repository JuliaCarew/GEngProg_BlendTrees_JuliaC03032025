using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    private Rigidbody2D playerRigidbody;
    private Vector2 moveDirection;

    public Animator animator;
    
    void Awake()
    {
        animator = this.GetComponentInChildren<Animator>(); // on the child bc Sprite has Animator component
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        InputActions.MoveEvent += UpdateMoveVector;
    }
    private void Update()
    {
        HandleAnim();
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

    /// <summary>
    /// Swap between animations in the blend tree based on 
    /// horizontal/vertical input vector. Handles idle state as well.
    /// </summary>
    private void HandleAnim() 
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (movement.magnitude != 0)
        {
            animator.SetFloat("Horizontal", movement.y);
            animator.SetFloat("Vertical", movement.x);
            animator.SetBool("Moving",true);
        }
        else {
            animator.SetBool("Moving", false);
        }
    }
}
// spritesheet link https://opengameart.org/content/lpc-full-plate-golden-armor 