using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    [Header("Sides detection range")]
    public float rightSideDetectionRange;
    public float leftSideDetectionRange;
    public float bottomSideDetectionRange;

    [Header("Player detection range")]
    public float rightPlayerDetectionRange;
    public float leftPlayerDetectionRange;

    [Header("Layer Mask")]
    public LayerMask groundLayer;

    private Character characterScript;
    private Rigidbody2D rb;
    private bool moovingRight;
    private float smoothTime = 0.1f;
    private Vector2 velocity = Vector2.zero;
    private Vector3 characterRotaion;


    void Start()
    {
        characterScript = GetComponent<Character>();
        rb = GetComponent<Rigidbody2D>();
        moovingRight = true;
        characterRotaion = new Vector3(0, 0, 0);
    }
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.right * rightSideDetectionRange, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * leftSideDetectionRange, Color.green);
        Debug.DrawRay(transform.position, Vector2.down * bottomSideDetectionRange, Color.blue);

        RaycastHit2D rightRayToSide = Physics2D.Raycast(transform.position, Vector2.right, rightSideDetectionRange, groundLayer);
        RaycastHit2D leftRayToSide = Physics2D.Raycast(transform.position, Vector2.left, leftSideDetectionRange, groundLayer);
        RaycastHit2D bottomRayToSide = Physics2D.Raycast(transform.position, Vector2.down, bottomSideDetectionRange, groundLayer);

        if (rightRayToSide.collider != null)
        {
            Debug.Log("Right ray hit: " + rightRayToSide.collider.name + " with tag: " + rightRayToSide.collider.tag);
            if (rightRayToSide.collider.CompareTag("Sides"))
            {
                moovingRight = false;
                Debug.Log("Raycast hitting right side by " + gameObject.name);
            }
        }
        if (leftRayToSide.collider != null)
        {
            Debug.Log("Left ray hit: " + leftRayToSide.collider.name + " with tag: " + leftRayToSide.collider.tag);
            if (leftRayToSide.collider.CompareTag("Sides"))
            {
                moovingRight = true;
                Debug.Log("Raycast hitting left side by " + gameObject.name);
            }
        }
        if (bottomRayToSide.collider != null)
        {
            Debug.Log("Bottom ray hit: " + bottomRayToSide.collider.name + " with tag: " + bottomRayToSide.collider.tag);
            if (bottomRayToSide.collider.CompareTag("Sides"))
            {
                moovingRight = !moovingRight;
                Debug.Log("Raycast hitting bottom side by " + gameObject.name);
            }
        }

        HandleMovement(moovingRight);
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
        rb.velocity = new Vector2(
            Mathf.SmoothDamp(rb.velocity.x, characterScript.speed, ref velocity.x, smoothTime),
            rb.velocity.y
        );
        characterRotaion = new Vector3(0, 0, 0);
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(
            Mathf.SmoothDamp(rb.velocity.x, -characterScript.speed, ref velocity.x, smoothTime),
            rb.velocity.y
        );
        characterRotaion = new Vector3(0, 180, 0);
    }
}
