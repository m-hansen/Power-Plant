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
        healthScript.OnDeath+= HealthScript_OnDeath;
    }

    private void HealthScript_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(healthScript.GetHealthPercent(), 1);
    }

    private void HealthScript_OnDeath(object sender, System.EventArgs e)
    {
        Fade();
    }

    IEnumerator Fade ()
    {
        yield return new WaitForSeconds(2f);
    }
   
}
