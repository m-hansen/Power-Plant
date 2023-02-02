using UnityEngine;

public class PowerPlant : MonoBehaviour
{
    [SerializeField]
    private float primaryResource = 25f;

    [SerializeField]
    private float tempResourcesPerSecond = 1f;

    public float ResourceValue
    {
        get => primaryResource;
    }

    private void Update()
    {
        primaryResource += tempResourcesPerSecond * Time.deltaTime;
    }

    public void ExpendResource(float amount)
    {
        primaryResource -= amount;
        if (primaryResource < 0)
        {
            Debug.LogWarning("You didn't have enough resources to expend, yet you did anyway. Something may be wrong.");
            primaryResource = 0;
        }
    }
}
