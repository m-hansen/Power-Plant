using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private float tickRate = 1;
    private float nextTick = 1;
    HealthScript healthScript;
    public Transform prefabHealthBar;


    // Start is called before the first frame update
    void Start()
    {
        healthScript = new HealthScript(80,100);

        Transform healthBarTransform = Instantiate(prefabHealthBar, new Vector3(0,0.85f),Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthScript);
    }

    // Update is called once per frame
    void Update()
    {
        // If the next update is reached
        if (Time.time >= nextTick)
        {
            nextTick = Mathf.FloorToInt(Time.time) + tickRate;
            Tick();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            healthScript.Heal(15);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            healthScript.Damage(15);
        }
    }

    void Tick()
    {
        healthScript.Damage(5);
        Debug.Log("Health" + healthScript.GetHealth());

    }


}
