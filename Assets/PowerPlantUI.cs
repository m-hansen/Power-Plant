using UnityEngine;

public class PowerPlantUI : MonoBehaviour
{
    public PowerPlant powerPlantScript;
    public TMPro.TMP_Text resourceLabel;

    void Update()
    {
        resourceLabel.text = ((int)powerPlantScript.ResourceValue).ToString();
    }
}
