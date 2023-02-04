using UnityEngine;
using UnityEngine.UI;

public class SettlementDebug : MonoBehaviour
{
    public TMPro.TMP_Text cost;
    public TMPro.TMP_Text resource;
    public TMPro.TMP_Text id;
    public TMPro.TMP_Text depth;

    public Image resourcImg;
    public Image plusImg;
    public Image minusImg;
    public Settlement settlementScript;


    void Update()
    {
        id.text = "ID: " + settlementScript.Id.ToString();
        depth.text = "Depth: " + settlementScript.Depth.ToString();
        cost.text = settlementScript.Cost.ToString();
        if (settlementScript.hasResource)
        {
            resourcImg.enabled = true;
            if (settlementScript.IsConnectedToPowerPlant())
            {
                resource.enabled = true;
                resource.text = $"+{settlementScript.GetResourcePerSecond()}/s";

            }
        }
        if (settlementScript.IsConnectedToPowerPlant())
        {
            plusImg.enabled = true;
            minusImg.enabled = true;
            cost.enabled = false;
        }
        else
        {
            cost.text = settlementScript.Cost.ToString();
        }
    }
}
