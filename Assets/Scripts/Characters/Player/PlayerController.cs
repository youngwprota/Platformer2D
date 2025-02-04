using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Keyboard Settings")]
    public KeyCode moveForward;
    public KeyCode moveBackward;
    public KeyCode jump;
    public KeyCode dash;
    public KeyCode attack;

    private Player player;
    private Rigidbody2D rb;
    private PlayerAnimationsController animationController;
    private Vector3 playerRotaion;

    public bool IsGrounded { get; private set; } = true;
    public bool MoveX { get; private set; } = false;
    public bool IsDashing { get; private set; } = false;
    public bool DashOn { get; private set; } = false;

    private float targetVelocityX = 0f;
    private float smoothTime = 0.1f;
    private Vector2 velocity = Vector2.zero;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimationsController>();
        playerRotaion = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        transform.eulerAngles = playerRotaion;
        HandleMovement();

        if (Input.GetKeyDown(attack))
            Attack();

        if (Input.GetKeyDown(jump))
            Jump();

        if (Input.GetKeyDown(dash))
            Dash();

        animationController.EffectRunAnimation(MoveX);
        animationController.EffectJumpAnimation(IsGrounded, rb.velocity.y);
    }

    private void HandleMovement()
    {
        if (Input.GetKey(moveForward))
        {
            targetVelocityX = player.speed;
            playerRotaion = new Vector3(0, 0, 0);
        }
        else if (Input.GetKey(moveBackward))
        {
            playerRotaion = new Vector3(0, 180, 0);
            targetVelocityX = -player.speed;
        }
        else
        {
            targetVelocityX = 0f;
        }

        rb.velocity = new Vector2(
            Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref velocity.x, smoothTime),
            rb.velocity.y
        );
        MoveX = Mathf.Abs(targetVelocityX) > 0.1f;
    }

    public void Jump()
    {
        if (!IsGrounded || IsDashing) return;

        rb.velocity = new Vector2(rb.velocity.x, player.jumpHeight);
        // rb.AddForce(new Vector2(rb.velocity.x, player.jumpHeight), ForceMode2D.Impulse);
        IsGrounded = false;
    }

    public void Dash()
    {
        if (DashOn) return;

        IsDashing = true;
        DashOn = true;
        int direction = (playerRotaion == Vector3.zero) ? 1 : -1;

        rb.velocity = new Vector2(player.dashValue * direction, 0);
        animationController.EffectDashAnimation();

        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        rb.velocity = new Vector2(0, rb.velocity.y);
        IsDashing = false;

        yield return new WaitForSeconds(player.dashCooldown);
        DashOn = false;
    }

    public void Attack()
    {
        animationController.EffectAttackAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
        }
    }
}