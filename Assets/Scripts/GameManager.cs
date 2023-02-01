using UnityEngine;

// TODO: Enforce as singleton
// Do not add more than one of these in a scene
public class GameManager : Singleton<GameManager>
{
    public enum GameStateEnum
    {
        // TODO: initializing, game over, menus?
        Observing,
        Editing,
    }

    public GameStateEnum GameState { get; private set; } = GameStateEnum.Observing; // Do not modify this variable directly - call ChangeGameState instead

    // Convienence to access which nodes were selected by the player - must null check
    public Node FirstSelectedNode { get => selectedNodes[0]; }
    public Node SecondSelectedNode { get => selectedNodes[1]; }

    private Node[] selectedNodes = new Node[2];

    private void Awake()
    {
        // By design we're intending to only support two selections.
        // This may change, in which case we must update some assumptions in this class.
        Debug.Assert(selectedNodes.Length == 2);
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

    public void TrySelectNode(Node node)
    {
        bool wasNodeSelectionSuccessful = false;
        for (int i = 0; i < selectedNodes.Length; i++)
        {
            if (selectedNodes[i] == null)
            {
                selectedNodes[i] = node;
                wasNodeSelectionSuccessful = true;
                break;
            }
        }

        if (!wasNodeSelectionSuccessful)
        {
            Debug.LogWarning("Trying to select a new node, but there is already a first and second selection");
            // So if we get this warning should we clear out the nodes and have assigned this to the first selection? second selection? just ignore?
        }

        if (FirstSelectedNode != null) // we can assume if there is no first node, there's not a second
        {
            ChangeGameState(GameStateEnum.Editing);
        }
        else
        {
            ChangeGameState(GameStateEnum.Observing);
        }
    }

    // The state may end up always being observing, but we'll explicitly pass it for now just in case we end up calling this at the end of the game
    private void ClearSelectedNodes(GameStateEnum newState)
    {
        for (int i = 0; i < selectedNodes.Length; i++)
        {
            selectedNodes[i] = null;
        }

        ChangeGameState(newState);
    }
}
