using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance => _instance;

    [SerializeField] private List<TowerSpawner> towerSpawners;
    [SerializeField] private List<EnemySpawner> enemySpawners;
    [SerializeField] private HealthHolder mainTower;
    [SerializeField] private List<GameObject> placementButtons;
    [SerializeField] private List<GameObject> fightingButtons;

    [SerializeField] private Renderer ground;
    [SerializeField] private Material placementMaterial;
    [SerializeField] private Material fightingMaterial;
    [SerializeField] private TextMeshProUGUI stateText;

    public GameObject VictoryScreen;
    public GameObject DefeatScreen;

    public Transform Ground; // for particleEffects

    private Dictionary<GameState, GameStateBase> gameStates;
    private GameStateBase _currentGameStateInstance;
    public GameState CurrentGameState;
    public bool GameResult;
    public bool DoubleSpeed;

    private const string FIGHT = "FIGHT";
    private const string PLACE = "PLACE";

    public void Awake()
    {
        _instance = this;
        gameStates = new Dictionary<GameState, GameStateBase>();
        gameStates.Add(GameState.Placement, new PlacementGameState());
        gameStates.Add(GameState.Fighting, new FightingGameState());
        gameStates.Add(GameState.Result, new ResultGameState());
        gameStates[GameState.Placement].Init();
        gameStates[GameState.Fighting].Init();
        gameStates[GameState.Result].Init();

        CurrentGameState = GameState.Placement;
        _currentGameStateInstance = gameStates[CurrentGameState];

        mainTower.DestroyedAction += OnMainTowerDestroyed;
    }

    public void ChangeState(GameState state)
    {
        _currentGameStateInstance.OnExit();
        CurrentGameState = state;
        _currentGameStateInstance = gameStates[CurrentGameState];
        _currentGameStateInstance.OnEnter();
    }

    public void ToggleGameStates()
    {
        switch (CurrentGameState)
        {
            case GameState.Placement:
                ground.material = fightingMaterial;
                foreach (var b in placementButtons)
                {
                    b.SetActive(false);
                }
                foreach (var b in fightingButtons)
                {
                    b.SetActive(true);
                }
                ChangeState(GameState.Fighting);
                stateText.text = PLACE;
                break;
            case GameState.Fighting:
                ground.material = placementMaterial;
                foreach (var b in placementButtons)
                {
                    b.SetActive(true);
                }
                foreach (var b in fightingButtons)
                {
                    b.SetActive(false);
                }
                ChangeState(GameState.Placement);
                stateText.text = FIGHT;
                break;
            default:
                break;
        }
    }

    private void OnMainTowerDestroyed(HealthHolder healthHolder)
    {
        GameResult = false;
        ChangeState(GameState.Result);
    }

    public void AllEnemiesAreDead()
    {
        GameResult = true;
        ChangeState(GameState.Result);
    }

    public List<TowerSpawner> GetTowerSpawners()
    {
        return towerSpawners;
    }

    public List<EnemySpawner> GetEnemySpawners()
    {
        return enemySpawners;
    }

    public void Update()
    {
        _currentGameStateInstance.OnUpdate();
    }

    public void CloseGameController()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    public void Speed2X(bool value)
    {
        DoubleSpeed = !DoubleSpeed;
        Time.timeScale = DoubleSpeed ? 2 : 1;
    }
}