using UnityEngine;
using System.Collections.Generic;

// Do not add more than one of these in a scene
public class GameManager : Singleton<GameManager>
{
    public enum GameStateEnum
    {
        // TODO: initializing, game over, menus?
        Observing,
        Editing,
    }

    [SerializeField]
    private int startingPrimaryResource = 25;

    public GameStateEnum GameState { get; private set; } = GameStateEnum.Observing; // Do not modify this variable directly - call ChangeGameState instead

    public List<Node> Nodes { get; private set; }

    public GameObject Plant { get; private set; }

    public int PrimaryResource { get; private set; } // TODO: name me

    private void Awake()
    {
        Plant = GameObject.FindWithTag("Player");
        PrimaryResource = startingPrimaryResource;

        RegisterAllNodes();

        //Start Timer
        if(TimeController.instance!= null) TimeController.instance.StartTimer();
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
