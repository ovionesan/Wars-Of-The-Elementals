using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MoveController : MonoBehaviour
{
    PlayerControlBase controlBase;
    Rigidbody2D rb;
    [SerializeField] float horizontalMoveValue;
    public float moveSpeed;

    private void Start()
    {
        controlBase = GetComponent<PlayerControlBase>();
        rb = controlBase.rb;
    }

    public void OnHorizontalMove(InputAction.CallbackContext ctx)
    {
        horizontalMoveValue = ctx.ReadValue<Vector2>().x;
    }

    private void FixedUpdate()
    {
        StopHorizontalMove();
        HandleHorizontalMove();
    }
    private void StopHorizontalMove()
    {
        if (horizontalMoveValue == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void HandleHorizontalMove()
    {
        rb.velocity = new Vector2(horizontalMoveValue * moveSpeed, rb.velocity.y);
    }
}
