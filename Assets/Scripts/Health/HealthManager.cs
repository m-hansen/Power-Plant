using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField]
    private float tickRate = 1;
    [SerializeField]
    private Vector3 healthBarOffset = new Vector3(0, 0.85f);
    [SerializeField]
    private bool pauseTick = false;

    private float hp;
    private float nextTick = 1;
    HealthScript healthScript;
    public Transform prefabHealthBar;

    void Start()
    {
        healthScript = new HealthScript(100, 100);
        Transform healthBarTransform = Instantiate(prefabHealthBar, gameObject.transform.position + healthBarOffset, Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthScript);
        healthScript.OnDeath += HealthScript_OnDeath;
    }

    void Update()
    {
        // If the next update is reached
        if (Time.time >= nextTick && !pauseTick)
        {
            nextTick = Mathf.FloorToInt(Time.time) + tickRate;
            Tick();
        }

        //TODO
        //TEMP FOR DEBUG, REMOVE LATER
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            healthScript.Heal(Random.Range(1, 20));
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            healthScript.Damage(Random.Range(1, 20));
        }
        //
        //
    }

    void Tick()
    {
        if (healthScript != null)
        {
            healthScript.Damage(5);
        }
        else
        {
            PauseTick();
        }
        //Debug.Log("Health" + healthScript.GetHealth());
    }
    public void PauseTick()
    {
        pauseTick = true;
    }
    public void UnpauseTick()
    {
        pauseTick = false;
    }

    private void HealthScript_OnDeath(object sender, System.EventArgs e)
    {
        PauseTick();
    }




}
