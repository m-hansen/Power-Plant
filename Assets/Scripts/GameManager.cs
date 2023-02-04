using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Do not add more than one of these in a scene
public class GameManager : Singleton<GameManager>
{
    public enum GameStateEnum
    {
        // TODO: initializing, game over, menus?
        MainMenu,
        Gameplay
    }

    [Header("Prefab Configuration")]
    [SerializeField] private PowerPlant powerPlant;
    [SerializeField] private CreepNode creep;

    [Header("Gameplay")]
    [SerializeField]
    private int startingPrimaryResource = 25;

    [Header("UI")]
    [SerializeField]
    private TMP_Text invokeText; // TODO make the timer know about the game manager and handle itself as an independent component

    [Header("Audio")]
    [SerializeField]
    [Tooltip("Primarily for development so we can easily handle the global audio stream from a convienent place.")]
    private bool isMutedOnStart = false;
    [SerializeField] private AudioClip backgroundMusic;

    private float timeIncrement;
    private float pauseIncrement;
    private float invokeTime;

    // We may not end up using this for the jam
    public GameStateEnum GameState { get; private set; } = GameStateEnum.Gameplay; // Do not modify this variable directly - call ChangeGameState instead

    public bool IsGamePaused { get; private set; }

    // Note nodes are not dynamic and will only be created/updated on launch
    // Nodes are currently SEttlements and the PowerPlant
    public List<Node> Nodes { get; private set; } 

    public PowerPlant Plant { get => powerPlant; }

    public CreepNode Creep { get => creep; }

    public int PrimaryResource { get; private set; } // TODO: name me

    private void Awake()
    {
        OnNewGame(); // we won't want to do this in awake oncee make a menu or start loading these resources in another (persistent) scene
    }

    private void Start()
    {
        if (isMutedOnStart)
        {
            AudioManager.Instance.Mute(true);
        }
    }

    private void OnNewGame()
    {
        PrimaryResource = startingPrimaryResource;
        RegisterAllNodes();
        AudioManager.Instance.PlayMusic(backgroundMusic);

        StartTimer();
    }

    // TODO: We should put the timer bback into its own class and reference a handle to the timer from the game manager
    private void StartTimer()
    {
        //Starts timer
        InvokeRepeating("InvokeTimer", 0, timeIncrement);
        IsGamePaused = false;
    }

    public void PauseTimer()
    {
        pauseIncrement = timeIncrement;
        timeIncrement = 0;
        IsGamePaused = true;
    }

    public void UnpauseTimer()
    {
        timeIncrement = pauseIncrement;
        IsGamePaused = false;
    }

    private void InvokeTimer()
    {
        int minutes = (int)(invokeTime / 60) % 60;
        int seconds = (int)(invokeTime % 60);
        invokeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        invokeTime += timeIncrement;
    }

    private void RegisterAllNodes()
    {
        Nodes = new List<Node>(FindObjectsOfType<Node>());
    }

    // TODO : We may need/want to make this public in the future. For now, we don't need to.
    private void ChangeGameState(GameStateEnum newState)
    {
        if (newState != GameState)
        {
            Debug.Log($"Changing GameState from '{GameState}' to '{newState}'");
            GameState = newState;
        }
    }
}
