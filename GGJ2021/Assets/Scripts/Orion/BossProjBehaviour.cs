using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjBehaviour : MonoBehaviour
{
    public bool sinPath;
    public int xSpeed;

    private float startX;
    
    private void Start()
    {
        startX = transform.position.x;
    }
    
    private void FixedUpdate()
    {
        transform.position = NextPos();
    }

    private Vector3 NextPos()
    {
        float x;
        float calcX;
        float y;

        x = transform.position.x - 0.01f * xSpeed;
        calcX = (x - startX) * 2 * Mathf.PI;
        y = sinPath ? RoundToPx(Mathf.Sin(calcX)) : -1 * RoundToPx(Mathf.Sin(calcX));
        y *= 0.55f;
        y -= 50;
        return new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<OrionPlayerBehaviour>().Die();
        }
    }

    private float RoundToPx(float x)
    {
        x *= 100;
        x = Mathf.Round(x);
        x /= 100;
        return x;
    }
}
