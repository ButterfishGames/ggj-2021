using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PyxisPlayerBehaviour : MonoBehaviour
{
    public int moveSpeed;

    public GameObject samplePrefab;
    public List<GameObject> samples;

    private int dir;

    private void OnButton(InputValue value)
    {
        ChangeDir();
    }

    private void OnQuit(InputValue value)
    {
        Application.Quit();
    }

    private void Start()
    { 
        samples = new List<GameObject>();
        dir = 2;
        SpawnNewSample();
    }
    
    private void FixedUpdate()
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

        for (int i = 0; i < samples.Count; i++)
        {
            samples[i].GetComponent<FollowerBehaviour>().turnPoints.Add(transform.position);
        }
    }

    public void CheckForWin()
    {
        if (samples.Count >= 10)
        {
            Win();
        }
        else
        {
            SpawnNewSample();
        }
    }

    private void SpawnNewSample()
    {
        float x = RoundToPx(Random.Range(-0.8f, 0.8f));
        float y = RoundToPx(Random.Range(-50.7f, -49.3f));

        if (Mathf.Abs(x - transform.position.x) >= 0.16f && Mathf.Abs(y - transform.position.y) >= 0.16f)
        {
            Vector3 spawnPos = new Vector3(x, y, 0);

            Instantiate(samplePrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            SpawnNewSample();
        }
    }

    private float RoundToPx(float x)
    {
        x *= 100;
        x = Mathf.Round(x);
        x /= 100;
        return x;
    }

    private void Win()
    {
        GameController.singleton.Win();
    }

    public int GetDir()
    {
        return dir;
    }
}
