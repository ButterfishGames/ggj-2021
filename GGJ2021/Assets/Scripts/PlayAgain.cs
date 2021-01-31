using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    private void OnButton(InputValue value)
    {
        SceneManager.LoadScene(0);
    }

    private void OnQuit(InputValue value)
    {
        Application.Quit();
    }
}
