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

    private Coroutine tickCoroutine;

    public bool IsDead { get; private set; }

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
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            TakeDamage(Random.Range(1, 20));
        }
        //
        //
    }
    public void StartHotHealing(float HealPerTick)
    {
        tickCoroutine = StartCoroutine(HealTicker(HealPerTick));
    }

    public void StartTakingDotDamage(float DamagePerTick)
    {
        tickCoroutine = StartCoroutine(Ticker(DamagePerTick));
    }

    //Can be called for one time damage/heal
    public void Heal(float amount)
    {
        if (health > 0)
        {
            health += amount;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            StopCoroutine(tickCoroutine);
        }

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

    private IEnumerator Ticker(float DamagePerTick)
    {
        while (true)
        {
            yield return new WaitForSeconds(tickRate);

            while (GameManager.Instance.IsGamePaused) yield return null;
            if (health > 0)
            {
                TakeDamage(DamagePerTick);
            }
        }
    }
    private IEnumerator HealTicker(float HealPerTick)
    {
        while (true)
        {
            yield return new WaitForSeconds(tickRate);

            while (GameManager.Instance.IsGamePaused) yield return null;
            if (health > 0)
            {
                Heal(HealPerTick);
            }
        }
    }


    private void UpdateHealthBar()
    {
        prefabHealthBar.transform.Find("Bar").localScale = new Vector3(GetHealthPercent(), 1);
        if (!IsDead && health == 0)
        {
            NodeDeath();
        }
    }

    private void NodeDeath()
    {
        IsDead = true;
        Vector3 pos = transform.position;
        CreepNode creep =  gameObject.AddComponent<CreepNode>();
    }
}
