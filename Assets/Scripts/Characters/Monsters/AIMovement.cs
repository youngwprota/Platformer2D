using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [Header("Sides detection range")]
    public float rightSideDetectionRange;
    public float leftSideDetectionRange;

    [Header("Player detection range")]
    public float rightPlayerDetectionRange;
    public float leftPlayerDetectionRange;

    [Header("Layer Mask")]
    public LayerMask sidesLayer;

    [Header("If on platform")]
    public Transform edgeDetection;

    [Header("Smooth Movement")]
    public float smoothTime = 0.1f;

    private Character characterScript;
    private Rigidbody2D rb;
    private bool moovingRight;
    private Vector2 velocity = Vector2.zero;
    private AnimController animControler;

    void Start()
    {
        characterScript = GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
        animControler = GetComponent<AnimController>();
        moovingRight = true;
    }

    void FixedUpdate()
    {
        if (edgeDetection != null)
        {
            HandleEdgeDetection();
        }
        else
        {
            HandleNormalDetection();
        }

        HandleMovement(moovingRight);
    }

    private void HandleEdgeDetection()
    {
        Debug.DrawRay(edgeDetection.position, Vector2.right * rightSideDetectionRange, Color.green);
        Debug.DrawRay(edgeDetection.position, Vector2.left * leftSideDetectionRange, Color.green);

        RaycastHit2D bottomRightRayToSide = Physics2D.Raycast(edgeDetection.position, Vector2.right, rightSideDetectionRange, sidesLayer);
        RaycastHit2D bottomLeftRayToSide = Physics2D.Raycast(edgeDetection.position, Vector2.left, leftSideDetectionRange, sidesLayer);

        if (bottomRightRayToSide.collider != null && bottomRightRayToSide.collider.CompareTag("Sides"))
        {
            moovingRight = false;
        }
        if (bottomLeftRayToSide.collider != null && bottomLeftRayToSide.collider.CompareTag("Sides"))
        {
            moovingRight = true;
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
            moovingRight = false;
        }
        if (leftRayToSide.collider != null && leftRayToSide.collider.CompareTag("Sides"))
        {
            moovingRight = true;
        }
    }

    private void HandleMovement(bool moovingRight)
    {
        if (moovingRight)
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
}
