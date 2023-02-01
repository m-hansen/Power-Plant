using UnityEngine;

public class ToggleDebugCanvas : MonoBehaviour
{
    [SerializeField]
    private KeyCode toggleKey = KeyCode.F3;

    [SerializeField]
    private GameObject debugPanel;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            debugPanel.SetActive(!debugPanel.activeInHierarchy);
        }
    }
}
