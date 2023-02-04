using UnityEngine;

// Assumptions: No Settlements can overlap each other
public class Settlement : MonoBehaviour
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

    private float CalculateBaseCost()
    {
        // For now just treat cost = distance from the plant origin
        // We may have a more complex formula later, or assign costs by hand if low on time
        return Vector3.Distance(GameManager.Instance.Plant.transform.position, transform.position) * CostMultiplier;
    }

    public bool IsConnectedToPowerPlant()
    {

        // TODO: search graph space
        // FIXME: is already in use, plsfix :)
        return true;
    }
    public int GetResourcePerSecond()
    {
        return resourcePerSecond;
    }
}
