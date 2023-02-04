using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PowerPlantUI : MonoBehaviour
{
    public PowerPlant powerPlantScript;
    public TMPro.TMP_Text resourceLabel;
    public TMPro.TMP_Text resourceSpentLabel;
    private float lastSpent;

    void Update()
    {
        resourceLabel.text = ((int)powerPlantScript.ResourceValue).ToString();
    }
    
    public void SpendResourceUI(float n)
    {
        lastSpent= n;
        StartCoroutine(UpdateSpending());
    }

    IEnumerator UpdateSpending()
    {
        while (GameManager.Instance.IsGamePaused) yield return null;
        resourceSpentLabel.enabled = true;
        resourceSpentLabel.text = $"-{(int)lastSpent}";
        yield return new WaitForSeconds(1);
        resourceSpentLabel.enabled = false;
        yield break;
    }
        
}
