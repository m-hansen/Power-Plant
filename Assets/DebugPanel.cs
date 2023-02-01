using TMPro;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text selectedNodeText;

    void LateUpdate()
    {
        selectedNodeText.text = $"FirstSelectedNode: {GameManager.Instance.FirstSelectedNode.gameObject.name}\n" +
            $"SecondSelectedNode: {GameManager.Instance.SecondSelectedNode}";
    }
}
