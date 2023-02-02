using TMPro;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text gameStateLabel;
    [SerializeField] private TMP_Text nodeCountLabel;

    void LateUpdate()
    {
        gameStateLabel.text = "Game State: " + GameManager.Instance.GameState.ToString();
        nodeCountLabel.text = "Node Count: " + GameManager.Instance.Nodes.Count.ToString();
    }

    private string EvaluateSelectedNodeValue(Node n) => n != null ? n.gameObject.name : "None";
}
