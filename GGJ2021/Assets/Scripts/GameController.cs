using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController singleton;

    public GameScreenRenderer screen;

    private bool gameStarted;
    private PlayerInput pIn;

    private void OnButton(InputValue value)
    {
        StartGame();
    }

    private void Start()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }

        gameStarted = false;
        pIn = GetComponent<PlayerInput>();
    }

    private void StartGame()
    {
        if (!gameStarted)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            pIn.enabled = false;
            gameStarted = true;
        }
    }

    public void Win()
    {
        // TODO: WIN
    }
}
