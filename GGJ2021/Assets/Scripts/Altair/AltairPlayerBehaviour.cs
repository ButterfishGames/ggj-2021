using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltairPlayerBehaviour : MonoBehaviour
{
    public int upSpeed;

    private bool goingUp;

    private Rigidbody2D rb;

    private void OnButton(InputValue value)
    {
        goingUp = true;
    }

    private void OnRelease(InputValue value)
    {
        goingUp = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (goingUp)
        {
            rb.velocity = new Vector2(0, upSpeed * 0.01f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject); // TODO: Implement full death code
    }
}
