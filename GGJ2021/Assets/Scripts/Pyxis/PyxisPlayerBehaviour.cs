using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PyxisPlayerBehaviour : MonoBehaviour
{
    public int moveSpeed;

    private int dir;

    private void OnButton(InputValue value)
    {
        ChangeDir();
    }
    
    private void Start()
    {
        dir = 2;
    }
    
    void FixedUpdate()
    {
        Vector3 move;
        switch (dir)
        {
            case 0:
                move = new Vector3(0, moveSpeed * 0.01f, 0);
                break;

            case 1:
                move = new Vector3(moveSpeed * 0.01f, 0, 0);
                break;

            case 2:
                move = new Vector3(0, moveSpeed * -0.01f, 0);
                break;

            case 3:
                move = new Vector3(moveSpeed * -0.01f, 0, 0);
                break;

            default:
                move = Vector3.zero;
                Debug.Log("Impossible direction");
                break;
        }

        transform.position += move;
    }

    private void ChangeDir()
    {
        dir++;
        if (dir > 3)
        {
            dir = 0;
        }
    }
}
