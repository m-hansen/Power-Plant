using UnityEngine;

public class PowerPlant : Node
{
    [SerializeField]
    private float resourcesPerSecond = 1f;
    public int currentHeldHPS =0;
    public int MaxHeldHPS = 5;
    private int TimeIncrement=1;
    public float resourceLastSpent { get; private set; }

    public float PrimaryResource { get; set; }

    private void Awake()
    {
        HealthSystem = gameObject.GetComponentInChildren<HealthSystem>(); // TODO: 8305
        VineMaterialName = "Vine";
    }

    //Make coruntine or something
    private void Start()
    {
        InvokeRepeating("InvokeResourcePerSecond", 0, TimeIncrement);

        PrimaryResource += resourcesPerSecond * Time.deltaTime;
    }
     //call from settlement or something and if has resource make blue and spawn blue +1 to the right of settlement
    public void AddResourcePerSecond(float n)
    {
        resourcesPerSecond+= n;
    }

    public void AddHeldHPS()
    {
        currentHeldHPS++;
        currentHeldHPS = Mathf.Clamp(currentHeldHPS, 0, MaxHeldHPS);
    }

    public void RemoveHeldHPS()
    {
        currentHeldHPS--;
        currentHeldHPS = Mathf.Clamp(currentHeldHPS, 0, MaxHeldHPS);
    }

    void InvokeResourcePerSecond()
    {
        PrimaryResource += resourcesPerSecond;
    }

    public void ExpendResource(float n)
    {
        PrimaryResource -= n;
        Canvas c = gameObject.GetComponentInChildren<Canvas>();
        PowerPlantUI ui = c.GetComponentInChildren<PowerPlantUI>();
        ui.SpendResourceUI(n);
        if (PrimaryResource < 0)
        {
            Debug.LogWarning("You didn't have enough resources to expend, yet you did anyway. Something may be wrong.");
            PrimaryResource = 0;
        }
    }
}
