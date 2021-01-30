using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LepusPlayerBehaviour : MonoBehaviour
{
    public int dirChangeTime;
    public float jumpForce;

    private bool canJump;
    private int dir;
    private int dirDir;
    private int currTime;

    private Rigidbody2D rb;

    private void OnButton(InputValue value)
    {
        Jump();
    }

    private void Start()
    {
        canJump = true;
        dir = 0;
        dirDir = 1;
        currTime = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currTime++;
        if (currTime >= dirChangeTime)
        {
            ChangeDir();
        }
    }

    private void Jump()
    {
        if (!canJump)
        {
            return;
        }

        Vector2 jump;
        switch (dir)
        {
            case -2:
                jump = new Vector2(-0.6f, 1).normalized;
                break;

            case -1:
                jump = new Vector2(-0.3f, 1).normalized;
                break;

            case 0:
                jump = new Vector2(0, 1);
                break;

            case 1:
                jump = new Vector2(0.3f, 1).normalized;
                break;

            case 2:
                jump = new Vector2(0.6f, 1).normalized;
                break;

            default:
                jump = Vector2.zero;
                Debug.Log("Impossible jump direction");
                break;
        }

        rb.AddForce(jump * jumpForce);
        canJump = false;
    }

    private void ChangeDir()
    {
        if (dir == 2 || dir == -2)
        {
            dirDir *= -1;
        }

        dir += dirDir;
        currTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && CheckForGround())
        {
            canJump = true;
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
