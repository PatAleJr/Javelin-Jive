using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    public TextMeshProUGUI timerTextGame;
    public TextMeshProUGUI roundTextGame;

    public TextMeshProUGUI roundTextIntermediate;

    private float time;
    public float roundTime = 8;

    public GameObject GameScreen;
    public GameObject IntermediateScreen;
    public GameObject TitleScreen;
    public GameObject GameOverScreen;

    public GameObject roomPrefab;
    public Room currentRoom;
    private Room previousRoom;
    public int roomHeight;

    void Start() { 
        EnterTitleScreen();

        currentRoom = GameObject.FindWithTag("Room").GetComponent<Room>();
    }

    public void EnterTitleScreen()
    {
        TitleScreen.SetActive(true);
        GameOverScreen.SetActive(false);
        GameScreen.SetActive(false);
        IntermediateScreen.SetActive(false);

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
        if (previousRoom != null) Destroy(previousRoom.gameObject);

        time = roundTime;
        roundTextGame.text = "Round " + round;

        gameState = GameState.Play;
        GameScreen.SetActive(true);
        IntermediateScreen.SetActive(false);
    }

    public void TransitionRounds() {
        round++;
        time = 0;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            enemy.GetComponent<Enemy>().die();

        roundTextGame.text = "Round " + round;
        roundTextIntermediate.text = "Round " + round;

        gameState = GameState.Intermediate;
        IntermediateScreen.SetActive(true);
        GameScreen.SetActive(false);

        currentRoom.destroyBottomWalls();
        previousRoom = currentRoom;
        GameObject newRoom = Instantiate(roomPrefab);
        newRoom.transform.position = currentRoom.transform.position + new Vector3(0, -roomHeight, 0);
        currentRoom = newRoom.GetComponent<Room>();
        currentRoom.deactivateTopWalls();
    }

    public void EnteredNextRoom() {
        currentRoom.activateTopWalls();
        ScreenShake.instance.moveToDestination(currentRoom.transform.position, 50f);
    }
    public void GameOver()
    {
        gameState = GameState.End;
        GameOverScreen.SetActive(true);
        GameScreen.SetActive(false);
    }

    public void Update()
    {
        if (gameState == GameState.Title)
        {
            if (Input.GetKeyUp(KeyCode.Space))
                ExitTitleScreen();
        }
        else if (gameState == GameState.End) {
            if (Input.GetKeyUp(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (gameState == GameState.Play)
        {
            time -= Time.deltaTime;
            timerTextGame.text = time.ToString("#.00");

            if (time <= 0)
                TransitionRounds();
        }
    }
}
