using UnityEngine;

public class PowerPlant : Node
{
    [SerializeField]
    private float primaryResource = 10f;

    [SerializeField]
    private float ResourcesPerSecond = 1f;
    private int timeIncrement=1;
    public float resourceLastSpent { get; private set; }

    public float ResourceValue
    {
        get => primaryResource;
    }


    //Make coruntine or something
    private void Start()
    {
        InvokeRepeating("InvokeResourcePerSecond", 0, timeIncrement);
        primaryResource += ResourcesPerSecond * Time.deltaTime;
    }
     //call from settlement or something and if has resource make blue and spawn blue +1 to the right of settlement
    public void AddResourcePerSecond(float n)
    {
        ResourcesPerSecond+= n;
    }

    void InvokeResourcePerSecond()
    {
        primaryResource += ResourcesPerSecond;
    }



    public void ExpendResource(float n)
    {
        primaryResource -= n;
        Canvas c = gameObject.GetComponentInChildren<Canvas>();
        PowerPlantUI ui = c.GetComponentInChildren<PowerPlantUI>();
        ui.SpendResourceUI(n);
        if (primaryResource < 0)
        {
            Debug.LogWarning("You didn't have enough resources to expend, yet you did anyway. Something may be wrong.");
            primaryResource = 0;
        }
    }
}
