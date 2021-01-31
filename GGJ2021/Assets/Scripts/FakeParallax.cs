using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeParallax : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position -= new Vector3(0.005f, 0, 0);
    }
}
