using TMPro;
using UnityEngine;

public class UIRefresher : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gameplayTimeLabel;

    [SerializeField]
    private GameObject howToPlayPanel;

    private void Start()
    {
        InvokeRepeating("UpdateGameTimeUI", 0.001f, 1f); // the user doesn't need to see ultra accurate time updates, but it will still be stored on the backend
    }

    private void Update()
    {
        // Toggle how to play 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            howToPlayPanel.SetActive(!howToPlayPanel.activeInHierarchy);
        }
    }

    private void UpdateGameTimeUI()
    {
        gameplayTimeLabel.text = GameManager.Instance.GameplayTimer.FormattedTime;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
