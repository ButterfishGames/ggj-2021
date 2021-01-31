using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PyxisPlayerBehaviour player = collision.GetComponent<PyxisPlayerBehaviour>();
            bool hasFollows = player.samples.Count > 0;
            GameObject prevObj = hasFollows ? player.samples[player.samples.Count - 1] : player.gameObject;

            FollowerBehaviour follow = GetComponent<FollowerBehaviour>();
            follow.enabled = true;
            follow.SetPlayer(player);

            if (hasFollows)
            {
                FollowerBehaviour prevFollow = prevObj.GetComponent<FollowerBehaviour>();
                follow.SetDir(prevFollow.GetDir());
                follow.turnPoints = new List<Vector3>();
                for (int i = 0; i < prevFollow.turnPoints.Count; i++)
                {
                    follow.turnPoints.Add(prevFollow.turnPoints[i]);
                }
            }
            else
            {
                follow.SetDir(player.GetDir());
                follow.turnPoints = new List<Vector3>();
            }
            
            player.samples.Add(gameObject);

            switch (follow.GetDir())
            {
                case 0:
                    transform.position = prevObj.transform.position - new Vector3(0, 0.16f, 0);
                    break;

                case 1:
                    transform.position = prevObj.transform.position - new Vector3(0.16f, 0, 0);
                    break;

                case 2:
                    transform.position = prevObj.transform.position + new Vector3(0, 0.16f, 0);
                    break;

                case 3:
                    transform.position = prevObj.transform.position + new Vector3(0.16f, 0, 0);
                    break;
            }

            follow.canKill = hasFollows;
            player.CheckForWin();
            Destroy(this);
        }
    }
}
