using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrionController : MonoBehaviour
{
    public static OrionController singleton;

    public float waveSpawnWait;

    public GameObject[] waves;
    public Text waveText;

    private int currWave;

    // Start is called before the first frame update
    void Start()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        currWave = 1;
        StartCoroutine(StartNextWave());
    }

    public void NextWave()
    {
        currWave++;
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        if (currWave > waves.Length)
        {
            GameController.singleton.Win();
        }
        else
        {

            if (currWave == waves.Length)
            {
                waveText.text = "FINAL WAVE";
            }
            else
            {
                waveText.text = "WAVE " + currWave;
            }
            waveText.enabled = true;

            yield return new WaitForSeconds(waveSpawnWait);

            Instantiate(waves[currWave - 1], new Vector3(0, -50, 0), Quaternion.identity);
            waveText.enabled = false;
        }
    }
}
