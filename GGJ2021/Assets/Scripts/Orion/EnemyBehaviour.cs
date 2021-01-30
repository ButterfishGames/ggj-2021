using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;

    [Range(0,2)]
    public float deathAccel;

    private int dir = 1;

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, ySpeed * dir * 0.01f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBound"))
        {
            if (transform.position.y+50 > 0)
            {
                dir = -1;
            }
            else
            {
                dir = 1;
            }
            transform.position -= new Vector3(0.01f * xSpeed, 0, 0);
        }

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<OrionPlayerBehaviour>().Die();
        }
    }

    public void Die()
    {
        EnemyBehaviour wave = transform.parent.GetComponent<EnemyBehaviour>();
        if (wave.transform.childCount > 1)
        {
            wave.ySpeed += wave.deathAccel;
            Destroy(gameObject);
        }
        else
        {
            OrionController.singleton.NextWave();
            Destroy(wave.gameObject);
        }
    }
}
