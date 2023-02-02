using UnityEngine;

// Assumptions: No Settlements can overlap each other
public class Settlement : MonoBehaviour
{
    private const float CostMultiplier = 3.5f; // temporary to pad numbers

    public int Cost { get; private set; }

    private void Start()
    {
        Cost = (int)CalculateBaseCost(); // truncate our generated cost for simplicity
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
        // consider running a search from the power plant instead
        return true;
    }
}
