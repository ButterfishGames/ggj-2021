using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScreenRenderer : MonoBehaviour
{
    private bool gameStarted;

    private GameObject gcObj;
    private RawImage img;
    private Rect correctRect;

    private void Start()
    {
        gameStarted = false;
        img = GetComponent<RawImage>();
    }

    private void Update()
    {
        if (gameStarted)
        {
            Debug.Log(gcObj.GetComponent<Camera>().rect);

            RenderTexture rt = new RenderTexture(196, 160, 24);

            Debug.Log(gcObj.GetComponent<Camera>().rect);

            Camera gameCam = gcObj.GetComponent<Camera>();

            Debug.Log(gcObj.GetComponent<Camera>().rect);

            gameCam.targetTexture = rt;

            Debug.Log(gcObj.GetComponent<Camera>().rect);

            img.texture = rt;

            Debug.Log(gcObj.GetComponent<Camera>().rect);

            gameCam.Render();

            Debug.Log(gcObj.GetComponent<Camera>().rect);
        }

        Resources.UnloadUnusedAssets();
    }

    public IEnumerator StartGame()
    {
        
        gcObj = null;
        while (gcObj == null)
        {
            gcObj = GameObject.Find("GameCam");
            yield return new WaitForEndOfFrame();
        }
        correctRect = gcObj.GetComponent<Camera>().rect;
        gameStarted = true;
    }
}
