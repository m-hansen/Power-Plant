using UnityEngine;
using System.Collections.Generic;
using TMPro;

// Do not add more than one of these in a scene
public class GameManager : Singleton<GameManager>
{
    public enum GameStateEnum
    {
        // TODO: initializing, game over, menus?
        Observing,
        Editing,
    }

    public TMP_Text invokeText;
    [SerializeField]
    private float timeIncrement;
    private float pauseIncrement;
    //private bool timerStarted;
    private float invokeTime;
    private bool infectionStarted = false;

    [SerializeField]
    private float initialinfectionTime = 0.5f;


    [SerializeField]
    private PowerPlant powerPlant;

    [SerializeField]
    private CreepNode creep;

    [SerializeField]
    private int startingPrimaryResource = 25;

    public GameStateEnum GameState { get; private set; } = GameStateEnum.Observing; // Do not modify this variable directly - call ChangeGameState instead

    public List<Node> Nodes { get; private set; }

    public PowerPlant Plant { get => powerPlant; }
    public CreepNode Creep { get => creep; }

    public int PrimaryResource { get; private set; } // TODO: name me

    private void Awake()
    {
        PrimaryResource = startingPrimaryResource;

        RegisterAllNodes();

    }
    private void Start()
    {
        //Starts timer
        InvokeRepeating("InvokeTimer", 0, timeIncrement);
    }
    public void Pause()
    {

        //timer pause
        pauseIncrement = timeIncrement;
        timeIncrement = 0;
    }
    public void Unpause()
    {
        //timer unpause
        timeIncrement = pauseIncrement;
    }

    void InvokeTimer()
    {
        invokeText.text = invokeTime.ToString("00:00.00");
        invokeTime = invokeTime + timeIncrement;
        if (invokeTime > initialinfectionTime && !infectionStarted)
        {
            creep.InfectNode();
            infectionStarted = true;
        }
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
