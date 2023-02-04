using System.Collections;
using UnityEngine;


public class HealthSystem : MonoBehaviour
{
    public GameObject prefabHealthBar;
    [SerializeField]
    private Vector3 healthBarOffset = new Vector3(0, 0.85f);
    [SerializeField]
    private float health = 100;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float tickRate = 1f;
    public bool IsInfected { get; private set; }


    void Start()
    {
        prefabHealthBar = Instantiate(prefabHealthBar, gameObject.transform.position + healthBarOffset, Quaternion.identity);
        prefabHealthBar.transform.parent = transform;
        UpdateHealthBar();
    }

    void Update()
    {
        //TODO
        //TEMP FOR DEBUG, REMOVE LATER
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Heal(Random.Range(1, 20));
            UpdateHealthBar();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Damage(Random.Range(1, 20));
            UpdateHealthBar();
        }
        //
        //
    }
    IEnumerator Ticker(float hp)
    {
        //isTicking = true;
        while (GameManager.Instance.IsGamePaused) yield return null;
        if (health != 0)
        { 
            DamageFor(hp);
        }
        yield return new WaitForSeconds(tickRate);
        if (!IsInfected) 
        {
            TickDamage(hp);
        }
    }

    private void UpdateHealthBar()
    {
        prefabHealthBar.transform.Find("Bar").localScale = new Vector3(GetHealthPercent(), 1);
        if (!IsInfected && health == 0)
        {
            NodeDeath();
        }
    }

    public void TickDamage(float hp)
    {
        StartCoroutine(Ticker(hp));
    }

    //Can be called for one time damage/heal
    public void HealFor(float hp)
    {
        Heal(hp);
        UpdateHealthBar();
    }
    public void DamageFor(float hp)
    {
        
        Damage(hp);
        UpdateHealthBar();
    }


    public float GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return health / maxHealth;

    }

    //Add effects?
    private void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0;
            StopAllCoroutines();
            
        }
    }
    //Should Probably add effects of some sort
    private void Heal(float healAmount)
    {
        if (health > 0)
        {
            health += healAmount;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void NodeDeath()
    {
        IsInfected = true;
        Vector3 pos = transform.position;
        CreepNode creep =  gameObject.AddComponent<CreepNode>();
    }
}
