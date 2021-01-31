using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBehaviour : MonoBehaviour
{
    public bool canKill;

    public List<Vector3> turnPoints;

    private int dir;

    private PyxisPlayerBehaviour player;

    private void FixedUpdate()
    {
        Vector3 move;
        switch (dir)
        {
            case 0:
                move = new Vector3(0, player.moveSpeed * 0.01f, 0);
                break;

            case 1:
                move = new Vector3(player.moveSpeed * 0.01f, 0, 0);
                break;

            case 2:
                move = new Vector3(0, player.moveSpeed * -0.01f, 0);
                break;

            case 3:
                move = new Vector3(player.moveSpeed * -0.01f, 0, 0);
                break;

            default:
                move = Vector3.zero;
                Debug.Log("Impossible direction");
                break;
        }

        transform.position += move;
        if (turnPoints.Count > 0 && ApproximatePosition(transform.position, turnPoints[0]))
        {
            ChangeDir();
        }
    }

    private void ChangeDir()
    {
        dir++;
        if (dir > 3)
        {
            dir = 0;
        }

        turnPoints.RemoveAt(0);
    }

    private bool ApproximatePosition (Vector3 pos, Vector3 comp)
    {
        return pos.x > comp.x - 0.005f && pos.x < comp.x + 0.005f
            && pos.y > comp.y - 0.005f && pos.y < comp.y + 0.005f
            && pos.z > comp.z - 0.005f && pos.z < comp.z + 0.005f;
    }

    public int GetDir()
    {
        return dir;
    }

    public void SetDir(int newDir)
    {
        dir = newDir;
    }

    public void SetPlayer(PyxisPlayerBehaviour p)
    {
        player = p;
    }
}
