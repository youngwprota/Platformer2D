using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [Header("Sides detection range")]
    public float rightSideDetectionRange;
    public float leftSideDetectionRange;

    [Header("Player detection range")]
    public float rightPlayerDetectionRange;
    public float leftPlayerDetectionRange;
    public float attackRange; 

    [Header("Layer Mask")]
    public LayerMask sidesLayer;
    public LayerMask playerLayer;

    [Header("If on platform")]
    public Transform edgeDetection;

    [Header("Smooth Movement")]
    public float smoothTime = 0.1f;

    [Header("Follow object")]
    public Transform player;

    private Character characterScript;
    private Rigidbody2D rb;
    private bool movingRight;
    private bool onPlayer;
    private Vector2 velocity = Vector2.zero;
    private AnimController animControler;
    private bool isDeathAnimationPlayed = false;

    void Start()
    {
        characterScript = GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
        animControler = GetComponent<AnimController>();
        movingRight = true;
    }

    void FixedUpdate()
    {
        if (characterScript.IsDead)
        {
            if (!isDeathAnimationPlayed) 
            {
                animControler.EffectDeathAnimation();
                isDeathAnimationPlayed = true;

                rb.velocity = Vector2.zero; 
                rb.bodyType = RigidbodyType2D.Static; 
            }
            return; 
        }

        DetectPlayer();

        if (onPlayer)
        {
            FollowPlayer();
        }
        else
        {
            if (edgeDetection != null)
            {
                HandleEdgeDetection();
            }
            else
            {
                HandleNormalDetection();
            }

            HandleMovement(movingRight);
        }
    }


    private void HandleEdgeDetection()
    {
        Debug.DrawRay(edgeDetection.position, Vector2.right * rightSideDetectionRange, Color.green);
        Debug.DrawRay(edgeDetection.position, Vector2.left * leftSideDetectionRange, Color.green);

        RaycastHit2D bottomRightRayToSide = Physics2D.Raycast(edgeDetection.position, Vector2.right, rightSideDetectionRange, sidesLayer);
        RaycastHit2D bottomLeftRayToSide = Physics2D.Raycast(edgeDetection.position, Vector2.left, leftSideDetectionRange, sidesLayer);

        if (bottomRightRayToSide.collider != null && bottomRightRayToSide.collider.CompareTag("Sides"))
        {
            movingRight = false;
        }
        if (bottomLeftRayToSide.collider != null && bottomLeftRayToSide.collider.CompareTag("Sides"))
        {
            movingRight = true;
        }
    }

    private void HandleNormalDetection()
    {
        Debug.DrawRay(transform.position, Vector2.right * rightSideDetectionRange, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * leftSideDetectionRange, Color.red);

        RaycastHit2D rightRayToSide = Physics2D.Raycast(transform.position, Vector2.right, rightSideDetectionRange, sidesLayer);
        RaycastHit2D leftRayToSide = Physics2D.Raycast(transform.position, Vector2.left, leftSideDetectionRange, sidesLayer);

        if (rightRayToSide.collider != null && rightRayToSide.collider.CompareTag("Sides"))
        {
            movingRight = false;
        }
        if (leftRayToSide.collider != null && leftRayToSide.collider.CompareTag("Sides"))
        {
            movingRight = true;
        }
    }

    private void HandleMovement(bool movingRight)
    {
        if (movingRight)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    private void MoveRight()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.velocity = new Vector2(
            Mathf.SmoothDamp(rb.velocity.x, characterScript.speed, ref velocity.x, smoothTime),
            rb.velocity.y
        );

        animControler.EffectRunAnimation((int)rb.velocity.x);
    }

    private void MoveLeft()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        rb.velocity = new Vector2(
            Mathf.SmoothDamp(rb.velocity.x, -characterScript.speed, ref velocity.x, smoothTime),
            rb.velocity.y
        );

        animControler.EffectRunAnimation((int)rb.velocity.x);
    }

    private void DetectPlayer()
    {
        RaycastHit2D rightRayToPlayer = Physics2D.Raycast(transform.position, Vector2.right, rightPlayerDetectionRange, playerLayer);
        RaycastHit2D leftRayToPlayer = Physics2D.Raycast(transform.position, Vector2.left, leftPlayerDetectionRange, playerLayer);

        if (rightRayToPlayer.collider != null && rightRayToPlayer.collider.CompareTag("Player"))
        {
            //Debug.Log("Find " + rightRayToPlayer.collider.name);
            onPlayer = true;
            movingRight = true;
        }
        else if (leftRayToPlayer.collider != null && leftRayToPlayer.collider.CompareTag("Player"))
        {
            //Debug.Log("Find " + rightRayToPlayer.collider.name);
            onPlayer = true;
            movingRight = false;
        }
        else
        {
            onPlayer = false;
        }
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else
        {
            if (player.position.x > transform.position.x)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
    }

    private void Attack()
    {
        rb.velocity = Vector2.zero;
        animControler.EffectAttackAnimation();
    }
}
