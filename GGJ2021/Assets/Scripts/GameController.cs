using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    private readonly int[] GAME_IND_ARR = { 4, 5, 6, 7, 8 };
    private readonly int[] GAME_COST_ARR = { 1, 2, 4 };
    private readonly float[] SCREAM_TIME_ARR = { 0.2f, 1 };

    public static GameController singleton;

    public GameScreenRenderer screen;
    public List<GameObject> coins;

    public int currCost;

    private bool gameStarted;
    private bool continuing;
    private int remainingCost;
    private int currGame;

    private int[] games;

    private GameObject continueScreen;
    private PlayerInput pIn;

    private void OnButton(InputValue value)
    {
        InsertCoin();
    }

    private void OnQuit(InputValue value)
    {
        Application.Quit();
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

        games = new int[3];
        games[0] = 5; //GAME_IND_ARR[Random.Range(0, 4)];
        do
        {
            games[1] = GAME_IND_ARR[Random.Range(0, 4)];
        } while (games[1] == games[0]);
        games[2] = GAME_IND_ARR[4];

        currCost = 1;
        currGame = 0;
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

        gameStarted = false;
        pIn = GetComponent<PlayerInput>();
    }

    private void StartGame()
    {
        if (!gameStarted)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).buildIndex);
            SceneManager.LoadScene(games[currGame], LoadSceneMode.Additive);
            StartCoroutine(SwitchActiveScene());
            pIn.enabled = false;
            gameStarted = true;
        }
    }

    private IEnumerator SwitchActiveScene()
    {
        yield return new WaitForSeconds(0.1f);

        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
    }

    private void InsertCoin()
    {
        if (coins.Count <= 0)
        {
            return;
        }

        GetComponent<AudioSource>().Play();
        Destroy(coins[coins.Count - 1]);
        coins.RemoveAt(coins.Count - 1);

        currCost--;

        if (currCost == 0)
        {
            if (continuing)
            {
                Time.timeScale = 1;
                continuing = false;
                continueScreen.SetActive(false);
                gameStarted = false;
            }
            StartGame();
        }
        else
        {
            GameObject.Find("PayText").GetComponent<TextMeshProUGUI>().text = "insert " + currCost + " tokens";
        }
    }

    public void Continue()
    {
        StartCoroutine(ContinueScreen());
        continueScreen = FindContinueScreen();
        continueScreen.SetActive(true);
        currCost = GAME_COST_ARR[currGame];
        GameObject.Find("PayText").GetComponent<TextMeshProUGUI>().text = "insert " + currCost + " tokens";
        pIn.enabled = true;
    }

    private GameObject FindContinueScreen()
    {
        GameObject canvas = GameObject.Find("Canvas");
        RectTransform[] children = canvas.GetComponentsInChildren<RectTransform>(true);
        for (int i = 0; i < children.Length; i++)
        {
            Debug.Log(children[i].gameObject.name);
            if (children[i].gameObject.name.Equals("ContinueScreen"))
            {
                return children[i].gameObject;
            }
        }

        return null;
    }

    private IEnumerator ContinueScreen()
    {
        continuing = true;
        int countdown = 9;
        while (countdown > 0 && continuing)
        {
            yield return new WaitForSecondsRealtime(1);
            countdown--;
            if (continuing)
            {
                GameObject.Find("CountText").GetComponent<TextMeshProUGUI>().text = "" + countdown;
            }
        }

        if (continuing)
        {
            Lose();
        }
    }

    public void Win()
    {
        StartCoroutine(WinRtn());
    }

    private IEnumerator WinRtn()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).buildIndex);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);

        yield return new WaitForSeconds(SCREAM_TIME_ARR[currGame]);

        currGame++;

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).buildIndex);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

        currCost = GAME_COST_ARR[currGame];

        yield return new WaitForSeconds(0.05f);
        GameObject.Find("PayText").GetComponent<TextMeshProUGUI>().text = "insert " + currCost + " tokens";
        pIn.enabled = true;
        gameStarted = false;
    }

    public void Lose()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).buildIndex);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}
