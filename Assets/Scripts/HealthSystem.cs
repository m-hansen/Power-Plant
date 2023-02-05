using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class HealthSystem : MonoBehaviour
{
    public GameObject prefabHealthBar;
    public Button plus;
    public Button minus;
    [SerializeField]
    private Vector3 healthBarOffset = new Vector3(0, 0.85f); // it's more difficult to manage some UI through code and the other through prefabs, we have to guess numbers and wont ever see a preview, eg: I move health and the + / - buttons dont follow
    [SerializeField]
    private float health = 100;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float tickRate = 1f;
    private Coroutine tickerCoroutine;
    private float Dmg;
    private float Heal;
    private ColorBlock colorSet;
    private bool tickerRunning = false;

    [Header("Audio")]
    [SerializeField] private AudioClip plusClickSound;
    [SerializeField] private AudioClip minusClickSound;

    public float Tick { get; private set; }

    public bool IsDead { get; private set; }

    void Start()
    {
        Button plusbtn = plus.GetComponent<Button>();
        Button minusbtn = minus.GetComponent<Button>();
        prefabHealthBar = Instantiate(prefabHealthBar, gameObject.transform.position + healthBarOffset, Quaternion.identity);
        prefabHealthBar.transform.parent = transform;
        plusbtn.onClick.AddListener(OnClickPlus);
        minusbtn.onClick.AddListener(OnClickMinus);
        UpdateHealthBar();
        colorSet = minus.colors;
        colorSet.disabledColor = Color.grey;
    }

    void Update()
    {
        int maxheld = GameManager.Instance.Player.MaxHeldHPS;
        int currentheld = GameManager.Instance.Player.currentHeldHPS;
        //TODO
        //TEMP FOR DEBUG, REMOVE LATER
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log(currentheld);
            //GameManager.Instance.Player.AddHeldHPS();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Debug.Log(maxheld);
            //GameManager.Instance.Player.RemoveHeldHPS();
        }
        //
        //
    }

    private void OnClickPlus()
    {
        int maxheld = GameManager.Instance.Player.MaxHeldHPS;
        int currentheld = GameManager.Instance.Player.currentHeldHPS;

        if (plusClickSound != null)
        {
            AudioManager.Instance.PlaySound(plusClickSound);
        }

        if (currentheld !=0)
        {
            minus.interactable = true;
            TickAmountIncrementBy(1);
            GetComponentInChildren<Settlement>().AddToResource(1);
            GameManager.Instance.Player.RemoveHeldHPS();
            Debug.Log(currentheld);
        }
        else plus.interactable = false;
    }

    private void OnClickMinus()
    {
        int maxheld = GameManager.Instance.Player.MaxHeldHPS;
        int currentheld = GameManager.Instance.Player.currentHeldHPS;

        if (minusClickSound != null)
        {
            AudioManager.Instance.PlaySound(minusClickSound);
        }

        if (Tick > 0 && currentheld < maxheld)
        {
            plus.interactable = true;
            TickAmountIncrementBy(-1);
            GetComponentInChildren<Settlement>().AddToResource(-1);
            GameManager.Instance.Player.AddHeldHPS();
            Debug.Log(currentheld);
        }
        else minus.interactable = false;
    }

    public void TickAmountIncrementBy(int damageorheal)
    {
        Tick = Tick +damageorheal;
    }

    public void StartTakingDotDamage(float DmgPerTick)
    {
        Dmg = DmgPerTick;
        CombineTicks();
    }

    public void StartHotHealing(float HealPerTick)
    {
        Heal = HealPerTick;
        CombineTicks();
    }
    public void CombineTicks()
    {
        if (!tickerRunning)
        {
            tickerCoroutine = StartCoroutine(TickerCoroutine());
        }

       Tick = Heal - Dmg;
    }
    
    private void ApplyHealth()
    {
        health = health + Tick;
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();
    }

    public float GetHealthPercent()
    {
        return health / maxHealth;
    }
    
    private IEnumerator TickerCoroutine()
    {
        tickerRunning=true;
        while (true)
        {
            ApplyHealth();
            yield return new WaitForSeconds(tickRate);
            while (GameManager.Instance.IsGamePaused) yield return null;
            plus.interactable = true;
            minus.interactable = true;
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
        CreepNode creep =  gameObject.AddComponent<CreepNode>();
        StopCoroutine(tickerCoroutine);
    }
}
