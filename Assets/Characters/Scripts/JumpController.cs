using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpController : MonoBehaviour
{
    PlayerControlBase controlBase;
    Rigidbody2D rb;
    [SerializeField] float jumpVelocity;
    [SerializeField] float fallModifier;
    [SerializeField] float minJumpModifier;

    [SerializeField] bool jumpPressed = false;
    [SerializeField] bool jumpReleased = false;

    private void Start()
    {
        controlBase = GetComponent<PlayerControlBase>();
        rb = controlBase.rb;
    }

    private void Update()
    {
        if (jumpPressed)
        {
            jumpReleased = false;
        }

        if (!jumpPressed && jumpReleased == false)
        {
            jumpReleased = true;
        }
    }

    private void FixedUpdate()
    {
        ModifyJump();
    }

    private void ModifyJump()
    {        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallModifier - 1) * Time.deltaTime;
        }        
        else if (rb.velocity.y > 0 && jumpReleased == true)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (minJumpModifier - 1) * Time.deltaTime;
        }        
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && controlBase.isGrounded)
        {
            Debug.Log("called");
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }

        if (ctx.performed)
        {
            jumpPressed = true;
        }
        else if (ctx.canceled)
        {
            jumpPressed = false;
        }
    }

}
