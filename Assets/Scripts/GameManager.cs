using UnityEngine;

// TODO: Enforce as singleton
// Do not add more than one of these in a scene
public class GameManager : MonoBehaviour
{
    public enum GameModeEnum
    {
        // TODO: initializing, game over, menus?
        Observing,
        Editing,
    }

    public GameModeEnum GameMode { get; private set; } = GameModeEnum.Observing;
}
