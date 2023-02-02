using UnityEngine;

public class SettlementDebug : MonoBehaviour
{
    public TMPro.TMP_Text uiLabel;
    public Settlement settlementScript;

    void Update()
    {
        uiLabel.text = settlementScript.Cost.ToString();
    }
}
