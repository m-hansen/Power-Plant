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
    [SerializeField] private Stopwatch time;

    [Header("Gameplay")]
    [SerializeField]
    private int startingResources = 25;

    [Header("Audio")]
    [SerializeField]
    [Tooltip("Primarily for development so we can easily handle the global audio stream from a convienent place.")]
    private bool isMutedOnLaunch = false;
    [SerializeField] private AudioClip backgroundMusic;

    // We may not end up using this for the jam
    public GameStateEnum GameState { get; private set; } = GameStateEnum.Gameplay; // Do not modify this variable directly - call ChangeGameState instead

    public bool IsGamePaused { get; private set; }

    // Note nodes are not dynamic and will only be created/updated on launch
    // Nodes are currently Settlements and the PowerPlant
    public Node[] Nodes { get; private set; } 

    public PowerPlant Player { get => powerPlant; }

    public CreepNode Creep { get => creep; }

    public Stopwatch GameplayTimer { get => time; }

    private void Start()
    {
        if (isMutedOnLaunch)
        {
            AudioManager.Instance.Mute(true);
        }

        OnNewGame(); // we won't want to do this in awake oncee make a menu or start loading these resources in another (persistent) scene
    }

    private void OnNewGame()
    {
        RegisterNodes();

        Player.PrimaryResource = startingResources;
        AudioManager.Instance.PlayMusic(backgroundMusic);

        // Start the camera on top of the player
        Vector3 playerPos = Player.transform.position;
        Camera.main.transform.position = new Vector3(playerPos.x, playerPos.y, Camera.main.transform.position.z);

        time?.Clear(); // Likely already done because the scene would've been loaded and the timer is not handled by the engine, just being safe
    }

    private void RegisterNodes()
    {
        Nodes = FindObjectsOfType<Node>();

        // Assign our own custom ids to a settlement instead of the engines instance id
        int currentId = 0;
        for (int i = 0; i < Nodes.Length; i++)
        {
            if (Nodes[i] is Settlement s)
            {
                s.Id = currentId;
                s.gameObject.name = $"Settlement_{currentId}";
                currentId++;
            }
        }
    }

    // TODO: We may need/want to make this public in the future. For now, we don't need to.
    private void ChangeGameState(GameStateEnum newState)
    {
        if (newState != GameState)
        {
            Debug.Log($"Changing GameState from '{GameState}' to '{newState}'");
            GameState = newState;
        }
    }
}
