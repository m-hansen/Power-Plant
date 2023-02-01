using UnityEngine;

// Assumptions: No Settlements can overlap each other
public class Settlement : MonoBehaviour
{
    [SerializeField]
    private Color hoverColor;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor = Color.magenta; // magenta is used as an error color - we may fail to find the correct default color

    private bool isHoveredOver = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Assert(spriteRenderer != null);

        defaultColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (isHoveredOver && Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {

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
}
