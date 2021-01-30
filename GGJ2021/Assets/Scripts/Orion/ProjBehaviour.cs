using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjBehaviour : MonoBehaviour
{
    public int speed;

    private void FixedUpdate()
    {
        transform.position += new Vector3(speed * 0.01f, 0, 0);
        if (transform.position.x > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBehaviour>().Die();
            Destroy(gameObject);
        }
    }
}
