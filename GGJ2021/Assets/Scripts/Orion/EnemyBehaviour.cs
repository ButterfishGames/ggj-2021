using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;

    [Range(1,5)]
    public float deathAccel;

    private int dir = 1;

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, ySpeed * dir * 0.01f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("detected");
        if (collision.CompareTag("EnemyBound"))
        {
            dir *= -1;
            transform.position -= new Vector3(0.01f * xSpeed, 0, 0);
        }
    }

    public void Die()
    {
        EnemyBehaviour wave = transform.parent.GetComponent<EnemyBehaviour>();
        wave.ySpeed *= wave.deathAccel;
        Destroy(gameObject);
    }
}
