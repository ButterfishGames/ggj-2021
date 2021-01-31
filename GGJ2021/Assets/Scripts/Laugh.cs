using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laugh : MonoBehaviour
{
    public Sprite laugh0;
    public Sprite laugh1;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(LaughRtn());   
    }

    private IEnumerator LaughRtn()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.2f);
            sr.sprite = laugh0;
            yield return new WaitForSeconds(0.2f);
            sr.sprite = laugh1;
        }

        SceneManager.LoadScene(9);
    }
}
