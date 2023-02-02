using UnityEngine;

// Assumptions: No Settlements can overlap each other
public class Settlement : MonoBehaviour
{
    private const float CostMultiplier = 3.5f; // temporary to pad numbers

    [SerializeField]
    private Color hoverColor;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor = Color.magenta; // magenta is used as an error color - we may fail to find the correct default color

    private bool isHoveredOver = false; // old code used to highlight nodes, will likely be removed soon in favor of a different technique

    public int Cost { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Assert(spriteRenderer != null);

        defaultColor = spriteRenderer.color;
    }

    private void Start()
    {
        Cost = (int)CalculateBaseCost(); // truncate our generated cost for simplicity
    }

    private void OnMouseEnter()
    {
        isHoveredOver = true;
        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = defaultColor;
        isHoveredOver = false;
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
