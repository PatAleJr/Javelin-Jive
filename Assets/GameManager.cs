using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != this)
            Destroy(instance);

        instance = this;
    }
    public enum GameState { Title, Intermediate, Play, End};
    public GameState gameState;

    public int round;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI roundText;

    private float time;
    public float roundTime = 45;

    public GameObject GameScreen;
    public GameObject TitleScreen;
    public GameObject GameOverScreen;

    void Start() { 
        EnterTitleScreen();
    }

    public void EnterTitleScreen()
    {
        TitleScreen.SetActive(true);
        GameOverScreen.SetActive(false);
        GameScreen.SetActive(false);

        gameState = GameState.Title;
    }
    public void ExitTitleScreen()
    {
        GameScreen.SetActive(true);
        TitleScreen.SetActive(false);
        BeginRound();
    }

    public void BeginRound()
    {
        gameState = GameState.Play;
        time = roundTime;

        roundText.text = "Round " + round;
    }
    public void GameOver()
    {
        gameState = GameState.End;
        GameOverScreen.SetActive(true);
        GameScreen.SetActive(false);
    }


    public void Update()
    {
        if (gameState == GameState.Title) { 
            if (Input.GetKeyUp(KeyCode.Space))
                ExitTitleScreen();
        }

        if (gameState == GameState.Play)
        {
            time -= Time.deltaTime;
            timerText.text = time.ToString("#.00");

            if (time <= 0)
            {
                gameState = GameState.Play;
                round++;
                time = 0;
            }
        }
    }
}
