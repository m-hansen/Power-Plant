using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private HealthScript healthScript;
    public void Setup (HealthScript healthScript)
    {
        this.healthScript = healthScript;
        transform.Find("Bar").localScale = new Vector3(healthScript.GetHealthPercent(), 1);

        healthScript.OnHealthChanged += HealthScript_OnHealthChanged;
    }

    private void HealthScript_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(healthScript.GetHealthPercent(), 1);
    }
   
}
