using TMPro;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text selectedNodeText;

    void LateUpdate()
    {
        // Intentionally left blank for now - the only debug info being used was removed from the game manager
    }

    private string EvaluateSelectedNodeValue(Node n) => n != null ? n.gameObject.name : "None";
}
