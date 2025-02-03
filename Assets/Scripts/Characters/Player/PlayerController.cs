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

    public bool IsGrounded { get; private set; } = true;
    public bool MoveX { get; private set; } = false;
    public bool IsDashing { get; private set; } = false;
    public bool DashOn { get; private set; } = false;

    private void Awake()
    {

        player = GetComponent<Player>();
        
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimationsController>();
    }

    public void MoveForward()
    {
        if (IsDashing) return; 

        rb.velocity = new Vector2(player.speed, rb.velocity.y);
        transform.localScale = new Vector3(1, 1, 1);
        MoveX = true;
    }

    public void MoveBackward()
    {
        if (IsDashing) return; 

        rb.velocity = new Vector2(-player.speed, rb.velocity.y);
        transform.localScale = new Vector3(-1, 1, 1);
        MoveX = true;
    }

    public void Jump()
    {
        if (!IsGrounded || IsDashing) return;

        rb.velocity = new Vector2(rb.velocity.x, player.jumpHeight);
        IsGrounded = false;
    }

    public void Dash()
    {
        if (DashOn) return; 

        IsDashing = true;
        DashOn = true; 
        float direction = Mathf.Sign(transform.localScale.x);

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

    private void Update()
    {
        if (rb.velocity.x == 0)
            MoveX = false;

        if (Input.GetKey(moveForward))
            MoveForward();
        if (Input.GetKey(moveBackward))
            MoveBackward();

        if (Input.GetKeyDown(attack))
            Attack();

        if (Input.GetKeyDown(jump))
            Jump();

        if (Input.GetKeyDown(dash))
            Dash();

        animationController.EffectRunAnimation(MoveX);
        animationController.EffectJumpAnimation(IsGrounded, rb.velocity.y);
    }
}
