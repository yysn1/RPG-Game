using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    protected StateMachine stateMachine;

    private bool isFacingRight = true;
    public int facingDir {  get; private set; } = 1;

    [Header("Collision detection")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected { get ; private set; }
    public bool wallDetected { get; private set; }

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine();
        
    }
    
    protected virtual void Start()
    {

    }

    private void Update()
    {
        HandleGroundDetection();
        HandleWallDetection();
        stateMachine.UpdataActiveState();
    }

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        HandleFlip(xVelocity);
    }

    private void HandleFlip(float xVelocity)
    {
        if (xVelocity > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (xVelocity < 0f && isFacingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        facingDir *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    private void HandleGroundDetection() => groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    private void HandleWallDetection()
    {
        if (secondaryWallCheck != null)
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * (isFacingRight ? 1 : -1), wallCheckDistance, whatIsGround)
                        && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * (isFacingRight ? 1 : -1), wallCheckDistance, whatIsGround);
        }
        else
        {
            wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * (isFacingRight ? 1 : -1), wallCheckDistance, whatIsGround);
        }
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x,groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(primaryWallCheck.position, new Vector3(primaryWallCheck.position.x + (wallCheckDistance * (isFacingRight ? 1 : -1)), primaryWallCheck.position.y));
        if (secondaryWallCheck != null)
            Gizmos.DrawLine(secondaryWallCheck.position, new Vector3(secondaryWallCheck.position.x + (wallCheckDistance * (isFacingRight ? 1 : -1)), secondaryWallCheck.position.y));
    }
}
