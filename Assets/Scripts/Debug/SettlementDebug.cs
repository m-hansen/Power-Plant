using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class SettlementDebug : MonoBehaviour
{
    public TMPro.TMP_Text cost;
    public TMPro.TMP_Text resource;
    public TMPro.TMP_Text id;
    public TMPro.TMP_Text depth;
   // public TMPro.TMP_Text hps;
    public TMPro.TMP_Text tick;

    public Image resourcImg;
    public Image plusImg;
    public Image minusImg;
    public Settlement settlementScript;

    void Update()
    {
        id.text = "ID: " + settlementScript.Id.ToString();
        depth.text = "Depth: " + settlementScript.Depth.ToString();
        cost.text = settlementScript.Cost.ToString();
        tick.text = $"{settlementScript.HealthSystem.Tick}";
        if (settlementScript.HealthSystem.Tick>0)
        {
            tick.color = new Color32(80, 190, 37, 255);
            tick.enabled = true;
        }
        if (settlementScript.HealthSystem.Tick == 0)
        {
            tick.color = new Color32(175, 175, 175, 255);
        }
        if (settlementScript.HealthSystem.Tick < 0)
        {
            tick.color = new Color32(190, 37, 40, 255);
            tick.enabled = true;
        }

        if (settlementScript.IsConnectedToPowerPlant()& !settlementScript.isInfected)
        {
            plusImg.enabled = true;
            minusImg.enabled = true;
            cost.enabled = false;
            settlementScript.HealthSystem.prefabHealthBar.SetActive(true);
            //tick.enabled = true;
            //hps.enabled = true;
            //tick.text = $"{settlementScript.HealthSystem.isCurrentlyTickingfor}";
            //hps.text = $"+{settlementScript.HealthSystem.isTickingHeal}";
            if (settlementScript.hasResource)
            {
                resourcImg.enabled = true;
                if (settlementScript.IsConnectedToPowerPlant())
                {
                    resource.enabled = true;
                    resource.text = $"+{settlementScript.GetResourcePerSecond()}/s";

                }
            }
        }
        if(settlementScript.isInfected)
        {
            settlementScript.HealthSystem.prefabHealthBar.SetActive(true);
            //tick.enabled = true;
            //hps.enabled = true;
            cost.enabled = false;
            //tick.text = $"{settlementScript.HealthSystem.isTickingDmg}";
            //hps.text = $"+{settlementScript.HealthSystem.isTickingHeal}";
        }
        else
        {
            cost.text = settlementScript.Cost.ToString();
        }
    }
}
