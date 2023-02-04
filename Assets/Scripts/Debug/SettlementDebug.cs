using UnityEngine;
using UnityEngine.UI;

public class SettlementDebug : MonoBehaviour
{
    public TMPro.TMP_Text cost;
    public TMPro.TMP_Text resource;
    public TMPro.TMP_Text id;
    public Image resourcImg;
    public Settlement settlementScript;

    void Update()
    {
        id.text = "ID: " + settlementScript.Id.ToString();
        cost.text = settlementScript.Cost.ToString();

        if (settlementScript.hasResource)
        {
            resourcImg.enabled= true;
            if (settlementScript.IsConnectedToPowerPlant())
            {
                resource.enabled = true;
                resource.text = $"+{settlementScript.GetResourcePerSecond()}/s";
            }
        }
    }
}
