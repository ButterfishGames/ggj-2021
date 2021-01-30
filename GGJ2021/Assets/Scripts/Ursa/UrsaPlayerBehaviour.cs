using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UrsaPlayerBehaviour : MonoBehaviour
{
    public float smallJumpForce;
    public float bigJumpForce;

    private bool canJump;

    private Rigidbody2D rb;

    private void OnTap(InputValue value)
    {
        Jump(false);
    }

    private void OnHold(InputValue value)
    {
        Jump(true);
    }
    
    void Start()
    {
        canJump = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Jump(bool isBig)
    {
        if (!canJump)
        {
            return;
        }

        float jumpForce = isBig ? bigJumpForce : smallJumpForce;
        rb.AddForce(Vector2.up * jumpForce);
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && CheckForGround())
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!CheckForGround())
        {
            canJump = false;
        }
    }

    private bool CheckForGround()
    {
        Vector3 leftCorner = transform.position + new Vector3(-0.14f, -0.14f, 0);
        Vector3 rightCorner = transform.position + new Vector3(0.14f, -0.14f, 0);

        RaycastHit2D hit0 = Physics2D.Raycast(leftCorner, Vector2.down, 0.2f);
        RaycastHit2D hit1 = Physics2D.Raycast(rightCorner, Vector2.down, 0.2f);

        return hit0.collider != null || hit1.collider != null;
    }
}
