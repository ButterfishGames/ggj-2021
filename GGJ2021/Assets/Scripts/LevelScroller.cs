using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    public int scrollSpeed;

    void FixedUpdate()
    {
        transform.position -= new Vector3(0.01f * scrollSpeed, 0, 0);
    }
}
