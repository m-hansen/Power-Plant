using UnityEngine;

// Assumptions: No Settlements can overlap each other
public class Settlement : Node
{
    private const float CostMultiplier = 3.5f; // temporary to pad numbers

    public int Cost { get; private set; }
    //MIGHT NOT NEED BOOL
    public bool hasResource { get; private set; }
    [SerializeField]
    private int resourcePerSecond;

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

    private float CalculateBaseCost()
    {
        // For now just treat cost = distance from the plant origin
        // We may have a more complex formula later, or assign costs by hand if low on time
        return Vector3.Distance(GameManager.Instance.Plant.transform.position, transform.position) * CostMultiplier;
    }

    public bool IsConnectedToPowerPlant()
    {
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
