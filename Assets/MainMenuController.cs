using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button PlayButton;
    public Button CreditsButton;
    public Button QuitButton;
    public Button CreditsBackButton;

    public Canvas mainCanvas;
    public Canvas creditsCanvas;

    void Start()
    {
        mainCanvas.enabled = true;
        creditsCanvas.enabled = false;
    }

    void OnEnable()
    {
        PlayButton.onClick.AddListener(StartGame);
        CreditsButton.onClick.AddListener(ShowCredits);
        QuitButton.onClick.AddListener(QuitGame);
        CreditsBackButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void OnDisable()
    {
        CreditsBackButton.onClick.RemoveAllListeners();
        QuitButton.onClick.RemoveAllListeners();
        CreditsButton.onClick.RemoveAllListeners();
        PlayButton.onClick.RemoveAllListeners();
    }

    void StartGame()
    {
        QuitButton.onClick.RemoveAllListeners();
        CreditsButton.onClick.RemoveAllListeners();
        PlayButton.onClick.RemoveAllListeners();

        // TODO call a scene manager to load gameplay and unload this
    }

    void ShowCredits()
    {
        CreditsBackButton.onClick.AddListener(ReturnToMainMenu);

        QuitButton.onClick.RemoveAllListeners();
        CreditsButton.onClick.RemoveAllListeners();
        PlayButton.onClick.RemoveAllListeners();

        mainCanvas.enabled = false;
        creditsCanvas.enabled = true;
    }

    void ReturnToMainMenu()
    {
        CreditsBackButton.onClick.RemoveAllListeners();

        PlayButton.onClick.AddListener(StartGame);
        CreditsButton.onClick.AddListener(ShowCredits);
        QuitButton.onClick.AddListener(QuitGame);

        creditsCanvas.enabled = false;
        mainCanvas.enabled = true;
    }

    void QuitGame()
    {
        Application.Quit();
    }

}
