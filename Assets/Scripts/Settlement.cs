using UnityEngine;
using static UnityEngine.UI.Image;

// Assumptions: No Settlements can overlap each other
public class Settlement : Node
{
    private const float CostMultiplier = 3.5f; // temporary to pad numbers

    public int Cost { get; private set; }
    //MIGHT NOT NEED BOOL
    public bool hasResource { get; private set; }
    public bool isInfected { get; private set; }
    [SerializeField]
    private int resourcePerSecond;

    [SerializeField]
    private Color vineColor = Color.green;

    public int Id { get; set; }

    private void Awake()
    {
        HealthSystem = gameObject.GetComponentInChildren<HealthSystem>(); // TODO: 8305
        edgeColor = vineColor;
        VineMaterialName = "Vine";
    }

    private void Start()
    {
        Cost = (int)CalculateBaseCost(); // truncate our generated cost for simplicity
        if(resourcePerSecond>0)
        {
            hasResource = true;
            //TODO: Change Color to blue and add water icon and +1
        }
    }

    private void Update()
    {
        if (IsConnectedToPowerPlant())
        {
            // Temp for debugging / testing
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void StartDamageTick(float hpPerTick)
    {
        // Question:
        // Does infect imply death?
        // or does it mean that we start taking damage from evil, but we may be alive still and under the player's control?
        Debug.Log($"The creep is nearby. I'm starting to take damage! (Settlement Id: {Id})");
        HealthSystem.StartTakingDotDamage(hpPerTick);
        isInfected= true;
    }

    private float CalculateBaseCost()
    {
        // For now just treat cost = distance from the plant origin
        // We may have a more complex formula later, or assign costs by hand if low on time
        return Vector3.Distance(GameManager.Instance.Player.transform.position, transform.position) * CostMultiplier;
    }

    public bool IsConnectedToPowerPlant()
    {
        var CreepOrigin = gameObject.GetComponent<CreepNode>();
        if (CreepOrigin)
        {
            return false;
        }
        // HACK - lets assume any node with ANY adjacent nodes MUST be connected to a power plant... for now. This assumption should be true for this game anyway but-
        // it wont be true if we're checking in teh creep graph OR if we have two nodes connected somehow.
        // This will be cleaned up tomorrow on 2/4/23
        return adjacentNodes.Count > 0;
    }

    public int GetResourcePerSecond()
    {
        return resourcePerSecond;
    }
}
