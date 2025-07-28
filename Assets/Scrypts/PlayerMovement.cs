using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private bool isMovingForward;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    public UiController controller;
    [SerializeField] Rigidbody2D anchorsRb;
    DistanceJoint2D distanceJoint;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerInput input;
    public GameObject gravePrefab;
    Rigidbody2D rb;
    public bool IsGrabbed = false;
    float horMove;
    bool isGrounded;
    public bool IsGrabbable = false;
    public bool IsMovingForward {  get { return isMovingForward; } } 

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        animator = GetComponent<Animator>();
        isMovingForward = true;
    }
    
    private void FixedUpdate()
    {
            rb.linearVelocity = new Vector2(horMove * speed, rb.linearVelocity.y);
            Flipper();
    }
    public void Movement(InputAction.CallbackContext context)
    {
            horMove = context.ReadValue<Vector2>().x;
            animator.SetBool("IsRunning", horMove != 0);
    }
    public void Jumping(InputAction.CallbackContext cxt)
    {
        if (cxt.performed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public void Grabbing(InputAction.CallbackContext cxt)
    {
        if (cxt.performed && IsGrabbable)
        {
            if (!IsGrabbed)
            {
                Grab();
            }
            else
            {
                distanceJoint.enabled = false;
                IsGrabbed = true;
                IsGrabbable = false;
            }
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fork") )
        {
            IsGrabbable = true;
        }
        if (collision.CompareTag("Finish"))
        {
            controller.Finished = true;
            controller.ShowUi();
            StopMoving();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void Flipper()
    {
        if (horMove != 0)
        {
            spriteRenderer.flipX = horMove < 0;
            isMovingForward = horMove > 0;
        }
    }
    public void Die()
    {
        Instantiate(gravePrefab, new Vector2(transform.position.x, transform.position.y + 15), Quaternion.identity);
        Destroy(gameObject);
        controller.ShowUi();
    }
    void Grab()
    {
        distanceJoint.enabled = true;
        distanceJoint.connectedBody = anchorsRb;
        distanceJoint.distance = 0.4f;
        anchorsRb.bodyType = RigidbodyType2D.Dynamic;
        IsGrabbed = true;
    }
    void StopMoving()
    {
        input.enabled = false;
        animator.SetBool("IsRunning",false);
    }
}
