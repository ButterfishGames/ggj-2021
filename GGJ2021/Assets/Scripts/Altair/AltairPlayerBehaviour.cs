using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class AltairPlayerBehaviour : MonoBehaviour
{
    public int upSpeed;

    private bool goingUp;

    private PlayerInput pIn;

    public Sprite upSprite;
    public Sprite downSprite;

    public LevelScroller level;
    public SpriteRenderer sr;
    public TextMeshProUGUI tutText;

    private Rigidbody2D rb;

    private void OnButton(InputValue value)
    {
        goingUp = true;
        sr.sprite = upSprite;
        if (tutText.enabled)
        {
            tutText.enabled = false;
            rb.gravityScale = 0.5f;
            level.scrollSpeed = 2;
        }
    }

    private void OnRelease(InputValue value)
    {
        goingUp = false;
        sr.sprite = downSprite;
    }

    private void OnQuit(InputValue value)
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        pIn = GetComponent<PlayerInput>();
        level.scrollSpeed = 0;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bound"))
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        pIn.enabled = false;
        GameController.singleton.Continue();
    }
}
