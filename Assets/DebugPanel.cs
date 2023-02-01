using TMPro;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text selectedNodeText;

    void LateUpdate()
    {
        selectedNodeText.text =
            "FirstSelectedNode: " + EvaluateSelectedNodeValue(GameManager.Instance.FirstSelectedNode) + "\n" +
            "SecondSelectedNode: " + EvaluateSelectedNodeValue(GameManager.Instance.SecondSelectedNode);
    }

    private string EvaluateSelectedNodeValue(Node n) => n != null ? n.gameObject.name : "None";
}
