using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{
    public int maxHealth;
    public int shotsPerWave;
    public float waveWait;
    public float shotWait;

    public Sprite main, shoot, die0, die1, die2, die3, die4;
    public GameObject proj;

    private bool sinPath;
    private int currHealth;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sinPath = true;
        sr = GetComponentInChildren<SpriteRenderer>();
        currHealth = maxHealth;

        StartCoroutine(BossAI());
    }

    private IEnumerator BossAI ()
    {
        while (currHealth > 0)
        {
            yield return new WaitForSeconds(waveWait);

            for (int i = 0; i < shotsPerWave; i++)
            {
                if (currHealth <= 0)
                {
                    break;
                }

                sr.sprite = shoot;
                Fire();
                yield return new WaitForSeconds(0.1f);
                sr.sprite = main;

                yield return new WaitForSeconds(shotWait);
            }
        }
    }

    private void Fire()
    {
        GameObject currProj = Instantiate(proj, transform.position, Quaternion.identity);
        currProj.GetComponent<BossProjBehaviour>().sinPath = sinPath;
        sinPath = !sinPath;
    }

    public void Damage()
    {
        currHealth--;

        if (currHealth == 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(DamageFlash());
        }
    }

    private IEnumerator DamageFlash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    private IEnumerator Die()
    {
        GetComponent<AudioSource>().Play();
        sr.sprite = die0;
        yield return new WaitForSeconds(0.4f);
        sr.sprite = die1;
        yield return new WaitForSeconds(0.4f);
        sr.sprite = die2;
        yield return new WaitForSeconds(0.4f);
        sr.sprite = die3;
        yield return new WaitForSeconds(0.4f);
        sr.sprite = die4;
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(10);
    }
}
