using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealthManager : MonoBehaviour
{
    public Transform prefabHealthBar;
    [SerializeField]
    private Vector3 healthBarOffset = new Vector3(0, 0.85f);
    [SerializeField]
    private float health = 100;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float tickRate = 1;
    [SerializeField]
    private bool pauseTick = false;
    private float nextTick = 1;
    HealthScript healthScript;
    public float tickingFor;

    void Start()
    {
        healthScript = new HealthScript(health, maxHealth);
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
            TickFor(tickingFor);
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

    public void TickFor(float value)
    {
        if (healthScript != null)
        {
            tickingFor = value;
            healthScript.Damage(value);
            UnpauseTick();
        }
        else
        {
            PauseTick();
        }
    }
    public void PauseTick()
    {
        pauseTick = true;
    }
    public void UnpauseTick()
    {
        pauseTick = false;
    }

    //Used for additional systems or applying effect in tandem with damage
    public void HealFor(float amount)
    {
        healthScript.Heal(amount);
        //Add other events or checks here(Sounds, Pop up text, etc)
    }
    public void DamageFor(float amount)
    {
        healthScript.Damage(amount);
        //Add other events or checks here
    }

    private void HealthScript_OnDeath(object sender, EventArgs e)
    {
        PauseTick();
    }




}
