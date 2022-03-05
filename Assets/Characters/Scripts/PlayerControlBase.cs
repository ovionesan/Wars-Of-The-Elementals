using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlBase : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isGrounded;

    Vector2 leftEdge;
    Vector2 rightEdge;
    Vector2 playerGroundPos;
    Vector2 rightCorner;
    Vector2 leftCorner;
    RaycastHit2D hasHitDown;
    RaycastHit2D hasHitRight;
    RaycastHit2D hasHitLeft;
    [SerializeField] public Transform groundedCheck;
    [SerializeField] float raycastLength = 0.1f;
    public LayerMask groundMasks;

    private void Awake()
    {
        InitExtentPositions();
        rb = GetComponent<Rigidbody2D>();
    }

    private void InitExtentPositions()
    {
        BoxCollider2D coll;
        coll = GetComponent<BoxCollider2D>();
        if (coll == null)
        {
            Debug.LogError("RB Collider not set.");
            return;
        }
        rightEdge = new Vector2(coll.bounds.max.x, coll.bounds.center.y);
        leftEdge = new Vector2(coll.bounds.min.x, coll.bounds.center.y);
        playerGroundPos = new Vector2(coll.bounds.center.x, coll.bounds.min.y);
        rightCorner = new Vector2(rightEdge.x, playerGroundPos.y);
        leftCorner = new Vector2(leftEdge.x, playerGroundPos.y);
    }

    
    private void FixedUpdate()
    {
        FireCasts();
        if (CheckIsGrounded()) { isGrounded = true; } else { isGrounded = false; }
    }

    private void FireCasts()
    {
        hasHitDown = Physics2D.Raycast(playerGroundPos, Vector2.down, raycastLength, groundMasks);
        hasHitRight = Physics2D.Raycast(rightCorner, new Vector2(1, -1), raycastLength, groundMasks);
        hasHitLeft = Physics2D.Raycast(leftCorner, new Vector2(-1, -1), raycastLength, groundMasks);
    }

    private bool CheckIsGrounded()
    {
        Debug.Log(hasHitDown && hasHitRight && hasHitLeft);
        if (!hasHitDown && !hasHitRight && !hasHitLeft) { return false; }
        if (Mathf.Abs(rb.velocity.y) > 0.1f) { return false; }
        return true;
    }
}
