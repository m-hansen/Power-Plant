using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public GameObject prefabHealthBar;
    public Button plus;
    public Button minus;
    [SerializeField]
    private Vector3 healthBarOffset = new Vector3(0, 0.85f);
    [SerializeField]
    private float health = 100;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float tickRate = 1f;
    public int isTickingDmg { get; private set; }
    public int isTickingHeal { get; private set; }

    private Coroutine tickCoroutine;
    private ColorBlock colorReset;
    private ColorBlock colorSet;

    public bool IsDead { get; private set; }

    void Start()
    {
        Button plusbtn =plus.GetComponent<Button>();
        Button minusbtn = minus.GetComponent<Button>();
        prefabHealthBar = Instantiate(prefabHealthBar, gameObject.transform.position + healthBarOffset, Quaternion.identity);
        prefabHealthBar.transform.parent = transform;
        plusbtn.onClick.AddListener(OnClickPlus);
        minusbtn.onClick.AddListener(OnClickMinus);
        UpdateHealthBar();
        colorSet = minus.colors;
        colorReset = minus.colors;
        colorSet.disabledColor = Color.grey;

        //TEMP
        if (isTickingDmg ==0)
        {
            minus.interactable = false;
        }
        minus.interactable = false;
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

    private void OnClickPlus()
    {
        /*        if (heldHPS < 1)
                {
                    heldHPS -= heldHPS;
                    isTickingHeal++;
                }*/
        if (isTickingHeal < 0)
        {
            isTickingDmg--;
        }
        if (isTickingHeal >= 0)
        {
            isTickingHeal++;
        }
        if (isTickingDmg < 0)
        {
            isTickingHeal++;
        }

    }
    private void OnClickMinus()
    {
        //Button b = minus.GetComponent<Button>();
        if (isTickingHeal == 0)
        {
            minus.interactable = false;
        }

        if (isTickingHeal > 0) 
        {
            isTickingHeal--;
        }
        if (isTickingDmg < 0)
        {
            isTickingDmg++;

            minus.interactable = true;
        }
    }



    public void StartHotHealing(float HealPerTick)
    {
        tickCoroutine = StartCoroutine(HealTicker(HealPerTick));
        isTickingHeal = (int)HealPerTick;
    }

    public void StartTakingDotDamage(float DamagePerTick)
    {
        tickCoroutine = StartCoroutine(Ticker(DamagePerTick));
        isTickingDmg = (int)DamagePerTick;
    }

    //Can be called for one time damage/heal
    public void Heal(float amount)
    {
        Mathf.Clamp(amount, 0f, 99f);
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
