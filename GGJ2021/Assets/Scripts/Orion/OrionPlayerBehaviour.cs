using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OrionPlayerBehaviour : MonoBehaviour
{
    [Tooltip("Set to 0 to have no cooldown")]
    public float cooldownTime;

    [Tooltip("The amount of heat gained per shot. Cannon overheats at 1")]
    [Range(0, 1)]
    public float heatGain;

    [Tooltip("The amount of heat lost each frame.")]
    [Range(0, 1)]
    public float heatLoss;

    public GameObject projectile;

    private bool bufferedShot;
    private bool overheated;
    private float currTimer;
    private float currHeat;

    private Slider heatGauge;
    private Text overheatText;

    private void OnButton(InputValue value)
    {
        Debug.Log("test");
        if (!overheated)
        {
            if (currTimer <= 0)
            {
                Shoot();
            }
            else
            {
                bufferedShot = true;
            }
        }
    }

    private void Start()
    {
        bufferedShot = false;
        overheated = false;
        currTimer = 0.0f;

        heatGauge = GameObject.Find("HeatGauge").GetComponent<Slider>();
        overheatText = GameObject.Find("OverheatText").GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (currTimer > 0)
        {
            currTimer -= Time.deltaTime;
        }
        else if (bufferedShot)
        {
            bufferedShot = false;
            Shoot();
        }

        if (currHeat > 0)
        {
            currHeat -= heatLoss;
            heatGauge.value = currHeat;
        }
        else if (overheated)
        {
            overheated = false;
            overheatText.enabled = false;
        }
    }

    private void Shoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
        currTimer = cooldownTime;
        currHeat += heatGain;
        heatGauge.value = currHeat;

        if (currHeat >= 1)
        {
            currHeat = 1.5f;
            bufferedShot = false;
            overheated = true;
            overheatText.enabled = true;
        }
    }

    public void Die()
    {
        Destroy(gameObject); // TEMPORARY
    }
}
